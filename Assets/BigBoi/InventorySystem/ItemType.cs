using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.InventorySystem
{
    [CreateAssetMenu(menuName = "BigBoi/Inventory System/Item", fileName = "New Item Type")]
    public class ItemType : ScriptableObject
    {
        #region Item Details
        [Header("Item Details")]
        [SerializeField, Tooltip("Display name of item type.\nie 'Sword'.")]
        private string displayName = "Generic Item";
        /// <summary> Display name for this item type. ie 'Sword' </summary>
        public string Name => displayName;

        [SerializeField, Tooltip("Display description for this item type.")]
        private string description = "";
        /// <summary>  </summary>
        public string Description => description;
        #endregion

        #region Category Details
        [Header("Category Details")]
        [SerializeField, Tooltip("What category does this item belong to?")]
        private Category category;

        [SerializeField, Tooltip("Unique ID for this item type. Keep within 3 digits in length.")]
        private int itemID = 0;
        /// <summary> Category ID followed by item ID. ie '1002' refers to item 2 from category 1 </summary>
        public int ID { get => category.ID * 1000 + itemID; }
        #endregion

        #region Measurement Details
        [Header("Measurement Details")]
        [SerializeField, Tooltip("How should this item type be counted?")]
        private MeasureType measurement;
        /// <summary> How the amount of this item type is measured. </summary>
        public MeasureType Measurement => measurement;

        [SerializeField, Tooltip("What units is this item type measured in? Display only.")]
        private string units, oneUnit, multipleUnits;
        /// <summary> Get display units of measurement based on measurement type and count of item. </summary>
        public string Units(float _amount = 0)
        {
            switch (measurement)
            {
                case MeasureType.SingleOnly:
                    return name;

                case MeasureType.NumberCount:
                    if (_amount == 1)
                        return oneUnit;
                    else return multipleUnits;

                case MeasureType.FractionalAmount:
                    return units;

                default:
                    return oneUnit;
            }
        }

        [SerializeField, Tooltip("Maximum stack size.")]
        private int stack;
        public int Stack => stack;
        #endregion

        #region Usage Details
        [Header("Usage Details")]
        [SerializeField, Tooltip("What actions can be taken with this item type?")]
        private AllowableActions possibleActions;
        public AllowableActions PossibleActions => possibleActions;
        /// <summary> Actions available to items. </summary>
        [Serializable]
        public struct AllowableActions
        {
            public bool eat;
            public bool drink;
            public bool use;
            public bool combine;
            public bool equip;
        }
        #region Usage Specifics
        [Header("Usage Specifics")]
        [SerializeField, Tooltip("Effect type of eating or drinking this item. Value should be positive even when harmful.")]
        private EdibleEffect edibleEffectType;

        [SerializeField, Tooltip("Value of effect.")]
        private float effectValue;
        /// <summary> Returns signed value of effect. </summary>
        public float ConsumeEffectValue
        {
            get
            {
                switch (edibleEffectType)
                {
                    case EdibleEffect.Heal:
                        return effectValue;
                    case EdibleEffect.Harm:
                        return -effectValue;
                }
                return 0;
            }
        }


        #endregion
        #endregion


        #region Usage Methods

        #endregion
    }

    /// <summary> Ways of measuring items. </summary>
    public enum MeasureType
    {
        [Tooltip("This item type cannot stack.")]
        SingleOnly,

        [Tooltip("This item type is counted in whole items.")]
        NumberCount,

        [Tooltip("This item type is counted in float amounts.")]
        FractionalAmount,
    }
    
    /// <summary> Effect type of eating or drinking an item. </summary>
    public enum EdibleEffect
    {
        Inedible,
        Heal,
        Harm,
        Other,
    }
}