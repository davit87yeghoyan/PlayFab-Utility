using System.Collections.Generic;
using System.IO;
using PlayFab.AdminModels;
using UnityEditor;
using UnityEngine;

namespace PlayFabUtilityEditor.GenerateEnumsFiles
{
    
    [InitializeOnLoad]
    public static class GenerateEnumsFiles
    {
        
        private static readonly string StatisticFile = "Assets/PlayFab-Utility/Scripts/AutoGeneration/Statistic.cs";
        private static readonly string CurrencyFile = "Assets/PlayFab-Utility/Scripts/AutoGeneration/Currency.cs";


        static GenerateEnumsFiles()
        {
           
            if (!File.Exists(StatisticFile))
            {
                CreateStatisticFile(new List<PlayerStatisticDefinition>());
            }
            
            if (!File.Exists(CurrencyFile))
            {
                CreateCurrencyFile(new List<VirtualCurrencyData>());
            }
            
        }

        public static void CreateStatisticFile(List<PlayerStatisticDefinition> statisticDefinitions)
        {
            CreateDirectory();
           
            List<string> lines = new List<string>()
            {
                "/* no change this file is generated*/",
                "namespace PlayFabUtility.AutoGeneration",
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
            CreateDirectory();
            List<string> lines = new List<string>()
            {
                "/* no change this file is generated*/",
                "namespace PlayFabUtility.AutoGeneration",
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

        
        
        
        
        private static void CreateDirectory()
        {
            if (!AssetDatabase.IsValidFolder("Assets/PlayFab-Utility"))
            {
                AssetDatabase.CreateFolder("Assets","PlayFab-Utility");
            }

            if (!AssetDatabase.IsValidFolder("Assets/PlayFab-Utility/Scripts"))
            {
                AssetDatabase.CreateFolder("Assets/PlayFab-Utility","Scripts");
            }

            if (!AssetDatabase.IsValidFolder("Assets/PlayFab-Utility/Scripts/AutoGeneration"))
            {
                AssetDatabase.CreateFolder("Assets/PlayFab-Utility/Scripts","AutoGeneration");
            }
        }
    }
}