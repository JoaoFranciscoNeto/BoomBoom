using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StarFighter
{
    [RequireComponent(typeof(BaseFighter))]
    public class PlayerFighter : MonoBehaviour
    {
        private BaseFighter _baseFighter;

        public float Throttle;
        public float Pitch;
        public float Yaw;
        public float Roll;
        public float Strafe;

        public const KeyCode ThrottleIncrease = KeyCode.W;
        public const KeyCode ThrottleDecrease = KeyCode.S;
        public const float ThrottleSensitivity = .5f;

        public float DeadZone = .1f;

        private Transform body;

        void Start()
        {
            body = transform.Find("Body").transform;

            _baseFighter = gameObject.GetComponent<BaseFighter>();

            Cursor.lockState = CursorLockMode.Confined;
        }

        void Update()
        {
            RotateInput();
            ThrustInput();

            _baseFighter.Thrust(Throttle, Strafe);
            _baseFighter.Rotate(Pitch,Yaw,0);
        }

        private void RotateInput()
        {
            var mousePos = Input.mousePosition;

            mousePos.y = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
            mousePos.x = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

            if (mousePos.magnitude <= DeadZone)
            {
                Pitch = 0;
                Yaw = 0;
            }
            else
            {
                Pitch = -Mathf.Clamp(mousePos.y, -1.0f, 1.0f);
                Yaw = -Mathf.Clamp(mousePos.x, -1.0f, 1.0f);
            }
            
            var currentAngle = Vector3.SignedAngle(transform.up, body.up, transform.forward);
            var desiredAngle = Mathf.Lerp(-45, 45, (Yaw / 2) + .5f);

            var rotationAngle = desiredAngle - currentAngle;

            body.Rotate(0,0,Mathf.Lerp(0,rotationAngle,Time.deltaTime*20f));
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
