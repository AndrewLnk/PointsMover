using System;
using Architecture.Main.Dots;
using Modules;
using Modules.Interfaces;
using UnityEngine;

namespace Architecture.Components
{
    public class Player : IBehaviorSync
    {
        private const string playerObjectName = "Player";
        private const float playerSpeed = 0.065f;
        private const float removeDistance = 0.1f;
        private const float lookAtRotationSpeed = 0.15f;
        
        private readonly Transform transform;
        private readonly DotsHolder dotsHolder;
        private readonly float zPosition;

        private Quaternion targetLook;

        public Vector3 GetPosition => transform.position;
        public Player(DotsHolder dotsHolder)
        {
            transform = SceneResources.GetPreparedCopy(playerObjectName).transform;
            zPosition = transform.position.z;
            this.dotsHolder = dotsHolder;
        }

        public void Update()
        {
            TransformProcess();
            SmoothRotation();
        }

        private void TransformProcess()
        {
            var targetPosition = GetTargetPosition();

            if (targetPosition == null)
                return;

            CheckMovement(targetPosition.Value);
            CheckRemovePoint(targetPosition.Value);
            CheckLookAt(targetPosition.Value);
        }

        private void CheckMovement(Vector3 position) =>
            transform.position = Vector3.Lerp(transform.position, position, playerSpeed);

        private void CheckRemovePoint(Vector3 position)
        {
            if (Vector3.Distance(transform.position, position) > removeDistance)
                return;
            
            dotsHolder.RemoveFirst();
        }

        private void CheckLookAt(Vector3 position)
        {
            var checkRotation = transform.rotation;
            
            transform.LookAt(position);
            transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.back);
            
            if (Math.Abs(transform.localEulerAngles.y - 180) < 0.1f)
            {
                var angles = transform.localEulerAngles;
                angles.y = 0;
                angles.z = 180 - angles.z;
                transform.localEulerAngles = angles;
            }
            
            targetLook = transform.rotation;
            transform.rotation = checkRotation;
        }
        
        private void SmoothRotation() => 
            transform.rotation = Quaternion.Lerp(transform.rotation, targetLook, lookAtRotationSpeed);

        private Vector3? GetTargetPosition()
        {
            var targetPosition = dotsHolder.TargetPosition;
            
            if (targetPosition == null)
                return null;

            var position = targetPosition.Value;
            position.z = zPosition;
            
            return position;
        }
    }
}
