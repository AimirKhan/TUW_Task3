using System;
using System.Collections.Generic;
using UnityEngine;

namespace TASK3.Scripts.Models.SO
{
    [CreateAssetMenu(fileName = "SlotItems", menuName = "Scriptable Objects/SlotItems")]
    public class SlotItemsSo : ScriptableObject
    {
        [SerializeField] private List<SlotItem> slotItems;

        public List<SlotItem> SlotItems => slotItems;
    }

    [Serializable]
    public class SlotItem
    {
        public int Id;
        public string Name;
        public Sprite Image;
    }
}
