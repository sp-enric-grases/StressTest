using UnityEngine;

namespace SP.Tools.StressTest
{
    public class ModelsSkinnedDiagnosis : DiagnosisBase
    {
        public override void Start()
        {
            base.Start();

            diagnosi = DiagnosisState.skinned3DDG;
            StarTest();
        }

        public override void FirstPass()
        {
            SpawnerModels.Instance.AddModel();
            StartCoroutine(Test());
        }

        public override void SecondPass()
        {
            SetFirstPassValue();
            SpawnerModels.Instance.AddModel();
            StartCoroutine(Test());
        }

        public override void SetFirstPassValue()
        {
            if (DiagnosisState.skinned3DDG.value60.x == 0)
            {
                Vector3 value60 = DiagnosisState.skinned3DDG.value60;
                value60.x = SpawnerModels.Instance.GetNumModels();
                value60.y = SpawnerModels.Instance.GetBones();
                value60.z = SpawnerModels.Instance.GetNumAnims();
                DiagnosisState.skinned3DDG.value60 = value60;
            }
        }

        public override void PrintHeader()
        {
            if (diagnosi.firstTime)
            {
                diagnosi.firstTime = false;
                diagnosi.log = "\n\n3D SKINNED MODELS\n" + DiagnosisState.simpleSeparation;
            }
        }

        public override void PrintIterarion()
        {
            if (DiagnosisState.activate30Limit)
            {
                diagnosi.log += string.Format("Iteration:{0,3}   {1,2}fps  Skinned:{2,5}   Bones:{3,7}   Anims:{4,7}      {5,2}fps  Skinned:{6,5}   Bones:{7,7}   Anims:{8,7}\n",
                    DiagnosisState.iteration,
                    DiagnosisState.downLimit60,
                    DiagnosisState.skinned3DDG.value60.x,
                    SetFormat(DiagnosisState.skinned3DDG.value60.y),
                    SetFormat(DiagnosisState.skinned3DDG.value60.z),
                    DiagnosisState.downLimit30,
                    SpawnerModels.Instance.GetNumModels(),
                    SetFormat(SpawnerModels.Instance.GetBones()),
                    SetFormat(SpawnerModels.Instance.GetNumAnims()));

                DiagnosisState.numberOf3DModels60.Add((int)DiagnosisState.skinned3DDG.value60.x);
                DiagnosisState.numberOf3DBones60.Add((int)DiagnosisState.skinned3DDG.value60.y);
                DiagnosisState.numberOf3DAnimations60.Add((int)DiagnosisState.skinned3DDG.value60.z);
                DiagnosisState.numberOf3DModels30.Add(SpawnerModels.Instance.GetNumModels());
                DiagnosisState.numberOf3DBones30.Add(SpawnerModels.Instance.GetBones());
                DiagnosisState.numberOf3DAnimations30.Add(SpawnerModels.Instance.GetNumAnims());
            }
            else
            {
                diagnosi.log += string.Format("Iteration:{0,3}   {1,2}fps  Skinned:{2,5}   Bones:{3,7}   Anims:{4,7}\n", 
                    DiagnosisState.iteration,
                    DiagnosisState.downLimit60,
                    SpawnerModels.Instance.GetNumModels(),
                    SetFormat(SpawnerModels.Instance.GetBones()),
                    SetFormat(SpawnerModels.Instance.GetNumAnims()));

                DiagnosisState.numberOf3DModels60.Add(SpawnerModels.Instance.GetNumModels());
                DiagnosisState.numberOf3DBones60.Add(SpawnerModels.Instance.GetBones());
                DiagnosisState.numberOf3DAnimations60.Add(SpawnerModels.Instance.GetNumAnims());
            }
        }

        public override void PrintAverage()
        {
            if (DiagnosisState.iteration == 1)
            {
                if (DiagnosisState.activate30Limit)
                {
                    diagnosi.log += string.Format("\nAVERAGE         {0,2}fps  Skinned:{1,5}   Bones:{2,7}   Anims:{3,7}      {4,2}fps  Skinned:{5,5}   Bones:{6,7}   Anims:{7,7}\n{8}",
                        DiagnosisState.downLimit60,
                        GetAverage(DiagnosisState.numberOf3DModels60),
                        GetAverage(DiagnosisState.numberOf3DBones60),
                        GetAverage(DiagnosisState.numberOf3DAnimations60),
                        DiagnosisState.downLimit30,
                        GetAverage(DiagnosisState.numberOf3DModels30),
                        GetAverage(DiagnosisState.numberOf3DBones30),
                        GetAverage(DiagnosisState.numberOf3DAnimations30),
                        DiagnosisState.largeSeparation);
                }
                else
                {
                    diagnosi.log += string.Format("\nAVERAGE         {0,2}fps  Skinned:{1,5}   Bones:{2,7}   Anims:{3,7}\n{4}",
                        DiagnosisState.downLimit60,
                        GetAverage(DiagnosisState.numberOf3DModels60),
                        GetAverage(DiagnosisState.numberOf3DBones60),
                        GetAverage(DiagnosisState.numberOf3DAnimations60),
                        DiagnosisState.largeSeparation);
                }
            }
        }

        public override void ResetDiagnosi()
        {
            DiagnosisState.skinned3DDG.isEnabled = false;
            DiagnosisState.skinned3DDG.value60 = Vector3.zero;
        }
    }
}