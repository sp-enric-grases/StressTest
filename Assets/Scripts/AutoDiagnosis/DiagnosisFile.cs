using System;
using System.IO;
using UnityEngine;

namespace SP.Tools.StressTest
{
    public class DiagnosisFile
    {
        public static DiagnosisFile instance;
        public string nameTest = "Test";

        private static string path;
        private static StreamWriter writer;

        public static void OpenFile()
        {
            path = Path.Combine(Application.persistentDataPath, string.Format("{0} {1}.txt", DiagnosisState.nameLog, DateTime.Now.ToString("HH.mm.ss")));
            writer = new StreamWriter(path, true);
        }

        public static void WriteInFile(string text)
        {
            writer.WriteLine(string.Format("{0}\n", text));
        }

        public static void WriteSeparation()
        {
            writer.WriteLine("───────────────────────────────\n");
        }

        public static void WriteBlankLine()
        {
            writer.WriteLine("  \n");
        }

        public static void CloseFile()
        {
            writer.Close();
        }
    }
}
