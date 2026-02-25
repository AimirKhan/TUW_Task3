using TASK3.Scripts.Models.SO;
using UnityEngine;
using UnityEngine.UI;

namespace TASK3.Scripts.Views
{
    public class SlotItemView : MonoBehaviour
    {
        [SerializeField] private Image itemImage;

        public SlotItem SlotItem;

        public void Init()
        {
            itemImage.sprite = SlotItem.Image;
        }
    }
}
