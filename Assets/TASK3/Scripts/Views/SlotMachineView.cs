using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using UnityEngine;

namespace TASK3.Scripts.Views
{
    public class SlotMachineView : MonoBehaviourExtBind
    {
        [SerializeField] private RectTransform itemsContainer;
        [SerializeField] private SlotItemView slotItemPrefab;
        
        public RectTransform ItemsContainer => itemsContainer;
        public SlotItemView SlotItemPrefab => slotItemPrefab;
        
        [Header("Animation values")]
        [SerializeField] private float speed = 3000f;
        [SerializeField] private float timeToStop = 3f;
        
        private bool isMoving;
        private float velocity;
        private Vector2 startPosition;
        private float elementHeight;
        private int targetWinId;

        [OnAwake]
        private void OnAwake()
        {
            elementHeight = itemsContainer.rect.size.y;
        }
        
        [Bind("RollItems")]
        private void RollItems()
        {
            isMoving = true;
            Path = new CPath()
                .EasingCubicEaseIn(1f, 0f, speed, f => velocity = f);
        }

        [Bind("StopRollItems")]
        private void StopRollItems()
        {
            
            isMoving = false;
            var currentSibling = Model.Get("WinItem") as SlotItemView;
            //var currentSiblingIndex = currentSibling.transform.GetSiblingIndex();
            var currentSiblingIndex = currentSibling.SlotItem.Id ;
            Debug.Log("Current sibling index" + currentSiblingIndex);
            
            var currentY = itemsContainer.anchoredPosition.y;
            var toReset = elementHeight - currentY;
            var totalDistance = toReset + (currentSiblingIndex * elementHeight) + (elementHeight * 5 * 2);
            
            var timeToStop = (3f * totalDistance) / velocity;
            
            if (timeToStop < 1.2f) timeToStop = 1.2f;
            
            var lastF = 0f;
            
            Path = new CPath()
                .EasingCubicEaseOut(timeToStop, 0f, totalDistance, f =>
                {
                    var delta = f - lastF;
                    lastF = f;

                    var position = itemsContainer.anchoredPosition;
                    position.y += delta;
                    itemsContainer.anchoredPosition = position;

                    if (itemsContainer.anchoredPosition.y >= elementHeight)
                    {
                        itemsContainer.anchoredPosition -= new Vector2(0, elementHeight);
                        itemsContainer.GetChild(0).SetAsLastSibling();
                    }
                })
                .Action(() =>
                {
                    Settings.Fsm?.Invoke("ItemsRollStopped");
                });
        }
        
        [OnUpdate]
        private void OnUpdate()
        {
            if (isMoving)
            {
                var position = itemsContainer.anchoredPosition;
                position.y += velocity * Time.deltaTime;
                itemsContainer.anchoredPosition = position;

                if (itemsContainer.anchoredPosition.y >= elementHeight)
                {
                    itemsContainer.anchoredPosition -= new Vector2(0, elementHeight);
                    if (itemsContainer.childCount > 0)
                    {
                        itemsContainer.GetChild(0).SetAsLastSibling();
                    }
                }
            }
        }
        
        void OnGUI()
        {
            if (GUI.Button(new Rect(20, 20, 250, 100), "Show current state"))
            {
                Log.Info("Current state is " + Settings.Fsm.CurrentStateName);
            }
        }
    }
}
