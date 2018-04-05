using System.Collections;
using UnityEngine;

namespace Assets.Planet.Scripts
{
    public class RotationSpeedManager : MonoBehaviour
    {
        public float Speed = 10f;
        public float Interval = 10f;
        public float SpeedIncrement = 10f;
        
        private void Start()
        {
            StartCoroutine(increaseSpeedCoroutine());
        }

        private IEnumerator increaseSpeedCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Interval);

                Speed += SpeedIncrement;
            }
        }
    }
}
