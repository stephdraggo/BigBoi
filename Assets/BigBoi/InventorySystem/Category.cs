using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.InventorySystem
{
    [CreateAssetMenu(menuName = "BigBoi/Inventory System/Category", fileName = "New Item Category")]
    public class Category : ScriptableObject
    {

        [SerializeField, Tooltip("Display name of category.\nie 'Edible'.")]
        private string displayName = "Generic Category";
        public string Name => displayName;

        [SerializeField, Tooltip("Unique ID for this category. Keep within 2 digits in length.")]
        private int id = 0;
        public int ID => id;
    }
}