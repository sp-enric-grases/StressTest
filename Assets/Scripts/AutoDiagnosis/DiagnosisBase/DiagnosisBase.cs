using SocialPoint.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SP.Tools.StressTest
{
    public class DiagnosisBase : MonoBehaviour
    {
        public GameObject cam;
        public GameObject canvas;

        protected Diagnosi diagnosi;

        public virtual void Start()
        {
            if (!DiagnosisState.automaticDiagnosisIsEnabled) return;
        }

        protected void StarTest()
        {
            if (diagnosi.isActiveForTesting)
            {
                Debug.Log("Loading " + diagnosi.scene);

                StartCoroutine(StartTest());
                DestroyImmediate(cam.GetComponent<CameraOrbit_v2>());
                DestroyImmediate(canvas);
            }
        }

        IEnumerator StartTest()
        {
            yield return new WaitForSeconds(DiagnosisState.cooldown);
            StartCoroutine(Test());
        }

        protected IEnumerator Test()
        {
            yield return new WaitForEndOfFrame();

            if (FPSCounterInternal.Instance.AverageFPS > DiagnosisState.downLimit60 || FPSCounterInternal.Instance.AverageFPS < DiagnosisState.spikeWarning)
            {
                FirstPass();
            }
            else if (DiagnosisState.activate30Limit && FPSCounterInternal.Instance.AverageFPS > DiagnosisState.downLimit30 || FPSCounterInternal.Instance.AverageFPS < DiagnosisState.spikeWarning)
            {
                SecondPass();
            }
            else
            {
                PrintHeader();
                PrintIterarion();
                PrintAverage();
                ResetDiagnosi();

                SceneManager.LoadScene(DiagnosisState.mainScene);
            }
        }

        public virtual void FirstPass() { }
        public virtual void SecondPass() { }
        public virtual void SetFirstPassValue() { }
        public virtual void ResetDiagnosi() { }
        public virtual void PrintHeader() { }
        public virtual void PrintIterarion() { }
        public virtual void PrintAverage() { }

        protected string SetFormat(float value)
        {
            string str = value.ToString("N", new CultureInfo("es-ES"));
            return str.Remove(str.Length - 3);
        }

        protected string GetAverage(List<int> data)
        {
            int total = 0;
            int max = data.Max();

            foreach (var item in data.ToList())
            {
                if (((float)item/(float)max) < DiagnosisState.threshold)
                    data.Remove(item);
            }

            foreach (var item in data)
                total += item;

            return SetFormat(total / data.Count());
        }
    }
}