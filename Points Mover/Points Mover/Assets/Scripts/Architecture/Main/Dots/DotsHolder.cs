using System.Collections.Generic;
using System.Linq;
using Modules;
using UnityEngine;

namespace Architecture.Main.Dots
{
    public class DotsHolder
    {
        private readonly DotsPool dotsPool;
        private readonly Queue<DotItem> dotItems = new Queue<DotItem>();
        public Vector3[] DotItemsPositions { get; private set; } = new Vector3[0];

        public Vector3? TargetPosition
        {
            get
            {
                if (dotItems.Count == 0)
                    return null;

                return dotItems.Peek().Transform.position;
            }
        }

        public DotsHolder()
        {
            dotsPool = new DotsPool();
        }

        public void AddDot(Vector3 position)
        {
            position.z = 0;
            
            var dot = dotsPool.GetDot();
            dot.Transform.SetParent(null);
            dot.Transform.position = position;
            dotItems.Enqueue(dot);

            UpdateDotsPositions();
        }

        public void RemoveFirst()
        {
            if (dotItems.Count == 0)
                return;

            var firstDot = dotItems.Dequeue();
            dotsPool.SetDot(firstDot);

            UpdateDotsPositions();
        }

        private void UpdateDotsPositions()
        {
            DotItemsPositions = dotItems.ToArray().Select(e=>e.Transform.position).ToArray();
        }
    }
}
