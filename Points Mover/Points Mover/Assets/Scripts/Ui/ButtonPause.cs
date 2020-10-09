using System;
using System.Collections.Generic;
using Architecture.Main;
using Architecture.Main.Dots;
using Modules;
using Modules.Main;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    [RequireComponent(typeof(SVGImage))]
    public class ButtonPause : MonoBehaviour, IPointerClickHandler
    {
        public Sprite viewPlay;
        public Sprite viewPause;
        
        private bool activePause;
        private SVGImage viewComponent;

        public readonly List<Action> TouchedActions = new List<Action>();

        private void Start()
        {
            viewComponent = GetComponent<SVGImage>();

            if (viewPlay == null || viewPause == null)
            {
                DebugLog.LogRed("There`s no sprite in ButtonPause.cs");
                return;
            }

            UpdateState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            activePause = !activePause;
            UpdateState();
        }
        
        private void UpdateState()
        {
            foreach (var touchedAction in TouchedActions)
                touchedAction.Invoke();

            viewComponent.sprite = activePause ? viewPause : viewPlay;
            SyncWithBehaviour.Instance.enabled = !activePause;
        }
    }
}
