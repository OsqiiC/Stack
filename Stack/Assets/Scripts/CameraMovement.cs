using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackReplica
{
    public class CameraMovement : MonoBehaviour
    {
        private int counter;
        private void Update()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            if (Input.GetMouseButtonDown(0))
            {
                counter++;
                if (counter > 3)
                {
                    transform.position += Vector3.up * GameProperties.instance.cubeScale.y;
                }
            }
        }
    }
}