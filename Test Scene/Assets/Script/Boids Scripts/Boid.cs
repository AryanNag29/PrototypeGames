using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace PrototypeGames
{
    public class Boid : MonoBehaviour
    {
        #region Reference

        private BoidSettings _settings;

        #endregion

        #region Variables

        //state
        [HideInInspector] public Vector3 position;
        [HideInInspector] public Vector3 forward;
        private Vector3 velocity;

        //To update
        private Vector3 acceleration;
        [HideInInspector] public Vector3 avgFlockHeading;
        [HideInInspector] public Vector3 avgAvoidHeading;
        [HideInInspector] public Vector3 centerofFlockmates;
        [HideInInspector] public int numPerceivedFlockmates;

        //Cached
        private Material _material;
        private Transform _cachedTransform;
        private Transform _target;

        #endregion
        

        #region Awake

        private void Awake()
        {
            _material = transform.GetComponentInChildren<MeshRenderer>().material;
            _cachedTransform = transform;
        }

        #endregion

        #region Functions

        public void Initialize(BoidSettings settings, Transform target)
        {
            this._target = target;
            this._settings = settings;

            position = _cachedTransform.position;
            forward = _cachedTransform.forward;

            float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
            velocity = transform.forward * startSpeed;
        }

        public void SetColor(Color col)
        {
            if (_material != null)
            {
                _material.color = col;
            }
        }

        public void UpdateBoid()
        {
            Vector3 acceleration = Vector3.zero;

            if (_target != null)
            {
                Vector3 offsetToTarget = (_target.position - position);
                acceleration = SteerTowards(offsetToTarget) * _settings.targetWeight;
            }

            if (numPerceivedFlockmates != 0)
            {
                centerofFlockmates /= numPerceivedFlockmates;

                Vector3 offsetToFlockmatesCentre = (centerofFlockmates - position);

                var alignmentForce = SteerTowards(avgFlockHeading) * _settings.alignWeight;
                var cohesionForce = SteerTowards(offsetToFlockmatesCentre) * _settings.cohesionWeight;
                var seprationForce = SteerTowards(avgAvoidHeading) * _settings.seprateWeight;

                acceleration += alignmentForce;
                acceleration += cohesionForce;
                acceleration += seprationForce;
            }

            if (IsHeadingForCollision())
            {
                Vector3 collisionAvoidDir = ObstacleRays();
                Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * _settings.avoidCollisionWeight;
                acceleration += collisionAvoidForce;
            }

            velocity += acceleration * Time.deltaTime;
            float speed = velocity.magnitude;
            Vector3 dir = velocity / speed;
            speed = Mathf.Clamp(speed, _settings.minSpeed, _settings.maxSpeed);
            velocity = dir * speed;

            _cachedTransform.position += velocity * Time.deltaTime;
            _cachedTransform.forward = dir;
            position = _cachedTransform.position;
            forward = dir;
        }

        bool IsHeadingForCollision()
        {
            RaycastHit hit;
            if (Physics.SphereCast(position, _settings.boundsRadius, forward, out hit, _settings.collisionAvoidDst,
                    _settings.obstacleMask))
            {
                return true;
            }
            else
            {
            }

            return false;
        }

        Vector3 ObstacleRays()
        {
            Vector3[] rayDirection = BoidHelper.direction;
            for (int i = 0; i < rayDirection.Length; i++)
            {
                Vector3 dir = _cachedTransform.TransformDirection(rayDirection[i]);
                Ray ray = new Ray(position, dir);
                if (!Physics.SphereCast(ray, _settings.boundsRadius, _settings.collisionAvoidDst,
                        _settings.obstacleMask))
                {
                    return dir;
                }
            }

            return forward;
        }

        Vector3 SteerTowards(Vector3 vector)
        {
            Vector3 v = vector.normalized * _settings.maxSpeed - velocity;
            return Vector3.ClampMagnitude(v, _settings.maxSteerForce);
        }

        #endregion
    }
}