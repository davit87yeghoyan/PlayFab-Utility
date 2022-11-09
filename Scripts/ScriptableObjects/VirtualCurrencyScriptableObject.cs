using System;
using Serializable_Dictionary.SerializableDictionary;
using PlayFabUtility.AutoGeneration;
using UnityEngine;
#if TERMS_POPUP_PLAYFAB_UTILITY
    using I2.Loc;
#endif

namespace PlayFabUtility.ScriptableObjects
{
    [CreateAssetMenu(fileName = "VirtualCurrency", menuName = "MAINX/PlayFab Utility/Virtual currency", order = 0)]
    public class VirtualCurrencyScriptableObject : ScriptableObject
    {
        [SerializeField]
        private VirtualCurrency virtualCurrency;
        
        public VirtualCurrency VirtualCurrency => virtualCurrency;
    }
    
    
    [Serializable]
    public class VirtualCurrency : SerializableDictionary<Currency, CurrencyInfo>
    {
    }
    
    
    [Serializable]
    public class CurrencyInfo:ItemBase
    {
    }   
    
    
    [Serializable]
    public abstract class ItemBase
    {
        public Sprite sprite;
#if TERMS_POPUP_PLAYFAB_UTILITY
        [TermsPopup]
#endif
        public string displayName;
        public string displayOrder;
    }
   
}
