using UnityEngine;

namespace SP.Tools.StressTest
{
    public class DrawCallsDiagnosis : DiagnosisBase
    {
        private string drawcalls = "Drawcalls";

        public override void Start()
        {
            base.Start();

            diagnosi = DiagnosisState.drawCallsDG;
            StarTest();
        }

        public override void FirstPass()
        {
            SpawnerCallsTrisVerts.Instance.CreateObject();
            StartCoroutine(Test());
        }

        public override void SecondPass()
        {
            SetFirstPassValue();
            SpawnerCallsTrisVerts.Instance.CreateObject();
            StartCoroutine(Test());
        }

        public override void SetFirstPassValue()
        {
            if (DiagnosisState.drawCallsDG.value60.x == 0)
            {
                Vector3 value60 = DiagnosisState.drawCallsDG.value60;
                value60.x = SpawnerCallsTrisVerts.Instance.GetDrawCalls();
                DiagnosisState.drawCallsDG.value60 = value60;
            }
        }

        public override void PrintHeader()
        {
            if (diagnosi.firstTime)
            {
                diagnosi.firstTime = false;
                diagnosi.log = "\n\nTEST DRAWCALLS\n" + DiagnosisState.simpleSeparation;
            }
        }

        public override void PrintIterarion()
        {
            if (DiagnosisState.activate30Limit)
            {
                diagnosi.log += string.Format("Iteration:{0,3}   Drawcalls {1,2}fps:{2,8}    Drawcalls {3,2}fps:{4,8}\n", 
                    DiagnosisState.iteration,
                    DiagnosisState.downLimit60,
                    SetFormat(DiagnosisState.drawCallsDG.value60.x),
                    DiagnosisState.downLimit30,
                    SetFormat(SpawnerCallsTrisVerts.Instance.GetDrawCalls()));

                DiagnosisState.numberOfDrawCalls60.Add((int)DiagnosisState.drawCallsDG.value60.x);
                DiagnosisState.numberOfDrawCalls30.Add(SpawnerCallsTrisVerts.Instance.GetDrawCalls());
            }
            else
            {
                diagnosi.log += string.Format("Iteration:{0,3}   Drawcalls {1,2}fps:{2,8}\n", 
                    DiagnosisState.iteration,
                    DiagnosisState.downLimit60, 
                    SetFormat(SpawnerCallsTrisVerts.Instance.GetDrawCalls()));

                DiagnosisState.numberOfDrawCalls60.Add(SpawnerCallsTrisVerts.Instance.GetDrawCalls());
            }
        }

        public override void PrintAverage()
        {
            if (DiagnosisState.iteration == 1)
            {
                if (DiagnosisState.activate30Limit)
                {
                    diagnosi.log += string.Format("\nAVERAGE         {0,2}fps:{1,7} Drawcalls   {2,2}fps:{3,7} Drawcalls\n{4}",
                        DiagnosisState.downLimit60,
                        GetAverage(DiagnosisState.numberOfDrawCalls60),
                        DiagnosisState.downLimit30,
                        GetAverage(DiagnosisState.numberOfDrawCalls30),
                        DiagnosisState.largeSeparation);
                }
                else
                {
                    diagnosi.log += string.Format("\nAVERAGE         {0,2}fps:{1,7} Drawcalls\n{2}",
                        DiagnosisState.downLimit60,
                        GetAverage(DiagnosisState.numberOfDrawCalls60),
                        DiagnosisState.largeSeparation);
                }
            }
        }

        public override void ResetDiagnosi()
        {
            DiagnosisState.drawCallsDG.value60 = Vector3.zero;

            if (DiagnosisState.changeEveryScene)
                DiagnosisState.drawCallsDG.isEnabled = false;
        }
    }
}