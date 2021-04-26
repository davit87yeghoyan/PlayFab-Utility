using System.Collections.Generic;
using System.IO;
using PlayFab.AdminModels;
using UnityEngine;

namespace PlayFabUtilityEditor.GenerateEnumsFiles
{
    public static class GenerateEnumsFiles
    {
        
        private static string RootFolder => GetPath();
        private static readonly string CloudScriptPath = RootFolder+"/Scripts/AutoGeneration";
        private static readonly string StatisticFile = CloudScriptPath + "/Statistic.cs";
        private static readonly string CurrencyFile = CloudScriptPath + "/Currency.cs";

        
        
        private static string GetPath()
        {
            string path = Path.GetFullPath("Packages/com.mainx.playfabutility");
            if (Directory.Exists(path))
            {
                return path;
            }
            return Application.dataPath + "/PlayFab-Utility";
        }
        
        public static void CreateStatisticFile(List<PlayerStatisticDefinition> statisticDefinitions)
        {
            List<string> lines = new List<string>()
            {
                "/* no change this file is generated*/",
                "namespace Scripts.AutoGeneration",
                "{",
                "    public enum Statistic",
                "    {",
            };
            
            foreach (var playerStatisticDefinition in statisticDefinitions)
            {
                lines.Add($"        {playerStatisticDefinition.StatisticName},");
            }
            
            lines.Add("    }");
            lines.Add("}");
            
            File.WriteAllLines(StatisticFile, lines);
        }
        
        public static void CreateCurrencyFile(List<VirtualCurrencyData> currencies)
        {
            List<string> lines = new List<string>()
            {
                "/* no change this file is generated*/",
                "namespace Scripts.AutoGeneration",
                "{",
                "    public enum Currency",
                "    {",
            };
            
            foreach (var currency in currencies)
            {
                lines.Add($"        {currency.CurrencyCode},");
            }
            
            lines.Add("    }");
            lines.Add("}");
            File.WriteAllLines(CurrencyFile, lines);
        }
    }
}