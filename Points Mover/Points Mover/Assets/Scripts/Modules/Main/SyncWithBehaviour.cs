using System.Collections.Generic;
using Modules.Interfaces;
using UnityEngine;

namespace Modules.Main
{
    public class SyncWithBehaviour : MonoBehaviour
    {
        private readonly List<IBehaviorSync> items = new List<IBehaviorSync>();
        private IBehaviorSync[] syncs;

        private static SyncWithBehaviour instance;

        private readonly Dictionary<int, SyncWithBehaviourAnchor> syncAnchors = new Dictionary<int, SyncWithBehaviourAnchor>();
        
        public static SyncWithBehaviour Instance
        {
            get
            {
                if (instance == null)
                {
                    var go = SingletonCenter.Instance.gameObject;
                    instance = go.AddComponent<SyncWithBehaviour>();
                }

                return instance;
            }
        }

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

        public void AddAnchor(GameObject syncObject, int anchorKey)
        {
            if (syncAnchors.ContainsKey(anchorKey))
                return;

            var sync = syncObject.AddComponent<SyncWithBehaviourAnchor>();
            syncAnchors.Add(anchorKey, sync);
        }
        
        public T AddObserver<T>(T behavior, int key) where T: IBehaviorSync
        {
            if (syncAnchors.ContainsKey(key)) 
                return syncAnchors[key].AddObserver(behavior);
            
            Debug.Log("<color=red>No Sync Anchor</color>");
            AddObserver(behavior);
            
            return behavior;
        }

        private void Update()
        {
            for (var i = 0; i < syncs.Length; i++) items[i]?.Update();
        }
    }
}