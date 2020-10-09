using Architecture.Main.Dots;
using Modules;
using Modules.Interfaces;
using UnityEngine;

namespace Architecture.Components
{
    public class DotsLineRenderer : IBehaviorSync
    {
        private const string lineName ="Line";
        private const float zPosition = 1;
        
        private readonly Player player;
        private readonly DotsHolder dotsHolder;
        private readonly LineRenderer lineRenderer;

        public DotsLineRenderer(Player player, DotsHolder dotsHolder)
        {
            this.player = player;
            this.dotsHolder = dotsHolder;
            lineRenderer = SceneResources.GetPreparedCopy(lineName).GetComponent<LineRenderer>();
        }
        
        public void Update()
        {
            var points = new Vector3[dotsHolder.DotItemsPositions.Length + 1];
            points[0] = player.GetPosition;
            
            for (var i = 0; i < dotsHolder.DotItemsPositions.Length; i++)
            {
                points[i + 1] = dotsHolder.DotItemsPositions[i];
            }

            for (var i = 0; i < points.Length; i++)
                points[i].z = zPosition;

            lineRenderer.positionCount = points.Length;
            lineRenderer.SetPositions(points);
        }
    }
}
