using UnityEngine;

namespace SP.Tools.StressTest
{
    public class VertTrisDiagnosis : DiagnosisBase
    {
        public override void Start()
        {
            base.Start();

            diagnosi = DiagnosisState.vertsTrisDG;
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
            if (DiagnosisState.vertsTrisDG.value60.x == 0)
            {
                Vector3 value60 = DiagnosisState.vertsTrisDG.value60;
                value60.x = SpawnerCallsTrisVerts.Instance.GetVertices();
                value60.y = SpawnerCallsTrisVerts.Instance.GetTriangles();
                DiagnosisState.vertsTrisDG.value60 = value60;
            }
        }

        public override void PrintHeader()
        {
            if (diagnosi.firstTime)
            {
                diagnosi.firstTime = false;
                diagnosi.log = "\n\nTEST VERTS & TRIS\n" + DiagnosisState.simpleSeparation;
            }
        }

        public override void PrintIterarion()
        {
            if (DiagnosisState.activate30Limit)
            {
                diagnosi.log += string.Format("Iteration:{0,3}  {1,2}fps  Verts:{2,11}  Tris:{3,11}     {4,2}fps  Verts:{5,11}  Tris:{6,11}\n",
                    DiagnosisState.iteration,
                    DiagnosisState.downLimit60,
                    SetFormat(DiagnosisState.vertsTrisDG.value60.x),
                    SetFormat(DiagnosisState.vertsTrisDG.value60.y),
                    DiagnosisState.downLimit30,
                    SetFormat(SpawnerCallsTrisVerts.Instance.GetVertices()),
                    SetFormat(SpawnerCallsTrisVerts.Instance.GetTriangles()));

                DiagnosisState.numberOfVertices60.Add((int)DiagnosisState.vertsTrisDG.value60.x);
                DiagnosisState.numberOfTriangles60.Add((int)DiagnosisState.vertsTrisDG.value60.y);
                DiagnosisState.numberOfVertices30.Add(SpawnerCallsTrisVerts.Instance.GetVertices());
                DiagnosisState.numberOfTriangles30.Add(SpawnerCallsTrisVerts.Instance.GetTriangles());
            }
            else
            {
                diagnosi.log += string.Format("Iteration:{0,3}  {1,2}fps  Verts:{2,11}  Tris:{3,11}\n", 
                    DiagnosisState.iteration,
                    DiagnosisState.downLimit60,
                    SetFormat(SpawnerCallsTrisVerts.Instance.GetVertices()), 
                    SetFormat(SpawnerCallsTrisVerts.Instance.GetTriangles()));

                DiagnosisState.numberOfVertices60.Add(SpawnerCallsTrisVerts.Instance.GetVertices());
                DiagnosisState.numberOfTriangles60.Add(SpawnerCallsTrisVerts.Instance.GetTriangles());
            }
        }

        public override void PrintAverage()
        {
            if (DiagnosisState.iteration == 1)
            {
                if (DiagnosisState.activate30Limit)
                {
                    diagnosi.log += string.Format("\nAVERAGE        {0,2}fps  Verts:{1,11}  Tris:{2,11}     {3,2}fps  Verts:{4,11}  Tris:{5,11}\n{6}",
                        DiagnosisState.downLimit60,
                        GetAverage(DiagnosisState.numberOfVertices60),
                        GetAverage(DiagnosisState.numberOfTriangles60),
                        DiagnosisState.downLimit30,
                        GetAverage(DiagnosisState.numberOfVertices30),
                        GetAverage(DiagnosisState.numberOfTriangles30),
                        DiagnosisState.largeSeparation);
                }
                else
                {
                    diagnosi.log += string.Format("\nAVERAGE        {0,2}fps  Verts:{1,11}  Tris:{2,11}\n{3}",
                        DiagnosisState.downLimit60,
                        GetAverage(DiagnosisState.numberOfVertices60),
                        GetAverage(DiagnosisState.numberOfTriangles60),
                        DiagnosisState.largeSeparation);
                }
            }
        }

        public override void ResetDiagnosi()
        {
            DiagnosisState.vertsTrisDG.isEnabled = false;
            DiagnosisState.vertsTrisDG.value60 = Vector3.zero;
        }
    }
}