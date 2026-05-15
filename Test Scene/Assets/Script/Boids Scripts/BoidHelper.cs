using Unity.Mathematics;
using UnityEngine;

namespace PrototypeGames
{
    public static class BoidHelper
    {
        #region variables

        private const int numViewDirection = 300;
        public static readonly Vector3[] direction;

        #endregion

        #region Constructor

        static BoidHelper()
        {
            direction = new Vector3[BoidHelper.numViewDirection];
            float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
            float angleincrement = Mathf.PI * 2 * goldenRatio;

            for (int i = 0; i < numViewDirection; i++)
            {
                float t = (float)i / numViewDirection;
                float inclination = Mathf.Acos(1 - 2 * t);
                float azimuth = angleincrement * i;

                float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                float y = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                float z = Mathf.Cos(inclination);
                direction[i] = new Vector3(x, y, z);
            }
        }

        #endregion
    }
}