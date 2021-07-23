using System;
using PlayFabUtility.AutoGeneration;
using PlayFabUtility.SerializableDictionary;
using UnityEngine;

namespace PlayFabUtility.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerStatistics", menuName = "MAINX/PlayFab Utility/Player statistics", order = 0)]
    public class PlayerStatisticsScriptableObject : ScriptableObject
    {
        [SerializeField]
        private PlayerStatistics playerStatistics;
        public PlayerStatistics PlayerStatistics => playerStatistics;
    }
    
    
    [Serializable]
    public class PlayerStatistics : SerializableDictionary<Statistic, StatisticInfo>
    {
    }
    
     
    [Serializable]
    public class StatisticInfo:ItemBase
    {
    }

}