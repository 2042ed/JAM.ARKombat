using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamelab
{
    // Rotates the cube in the template scene
    public class SpinningCube : MonoBehaviour
    {
        [Tooltip("Changes the rotation speed of the cube")]
        public float rotateSpeed = 1f;

        [Tooltip("Changes orientation of the cube")]
        public Vector3 objectRotation;

        void Update()
        {
            //Change the rotation (by the defined orientation * the time that has passed * defined speed)
            transform.Rotate(objectRotation * Time.deltaTime * rotateSpeed);
        }
    }
}