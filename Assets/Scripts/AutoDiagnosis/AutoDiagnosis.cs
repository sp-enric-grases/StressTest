using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SP.Tools.StressTest
{
    public class AutoDiagnosis : MonoBehaviour
    {
        public GameObject canvas;
        public TMP_Text log;
        public static AutoDiagnosis instance;

        private bool logHasBeenShown = false;

        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DiagnosisState.diagnosisIsDone = false;
            canvas.SetActive(false);
            CreateInstance();
        }

        private void CreateInstance()
        {
            if (!instance)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                WriteSpecifications();
                StartTest();
            }
            else
            {
                StartTest();
                Destroy(gameObject);
            }
        }

        void StartTest()
        {
            //if (DiagnosisState.changeEveryScene)
            {
                if (DiagnosisState.iteration > 0)
                {
                    if (DiagnosisState.drawCallsDG.isActiveForTesting && DiagnosisState.drawCallsDG.isEnabled)
                        SceneManager.LoadScene(DiagnosisState.drawCallsDG.scene);
                    else if (DiagnosisState.vertsTrisDG.isActiveForTesting && DiagnosisState.vertsTrisDG.isEnabled)
                        SceneManager.LoadScene(DiagnosisState.vertsTrisDG.scene);
                    else if (DiagnosisState.skinned2DDG.isActiveForTesting && DiagnosisState.skinned2DDG.isEnabled)
                        SceneManager.LoadScene(DiagnosisState.skinned2DDG.scene);
                    else if (DiagnosisState.skinned3DDG.isActiveForTesting && DiagnosisState.skinned3DDG.isEnabled)
                        SceneManager.LoadScene(DiagnosisState.skinned3DDG.scene);
                    else
                    {
                        DiagnosisState.iteration--;
                        EnableAllTest();
                        StartTest();
                    }
                }
                else
                {
                    DiagnosisState.diagnosisIsDone = true;
                }
            }
            //else
            //{
            //    DiagnosisState.iteration--;

            //    if (DiagnosisState.drawCallsDG.isActiveForTesting && DiagnosisState.drawCallsDG.isEnabled  && DiagnosisState.iteration >= 0)
            //    {
            //        RestartNextTest(DiagnosisState.drawCallsDG);
            //        SceneManager.LoadScene(DiagnosisState.drawCallsDG.scene);
            //    }
            //    else if (DiagnosisState.vertsTrisDG.isActiveForTesting && DiagnosisState.vertsTrisDG.isEnabled && DiagnosisState.iteration >= 0)
            //    {
            //        RestartNextTest(DiagnosisState.vertsTrisDG);
            //        SceneManager.LoadScene(DiagnosisState.vertsTrisDG.scene);
            //    }
            //    else
            //    {
            //        DiagnosisState.diagnosisIsDone = true;
            //    }
            //}
        }

        private void TestChangingEveryScene()
        {

        }

        private void WriteSpecifications()
        {
            DiagnosisState.log =
                "Model:\t\t" + SystemInfo.deviceModel + "\n\n" +
                "Graphic card:\t" + SystemInfo.graphicsDeviceName + "\n" +
                "Graphics version:\t" + SystemInfo.graphicsDeviceVersion + "\n" +
                "Graphics memory:\t" + SystemInfo.graphicsMemorySize.ToString() + "\n" +
                "O.S.:\t\t" + SystemInfo.operatingSystem + "\n" +
                "O.S. Family:\t" + SystemInfo.operatingSystemFamily.ToString() + "\n" +
                "Processor:\t" + SystemInfo.processorType + "\n" +
                "Cores:\t\t" + SystemInfo.processorCount.ToString() + "\n\n" +
                "60 FPS Limit:\t" + DiagnosisState.downLimit60 + "\n" +
                (DiagnosisState.activate30Limit ? "30 FPS Limit:\t" + DiagnosisState.downLimit30 : "30 FPS Test not activated") + "\n\n" +
                "=======================================";
        }

        private void EnableAllTest()
        {
            DiagnosisState.drawCallsDG.isEnabled = true;
            DiagnosisState.vertsTrisDG.isEnabled = true;
            DiagnosisState.skinned2DDG.isEnabled = true;
            DiagnosisState.skinned3DDG.isEnabled = true;
        }

        private void RestartNextTest(Diagnosi test)
        {
            if (DiagnosisState.iteration > 0) return;

            DiagnosisState.iteration = DiagnosisState.iterations;
            test.isEnabled = false;
        }

        private void Update()
        {
            if (DiagnosisState.diagnosisIsDone)
            {
                string log = DiagnosisState.log;
                log += DiagnosisState.drawCallsDG.log;
                log += DiagnosisState.vertsTrisDG.log;
                log += DiagnosisState.skinned2DDG.log;
                log += DiagnosisState.skinned3DDG.log;

                this.log.text = log;
                canvas.SetActive(true);

                if (DiagnosisState.saveLog && !logHasBeenShown)
                {
                    logHasBeenShown = true;
                    SaveLog(log);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(gameObject);
            }
        }

        private void SaveLog(string log)
        {
            DiagnosisFile.OpenFile();
            DiagnosisFile.WriteInFile(log);
            DiagnosisFile.CloseFile();
        }
    }
}