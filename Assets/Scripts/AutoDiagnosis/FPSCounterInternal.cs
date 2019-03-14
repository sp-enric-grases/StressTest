using UnityEngine;

namespace SP.Tools.StressTest
{
    public class FPSCounterInternal : MonoBehaviour
    {
        public float update = 0.1f;

        private float timeleft = 0;
        private float frames = 0;
        private float accum = 0;

        public static FPSCounterInternal Instance;

        public float AverageFPS
        {
            get { return Mathf.RoundToInt((accum / frames)); }
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        void OnEnable()
        {
            timeleft = update;
        }

        void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            if (timeleft <= 0.0)
            {
                timeleft = update;
                accum = 0;
                frames = 0;
            }
        }
    }
}