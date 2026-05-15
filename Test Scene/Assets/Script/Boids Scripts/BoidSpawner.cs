using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PrototypeGames
{
    public class BoidSpawner : MonoBehaviour
    {
        #region Enum

        public enum GismozType
        {
            Never,
            SelectedOnly,
            Always
        }

        #endregion

        #region References

        public Boid prefab;

        #endregion


        #region Variables

        public float spawnRadius = 10;
        public float spawnCount = 10;
        public Color colour;
        public float gizmosVisiblity = 0.3f;
        public GismozType showSpawnRegion;

        #endregion

        #region Awake

        private void Awake()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
                Boid boid = Instantiate(prefab);
                boid.transform.position = pos;
                boid.transform.forward = Random.insideUnitSphere;
            }
        }

        #endregion

        #region Functions

        private void OnDrawGizmos()
        {
            if (showSpawnRegion == GismozType.Always)
            {
                DrawGismos();
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (showSpawnRegion == GismozType.SelectedOnly)
            {
                DrawGismos();
            }
        }

        private void DrawGismos()
        {
            Gizmos.color = new Color(colour.r, colour.g, colour.b, gizmosVisiblity);
            Gizmos.DrawSphere(transform.position, spawnRadius);
        }

        #endregion
    }
}