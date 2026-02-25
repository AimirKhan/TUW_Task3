using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Model;
using TASK3.Scripts.Models.SO;
using TASK3.Scripts.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TASK3.Scripts.Controllers
{
    public class SlotMachineController : MonoBehaviourExtBind
    {
        [SerializeField] private SlotMachineView view;
        [SerializeField] private SlotItemsSo slotItemsSo;
        
        private List<SlotItemView> slotItems = new();
        
        [OnAwake]
        public void OnAwake()
        {
            slotItemsSo.SlotItems.ForEach(item =>
            {
                InstantiateNewItems(item);
            });
            if (slotItemsSo.SlotItems.Capacity >= 2)
            {
                for (int index = 0; index < 2; index++)
                {
                    InstantiateNewItems(slotItemsSo.SlotItems[index]);
                }
            }
            
            Model.Add("SlotItems", slotItems);
            return;
            
            void InstantiateNewItems(SlotItem itemContext)
            {
                var newSlotItem = Instantiate(view.SlotItemPrefab, view.ItemsContainer);
                newSlotItem.SlotItem = itemContext;
                newSlotItem.Init();
                slotItems.Add(newSlotItem);
            }
        }

        [Bind("SetWinItem")]
        public void SetWinItem()
        {
            Model.Set("WinItem", slotItems[Random.Range(0, slotItemsSo.SlotItems.Count)]);
            var item = Model.Get("WinItem") as SlotItemView;
            Debug.Log("Win item is " + item.SlotItem.Name);
        }
    }
}
