using System.Collections.Generic;

namespace SP.Tools.StressTest
{
    public class DiagnosisState
    {
        public static bool automaticDiagnosisIsEnabled = true;
        public static bool diagnosisIsDone;
        public static string mainScene = "AutomaticDiagnosis";
        public static string nameLog = string.Empty;
        public static float cooldown = 3.5f;
        public static int spikeWarning = 10;
        public static int iterations;
        public static int iteration;
        public static float threshold;
        public static int downLimit60 = 55;
        public static bool activate30Limit;
        public static int downLimit30 = 15;
        public static bool changeEveryScene;
        public static bool saveLog;
        public static string log = string.Empty;

        public static Diagnosi drawCallsDG;
        public static List<int> numberOfDrawCalls60;
        public static List<int> numberOfDrawCalls30;
        public static Diagnosi vertsTrisDG;
        public static List<int> numberOfVertices60;
        public static List<int> numberOfTriangles60;
        public static List<int> numberOfVertices30;
        public static List<int> numberOfTriangles30;
        public static Diagnosi skinned2DDG;
        public static List<int> numberOf2DModels60;
        public static List<int> numberOf2DBones60;
        public static List<int> numberOf2DAnimations60;
        public static List<int> numberOf2DModels30;
        public static List<int> numberOf2DBones30;
        public static List<int> numberOf2DAnimations30;
        public static Diagnosi skinned3DDG;
        public static List<int> numberOf3DModels60;
        public static List<int> numberOf3DBones60;
        public static List<int> numberOf3DAnimations60;
        public static List<int> numberOf3DModels30;
        public static List<int> numberOf3DBones30;
        public static List<int> numberOf3DAnimations30;

        public static string simpleSeparation = "--------------------------------------\n";
        public static string largeSeparation = "----------------------------------------------------------------------------\n\n";
    }
}

