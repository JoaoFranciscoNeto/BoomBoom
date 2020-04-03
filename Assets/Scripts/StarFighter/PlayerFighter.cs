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
        private BaseFighter _baseFighter;

        public float Strafe;

        public const KeyCode ThrottleIncrease = KeyCode.W;
        public const KeyCode ThrottleDecrease = KeyCode.S;
        public const float ThrottleSensitivity = .5f;

        public float DeadZone = .1f;

        void Start()
        {
            _baseFighter = gameObject.GetComponent<BaseFighter>();

            Cursor.lockState = CursorLockMode.Confined;
        }

        void Update()
        {
            RotationInput();
            ThrustInput();
        }

        private void RotationInput()
        {
            var mousePos = Input.mousePosition;

            mousePos.y = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
            mousePos.x = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);
            Pitch = -Mathf.Clamp(mousePos.y, -1.0f, 1.0f);
            Yaw = -Mathf.Clamp(mousePos.x, -1.0f, 1.0f);

            RotationDeadZone();
        }

        private void RotationDeadZone()
        {
            Pitch = Math.Abs(Pitch) < DeadZone ? 0 : Pitch;
            Yaw = Math.Abs(Yaw) < DeadZone ? 0 : Yaw;

            Debug.Log(Pitch + " " + Yaw);
        }

        private void ThrustInput()
        {
            Strafe = Input.GetAxis("Horizontal");

            var target = Throttle;

            if (Input.GetKey(ThrottleIncrease))
                target = 1.0f;
            else if (Input.GetKey(ThrottleDecrease))
                target = 0.0f;

            Throttle = Mathf.MoveTowards(Throttle, target, Time.deltaTime * ThrottleSensitivity);
        }
    }
}
