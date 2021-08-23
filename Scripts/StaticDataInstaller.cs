using System;
using System.Linq;
using PlayFabUtility.AutoGeneration;
using PlayFabUtility.ScriptableObjects;
using Serializable_Dictionary.SerializableDictionary;

using UnityEngine;

namespace PlayFabUtility
{
    [ExecuteInEditMode]
    public class StaticDataInstaller : MonoBehaviour
    {
      
        [SerializeField]
        private VirtualCurrencyScriptableObject virtualCurrency;
        
        [SerializeField]
        private PlayerStatisticsScriptableObject playerStatistics;
        
        
        public PlayerStatistics PlayerStatistics { get; private set; }
        public VirtualCurrency VirtualCurrency { get; private set; }

        

        private void Awake()
        {
#if UNITY_EDITOR
            LoadScriptableObjects();
#endif
            SetData();
            
        }

        private void LoadScriptableObjects()
        {
            virtualCurrency = Resources.FindObjectsOfTypeAll<VirtualCurrencyScriptableObject>().First();
            playerStatistics = Resources.FindObjectsOfTypeAll<PlayerStatisticsScriptableObject>().First();
        }


        private void SetData()
        {
            if (virtualCurrency.VirtualCurrency.Count > 0)
            {
                VirtualCurrency = OrderStatistic(virtualCurrency.VirtualCurrency) as VirtualCurrency;
            }

            if (playerStatistics.PlayerStatistics.Count > 0)
            {
                PlayerStatistics = OrderStatistic(playerStatistics.PlayerStatistics) as PlayerStatistics;
            }
        }
        
        
     
        public CurrencyInfo GetCurrencyByKey(string key)
        {
            Currency currency = (Currency) Enum.Parse(typeof(Currency), key, true);
            return VirtualCurrency.ContainsKey(currency) ? VirtualCurrency[currency] : null;
        }
        
        public StatisticInfo GetPlayerStatisticsByKey(string key)
        {
            Statistic statistic = (Statistic) Enum.Parse(typeof(Statistic), key, true);
            return PlayerStatistics.ContainsKey(statistic) ? PlayerStatistics[statistic] : null;
        }
        
        private SerializableDictionary<Statistic, T1> OrderStatistic<T1>(SerializableDictionary<Statistic, T1> statistics) 
            where T1 : ItemBase
        {
            var list = statistics.ToList().OrderBy(pair => pair.Value.displayOrder);
            statistics.Clear();
            foreach (var keyValuePair in list)
            {
                statistics.Add(keyValuePair);
            }
            return statistics;
        }
        
        private SerializableDictionary<Currency, T1> OrderStatistic<T1>(SerializableDictionary<Currency, T1> currency) 
            where T1 : ItemBase
        {
            var list = currency.ToList().OrderBy(pair => pair.Value.displayOrder);
            currency.Clear();
            foreach (var keyValuePair in list)
            {
                currency.Add(keyValuePair);
            }
            return currency;
        }
    }
}