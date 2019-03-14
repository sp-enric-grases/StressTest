using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SP.Tools.StressTest
{
    public class LoadSceneManager : MonoBehaviour
    {
        private const string DIAGNOSIS_SELECTOR = "DiagnosisSelector";
        private const string MANUAL_MAIN_SCENE = "ManualMainScene";
        private const string DIAGNOSIS_PANEL = "DiagnosisPanel";
        private const string AUTOMATIC_DIAGNOSIS = "AutomaticDiagnosis";

        private static LoadSceneManager instance;

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        public void LoadScene(string str)
        {
            SceneManager.LoadScene(str);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (SceneManager.GetActiveScene().name.ToString())
                {
                    case MANUAL_MAIN_SCENE:
                    case DIAGNOSIS_PANEL:
                    case AUTOMATIC_DIAGNOSIS:
                        SceneManager.LoadScene(DIAGNOSIS_SELECTOR);
                        break;
                    case DIAGNOSIS_SELECTOR:
                        break;
                    default:
                        SceneManager.LoadScene(MANUAL_MAIN_SCENE);
                        break;
                }
            }
        }

        public void SetAutomaticDiagnosisEnabled(bool state)
        {
            DiagnosisState.automaticDiagnosisIsEnabled = state;
        }
    }
}
