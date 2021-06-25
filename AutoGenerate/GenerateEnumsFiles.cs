using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PlayFab.AdminModels;
using UnityEditor;

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
                "",
                "namespace PlayFabUtility.AutoGeneration",
                "{",
                "    public enum Statistic",
                "    {",
            };
            
            foreach (var playerStatisticDefinition in statisticDefinitions)
            {
                lines.Add($"        {playerStatisticDefinition.StatisticName} = {playerStatisticDefinition.StatisticName.GetHashCode()},");
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
                "",
                "namespace PlayFabUtility.AutoGeneration",
                "{",
                "    public enum Currency",
                "    {",
            };
            
            foreach (var currency in currencies)
            {
                lines.Add($"        {currency.CurrencyCode} = {GetIntHashCode(currency.CurrencyCode)},");
            }
            
            lines.Add("    }");
            lines.Add("}");
            File.WriteAllLines(CurrencyFile, lines);
        }

        
        
        
        
        private static void CreateDirectory()
        {
            if (!Directory.Exists("Assets/PlayFab-Utility"))
            {
                Directory.CreateDirectory("Assets/PlayFab-Utility");
            }

            if (!Directory.Exists("Assets/PlayFab-Utility/Scripts"))
            {
                Directory.CreateDirectory("Assets/PlayFab-Utility/Scripts");
            }

            if (!Directory.Exists("Assets/PlayFab-Utility/Scripts/AutoGeneration"))
            {
                Directory.CreateDirectory("Assets/PlayFab-Utility/Scripts/AutoGeneration");
                Create_asmdef();
            }
        }
        
        
        private static void Create_asmdef()
        {
            string name = "PlayFabUtilityAutoGenerate";
            File.WriteAllLines( "Assets/PlayFab-Utility/Scripts/AutoGeneration/"+name+".asmdef", new List<string>()
            {
                "{\"name\": \""+name+"\"}",
            });
        }
        
        
        static int GetIntHashCode(string strText)
        {
            int hashCode = 0;
            if (!string.IsNullOrEmpty(strText))
            {
                //Unicode Encode Covering all characterset
                byte[] byteContents = Encoding.Unicode.GetBytes(strText);
                System.Security.Cryptography.SHA256 hash = 
                    new System.Security.Cryptography.SHA256CryptoServiceProvider();
                byte[] hashText = hash.ComputeHash(byteContents);
                //32Byte hashText separate
                //hashCodeStart = 0~7  8Byte
                //hashCodeMedium = 8~23  8Byte
                //hashCodeEnd = 24~31  8Byte
                //and Fold
                int hashCodeStart = BitConverter.ToInt32(hashText, 0);
                int hashCodeMedium = BitConverter.ToInt32(hashText, 8);
                int hashCodeEnd = BitConverter.ToInt32(hashText, 24);
                hashCode = hashCodeStart ^ hashCodeMedium ^ hashCodeEnd;
            }
            return (hashCode);
        }   
    }
}