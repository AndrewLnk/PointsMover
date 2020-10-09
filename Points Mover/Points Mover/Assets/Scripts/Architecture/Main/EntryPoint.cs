using Architecture.Components;
using Architecture.Main.Dots;
using Modules.Main;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Architecture.Main
{
    public class EntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            // dots holder
            var dotsHolder = new DotsHolder();
            ModulesLocation.Instance.DotsHolder = dotsHolder;
            
            // dots creator
            var dotsPositionCreator = new DotsPositionCreator(dotsHolder);
            ModulesLocation.Instance.DotsPositionCreator = 
                SyncWithBehaviour.Instance.AddObserver(dotsPositionCreator);

            // player
            var player = new Player(dotsHolder);
            ModulesLocation.Instance.Player =
                SyncWithBehaviour.Instance.AddObserver(player);
            
            // line renderer
            var lineRenderer = new DotsLineRenderer(player, dotsHolder);
            ModulesLocation.Instance.DotsLineRenderer =
                SyncWithBehaviour.Instance.AddObserver(lineRenderer);
        }
    }
}
