using Modules;
using Modules.Interfaces;
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

        public DotsPositionCreator(DotsHolder dotsHolder)
        {
            this.dotsHolder = dotsHolder;
            camera = Camera.main;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && !touched)
            {
                Touched();
                touched = true;
            }
            else if (!Input.GetMouseButtonDown(0) && touched)
            {
                touched = false;
            }
        }

        private void Touched()
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
