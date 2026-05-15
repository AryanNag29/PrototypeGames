using UnityEngine;

namespace PrototypeGames
{
    public class BoidSettings : ScriptableObject
    {
        #region Variables

        //boid Settings
        public float minSpeed = 2f;
        public float maxSpeed = 5f;
        public float preceptionRedius = 2.5f;
        public float avoidanceRadius = 1f;
        public float maxSteerForce = 3f;

        public float alignWeight = 1f;
        public float cohesionWeight = 1f;
        public float seprateWeight = 1f;

        public float targetWeight = 1f;

        [Header("Collisions")] public LayerMask obstacleMask;
        public float boundsRadius = 0.27f;
        public float avoidCollisionWeight = 10f;
        public float collisionAvoidDst = 5f;

        #endregion
    }
}