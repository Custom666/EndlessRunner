﻿using System;
using UnityEngine;

namespace Assets.Planet.Scripts
{
    public class PlanetRotation : MonoBehaviour
    {
        public float Deflection = 15;
        public float Speed = 5;

        public bool CanRotate = true;

        private float _horizontalMove;

        private void Update()
        {
            if (!CanRotate) return;

            var horizontal = Input.GetAxis("Horizontal").CompareTo(0f);

            if (Input.GetButtonDown("Horizontal")) _horizontalMove = Mathf.Clamp(_horizontalMove + horizontal, -1, 1);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (!CanRotate) return;

            var z = Mathf.Clamp(_horizontalMove * Deflection, -90, 90);
            
            transform.localEulerAngles = new Vector3
                (
                    0, 
                    0, 
                    Mathf.LerpAngle(transform.localEulerAngles.z, z, Time.deltaTime * Speed)
                );
        }
    }
}
