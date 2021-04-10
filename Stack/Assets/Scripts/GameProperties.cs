using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StackReplica
{
    public class GameProperties : MonoBehaviour
    {      
        public GameObject cube;
        public Vector3 cubeScale;
        public Vector3 lastCubeLocalScale;
        public Vector3 lastCubePosition;

        public Vector2 movementAxis = new Vector2(0, 1);
        public float cubeSpeed = 10;
        public float movementDistance = 20;
        public float currentHeight = 0;
        public float checkPosThreshold = 0.25f;

        public static GameProperties instance;

        private void Awake()
        {
            instance = this;
            if(Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }
        }

        private void Start()
        {
            Vector3 spawnPosition = new Vector3(movementAxis.x * movementDistance, currentHeight, movementAxis.y * movementDistance);

            GameObject firstCube = Instantiate(cube, spawnPosition, Quaternion.identity);

            firstCube.GetComponent<CubeMovement>().speed = cubeSpeed;
            firstCube.transform.localScale = cubeScale;
        }
    }
}
