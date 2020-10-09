using Modules.Interfaces;
using Ui;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Architecture.Main.Dots
{
    public class DotsPositionCreator : IBehaviorSync
    {
        private const string targetTag = "Target";
        
        private readonly DotsHolder dotsHolder;
        private readonly Camera camera;
        
        private bool touched;

        private bool HasTouch
        {
            get
            {
#if UNITY_EDITOR
                return Input.GetMouseButton(0);
#else
                return Input.touchCount > 0;
#endif
            }
        }

        public DotsPositionCreator(DotsHolder dotsHolder)
        {
            this.dotsHolder = dotsHolder;
            camera = Camera.main;
            
            Object.FindObjectOfType<ButtonPause>().TouchedActions.Add(()=>touched = false);
        }

        public void Update()
        {
            if (HasTouch && !touched)
            {
                touched = true;
            }
            else if (!HasTouch && touched)
            {
                TouchedProcess();
                touched = false;
            }
        }

        private void TouchedProcess()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            if (EventSystem.current.currentSelectedGameObject != null)
                return;
                
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit)) 
                return;
            
            if (!hit.collider.CompareTag(targetTag))
                return;
                
            dotsHolder.AddDot(hit.point);
        }
    }
}
