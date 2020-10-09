using System.Collections.Generic;
using Modules.Interfaces;
using UnityEngine;

namespace Modules
{
    public class SyncWithBehaviourAnchor : MonoBehaviour
    {
        private readonly List<IBehaviorSync> items = new List<IBehaviorSync>();
        private IBehaviorSync[] syncs = new IBehaviorSync[0];
        
        public T AddObserver<T>(T behavior) where T: IBehaviorSync
        {
            if (!items.Contains(behavior))
                items.Add(behavior);
            
            syncs = items.ToArray();

            return behavior;
        }

        public void RemoveObserver(IBehaviorSync behavior)
        {
            if (items.Contains(behavior))
                items.Remove(behavior);

            syncs = items.ToArray();
        }

        private void Update()
        {
            for (var i = 0; i < syncs.Length; i++) items[i]?.Update();
        }
    }
}
