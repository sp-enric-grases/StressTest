using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SP.Tools.StressTest
{
    [Serializable]
    public class Diagnosi
    {
        public bool isActiveForTesting;
        public string scene;
        public string log;
        public bool isEnabled;
        public Vector3 value60;
        [HideInInspector]
        public bool firstTime;
    }

    public class SetDiagnosisParams : MonoBehaviour
    {
        public Toggle drawCalls;
        public Toggle vertsTris;
        public Toggle skinning2D;
        public Toggle skinning3D;
        public TMP_InputField cooldown;
        public TMP_InputField iterations;
        public TMP_InputField threshold;
        public TMP_InputField downLimit60FPS;
        public Toggle activate30FPS;
        public TMP_InputField downLimit30FPS;
        public Toggle changeEveryScene;
        public Toggle saveLog;
        public TMP_InputField nameLog;

        private void Awake()
        {
            EnableDownLimitTo30();
            EnableLogFile();
        }

        public void EnableDownLimitTo30()
        {
            downLimit30FPS.interactable = activate30FPS.isOn;
        }

        public void EnableLogFile()
        {
            nameLog.interactable = saveLog.isOn;
        }

        public void SetParameters()
        {
            DiagnosisState.cooldown = (float)Convert.ToInt32(cooldown.textComponent.text) / 1000;
            DiagnosisState.iterations = Convert.ToInt32(iterations.textComponent.text);
            DiagnosisState.iteration = DiagnosisState.iterations;
            DiagnosisState.threshold = (float)Convert.ToInt32(threshold.textComponent.text) / 100;
            DiagnosisState.downLimit60 = Convert.ToInt32(downLimit60FPS.textComponent.text);
            DiagnosisState.activate30Limit = activate30FPS.isOn;
            DiagnosisState.downLimit30 = Convert.ToInt32(downLimit30FPS.textComponent.text);
            DiagnosisState.changeEveryScene = changeEveryScene.isOn;
            DiagnosisState.saveLog = saveLog.isOn;
            DiagnosisState.nameLog = nameLog.textComponent.text;
            
            DiagnosisState.drawCallsDG = new Diagnosi()
            {
                isActiveForTesting = drawCalls.isOn,
                scene = "SampleDrawCalls",
                log = string.Empty,
                isEnabled = true,
                value60 = Vector3.zero,
                firstTime = true
            };

            DiagnosisState.vertsTrisDG = new Diagnosi()
            {
                isActiveForTesting = vertsTris.isOn,
                scene = "SampleTrisVerts",
                log = string.Empty,
                isEnabled = true,
                value60 = Vector3.zero,
                firstTime = true
            };

            DiagnosisState.skinned2DDG = new Diagnosi()
            {
                isActiveForTesting = skinning2D.isOn,
                scene = "SampleSpine",
                log = string.Empty,
                isEnabled = true,
                value60 = Vector3.zero,
                firstTime = true
            };

            DiagnosisState.skinned3DDG = new Diagnosi()
            {
                isActiveForTesting = skinning3D.isOn,
                scene = "SampleSkinnedMesh",
                log = string.Empty,
                isEnabled = true,
                value60 = Vector3.zero,
                firstTime = true
            };

            DiagnosisState.numberOfDrawCalls60 = new List<int>();
            DiagnosisState.numberOfDrawCalls30 = new List<int>();
            DiagnosisState.numberOfVertices60 = new List<int>();
            DiagnosisState.numberOfTriangles60 = new List<int>();
            DiagnosisState.numberOfVertices30 = new List<int>();
            DiagnosisState.numberOfTriangles30 = new List<int>();
            DiagnosisState.numberOf2DModels60 = new List<int>();
            DiagnosisState.numberOf2DBones60 = new List<int>();
            DiagnosisState.numberOf2DAnimations60 = new List<int>();
            DiagnosisState.numberOf2DModels30 = new List<int>();
            DiagnosisState.numberOf2DBones30 = new List<int>();
            DiagnosisState.numberOf2DAnimations30 = new List<int>();
            DiagnosisState.numberOf3DModels60 = new List<int>();
            DiagnosisState.numberOf3DBones60 = new List<int>();
            DiagnosisState.numberOf3DAnimations60 = new List<int>();
            DiagnosisState.numberOf3DModels30 = new List<int>();
            DiagnosisState.numberOf3DBones30 = new List<int>();
            DiagnosisState.numberOf3DAnimations30 = new List<int>();
        }
    }
}
