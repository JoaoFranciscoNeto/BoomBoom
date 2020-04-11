using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StarFighter
{
    [RequireComponent(typeof(BaseFighter))]
    public class PlayerFighter : FighterInput
    {

        [Header("Responsiveness")]
        [Tooltip("Sensitivity in the pitch axis.\n\nIt's best to play with this value until you can get something the results in full input when at the edge of the screen.")]
        [SerializeField] private float pitchSensitivity = 2.5f;
        [Tooltip("Sensitivity in the yaw axis.\n\nIt's best to play with this value until you can get something the results in full input when at the edge of the screen.")]
        [SerializeField] private float yawSensitivity = 2.5f;
        [Tooltip("Sensitivity in the roll axis.\n\nTweak to make responsive enough.")]
        [SerializeField] private float rollSensitivity = 1f;

        public const KeyCode ThrottleIncrease = KeyCode.W;
        public const KeyCode ThrottleDecrease = KeyCode.S;
        public const float ThrottleSensitivity = .5f;

        [SerializeField] private float bankLimit = 35f;

        public float DeadZone = .1f;

        void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        void Update()
        {
            Throttle = 1f;

            RotationInput();
        }

        private void RotationInput()
        {
            var targetPoint = GetTargetPoint();

            var localTargetPoint = transform.InverseTransformVector(targetPoint - transform.position).normalized;
            var angleOff = Vector3.Angle(transform.forward, targetPoint - transform.position);

            var rollToTarget = -Mathf.Clamp(localTargetPoint.x, -1f, 1f);
            var rollToLevel = -transform.right.y;
            var levelInfluence = Mathf.InverseLerp(0f, bankLimit, angleOff);

            Pitch = Mathf.Clamp(-localTargetPoint.y * pitchSensitivity, -1f, 1f);
            Yaw = Mathf.Clamp(-localTargetPoint.x * yawSensitivity, -1f, 1f);
            Roll = Mathf.Lerp(rollToLevel, rollToTarget, levelInfluence);
        }

        private Vector3 GetTargetPoint()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 1000f;
            var targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            return targetPos;
        }
    }
}
