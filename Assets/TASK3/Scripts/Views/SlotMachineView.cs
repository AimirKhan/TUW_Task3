using System;
using System.Collections.Generic;
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
        [SerializeField] private ParticleSystem winParticleSystem;
        
        public RectTransform ItemsContainer => itemsContainer;
        public SlotItemView SlotItemPrefab => slotItemPrefab;
        
        [Header("Animation values")]
        [SerializeField] private float maxVelocity = 3000f;
        [SerializeField] private float timeToStop = 3f;
        
        private bool isMoving;
        private float currentVelocity;
        private Vector2 startPosition;
        private float elementHeight;
        private List<SlotItemView> totalItems;
        private SlotItemView winSlotItem;
        private int totalUniqueItems;
        private float fullCycleHeight;

        [OnAwake]
        private void OnAwake()
        {
            elementHeight = itemsContainer.rect.size.y;
        }

        [OnStart]
        private void OnStart()
        {
            try
            {
                totalItems = Model.Get("SlotItems") as List<SlotItemView>;
                totalUniqueItems = totalItems.Count - 2;
                fullCycleHeight = elementHeight * totalUniqueItems;
            }
            catch
            {
                Log.Error("Model.Get SlotItems Slot items are empty");
                throw;
            }
        }
        
        [Bind("RollItems")]
        private void StartRollItems()
        {
            isMoving = true;
            Path = new CPath()
                .EasingCubicEaseIn(1f, 0f, maxVelocity, v => currentVelocity = v);
        }

        [Bind("StopRollItems")]
        private void StopRollItems()
        {
            isMoving = false;
            var targetWinId = 0;
            try
            {
                winSlotItem = Model.Get("WinItem") as SlotItemView;
                targetWinId = winSlotItem.SlotItem.Id;
            }
            catch
            {
                Log.Error("Model.Get WinItem Slot items are empty");
                throw;
            }
            
            var currentY = itemsContainer.anchoredPosition.y % fullCycleHeight;
            if (currentY < 0) currentY += fullCycleHeight;
            
            var targetDestinationY = targetWinId * elementHeight;
            
            var distanceToTarget = targetDestinationY - currentY;
            if (distanceToTarget <= 0) distanceToTarget += fullCycleHeight;
            
            var totalDistance = distanceToTarget + (fullCycleHeight * 2);
            
            var timeToStop = (3f * totalDistance) / currentVelocity;
            
            var lastF = 0f;
            
            Path = new CPath()
                .EasingCubicEaseOut(timeToStop, 0f, totalDistance, f =>
                {
                    var delta = f - lastF;
                    lastF = f;
                    
                    var position = itemsContainer.anchoredPosition;
                    position.y += delta;
                    position.y %= fullCycleHeight;
                    itemsContainer.anchoredPosition = position;
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
                position.y += currentVelocity * Time.deltaTime;
                itemsContainer.anchoredPosition = position;
                
                position.y %= fullCycleHeight;
                
                itemsContainer.anchoredPosition = position;
            }
        }
        [Bind("PlayWinAnim")]
        private void PlayWinAnimation()
        {
            winParticleSystem.textureSheetAnimation.SetSprite(0, winSlotItem.SlotItem.Image);
            winParticleSystem.Play();
        }
    }
}
