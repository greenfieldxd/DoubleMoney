using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class StackComponent : MonoBehaviour
    {
        [SerializeField] [BoxGroup("Stack settings")] private Vector3 offset = new Vector3(0.5f, 0.2f, 0.5f);
        [SerializeField] [BoxGroup("Stack settings")] private Vector3 rotation;
        [SerializeField] [BoxGroup("Stack settings")] private int maxHorizontalCount = 4;
        [SerializeField] [BoxGroup("Stack settings")] private int maxForwardCount = 4;
        [SerializeField] [BoxGroup("Stack settings")] private bool addOffset;
        
        private Stack<Transform> stack = new Stack<Transform>();
        private List<PosInStack> positionsInStack = new List<PosInStack>();
        
        private float currentXPosition;
        private float currentYPosition;
        private float currentZPosition;
        private int horizontalCount;
        private int forwardCount;

        public event Action OnStackChanged;
        public int Capacity { get; private set; }
        public int ItemsCount => stack.Count;

        private void Start()
        {
            InitStack();
        }

        public void InitStack(int value = 100)
        {
            Capacity = value;

            for (int i = 0; i < Capacity; i++)
            {
                positionsInStack.Add(new PosInStack(new Vector3(currentXPosition, currentYPosition, currentZPosition)));
                CalculateNextPosition();
            }
        }

        public void  PushToStackWithJump(Transform item, Action onComplete = null)
        {
            if (stack.Count >= Capacity) return;
            
            stack.Push(item);
            OnStackChanged?.Invoke();
            AnimationExtension.JumpAnim(item, transform, positionsInStack[stack.Count - 1].Position, 1f, rotation, 0.45f, onComplete);
        }
        
        public void PushToStack(Transform item, float duration = 0.45f, Action onComplete = null)
        {
            if (stack.Count >= Capacity) return;
            
            stack.Push(item);
            OnStackChanged?.Invoke();
            AnimationExtension.MoveAnim(item, transform, positionsInStack[stack.Count - 1].Position, 1f, rotation, duration, onComplete);
        }
        

        public Transform PopFromStack()
        {
            if (stack.Count == 0) return null;

            var item = stack.Pop();
            OnStackChanged?.Invoke();
            return item;
        }

        private void CalculateNextPosition()
        {
            currentXPosition += offset.x;
            horizontalCount++;

            if (horizontalCount == maxHorizontalCount)
            {
                currentZPosition += offset.z;
                forwardCount++;
                horizontalCount = 0;
                currentXPosition = 0;
                
                if (addOffset) currentYPosition += offset.y;

                if (forwardCount == maxForwardCount)
                {
                    currentYPosition += offset.y;
                    currentZPosition = 0;
                    forwardCount = 0;
                }
            }
        }

        public Transform[] GetItemsFromStack()
        {
            var array = stack.ToArray();
            stack.Clear();
            return array;
        }
        
        [Serializable]
        public class PosInStack
        {
            public Vector3 Position { get; set; }

            public PosInStack(Vector3 position)
            {
                Position = position;
            }
        }
    }
}