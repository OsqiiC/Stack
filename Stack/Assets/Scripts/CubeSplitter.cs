using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackReplica {
    public class CubeSplitter : MonoBehaviour
    {
        public GameObject gravityCube;

        private Vector3 startScale;
        private Ray[] rays = new Ray[4];
        private GameProperties _gameProperties;

        void Start()
        {
            _gameProperties = GameProperties.instance;
            for (int i = 0; i < rays.Length; i++)
            {
                rays[i].direction = -Vector3.up/2;
            }
            startScale = transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            MoveRays();
        }

        private void MoveRays()
        {
            rays[0].origin = new Vector3(-transform.localScale.x / 2 + transform.position.x + 0.01f,
                                          transform.position.y,
                                          transform.localScale.z / 2 + transform.position.z - 0.01f);
            rays[1].origin = new Vector3( transform.localScale.x / 2 + transform.position.x - 0.01f,
                                          transform.position.y,
                                          transform.localScale.z / 2 + transform.position.z - 0.01f);
            rays[3].origin = new Vector3(-transform.localScale.x / 2 + transform.position.x + 0.01f,
                                          transform.position.y, 
                                         -transform.localScale.z / 2 + transform.position.z + 0.01f);
            rays[2].origin = new Vector3( transform.localScale.x / 2 + transform.position.x - 0.01f, 
                                          transform.position.y,
                                         -transform.localScale.z / 2 + transform.position.z + 0.01f);
        }

        public void SplitCube()
        {
            float rayDistance = transform.localScale.y;
            int signOfNumber = -1;

            // rays are showing which side of the cube must be splited
            if (Physics.Raycast(rays[3], rayDistance))
            {
                signOfNumber = 1;
            }

            // if cube moves alond Z axis 
            if (Physics.Raycast(rays[2], rayDistance) && Physics.Raycast(rays[3], rayDistance) ||
                Physics.Raycast(rays[0], rayDistance) && Physics.Raycast(rays[1], rayDistance))
            {
                // scaling kinematic cube
                transform.localScale -= Vector3.forward * Mathf.Abs(transform.position.z - _gameProperties.lastCubePosition.z);
                transform.position += -signOfNumber * Vector3.forward * Mathf.Abs(transform.position.z - _gameProperties.lastCubePosition.z) / 2;

                // creating gravity cube
                GameObject newCube = Instantiate(gravityCube, transform.position + signOfNumber * (Vector3.forward * startScale.z / 2), Quaternion.identity);
                newCube.transform.localScale = startScale - Vector3.forward * (transform.localScale.z);
            }
            // if cube moves along X axis
            else if (Physics.Raycast(rays[3], rayDistance) && Physics.Raycast(rays[0], rayDistance) ||
                     Physics.Raycast(rays[1], rayDistance) && Physics.Raycast(rays[2], rayDistance))
            {
                transform.localScale -= Vector3.right * Mathf.Abs(transform.position.x - _gameProperties.lastCubePosition.x);
                transform.position += -signOfNumber * Vector3.right * Mathf.Abs(transform.position.x - _gameProperties.lastCubePosition.x) / 2;

                GameObject newCube = Instantiate(gravityCube, transform.position + signOfNumber * (Vector3.right * startScale.x / 2), Quaternion.identity);
                newCube.transform.localScale = startScale - Vector3.right * (transform.localScale.x);
            }
        }
    }
}