using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackReplica
{
    public class CubeMovement : MonoBehaviour
    {
        [HideInInspector]
        public float speed;

        private GameProperties _gameProperties;
        int movementDirection = 1;
        private bool isDestroyed = false;
        void Awake()
        {
            _gameProperties = GameProperties.instance;
        }

        // Update is called once per frame
        void Update()
        {
            MoveCube();
            PlayerInput();
        }

        private void SpawnCube()
        {
            Vector3 spawnPosition = new Vector3(_gameProperties.movementAxis.x * _gameProperties.movementDistance + _gameProperties.lastCubePosition.x,
                                                _gameProperties.currentHeight,
                                                _gameProperties.movementAxis.y * _gameProperties.movementDistance + _gameProperties.lastCubePosition.z);

            Instantiate(gameObject, spawnPosition, Quaternion.identity);
        }

        private void MoveCube()
        {           
            if (transform.position.x < -_gameProperties.movementDistance || transform.position.z < -_gameProperties.movementDistance)
            {
                movementDirection = -1;
            }
            else if (transform.position.x > _gameProperties.movementDistance || transform.position.z > _gameProperties.movementDistance)
            {
                movementDirection = 1;
            }
            transform.position -= movementDirection * new Vector3(_gameProperties.movementAxis.x * speed * Time.deltaTime,
                                                  0,
                                                  _gameProperties.movementAxis.y * speed * Time.deltaTime);
        }

        private void PlayerInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckPosition();
                if (!isDestroyed)
                {
                    ChangeGameProp();
                    SpawnCube();
                }
                Destroy(this);
            }
        }

        private void ChangeGameProp()
        {
            if (_gameProperties.movementAxis.x > 0)
            {
                _gameProperties.movementAxis.x = 0;
                _gameProperties.movementAxis.y = 1;
            }
            else
            {
                _gameProperties.movementAxis.x = 1;
                _gameProperties.movementAxis.y = 0;
            }

            _gameProperties.lastCubeLocalScale = transform.localScale;
            _gameProperties.lastCubePosition = transform.position;
            _gameProperties.currentHeight += transform.localScale.y;
        }

        private void CheckPosition()
        {
            if (Mathf.Abs(transform.position.x - _gameProperties.lastCubePosition.x) < _gameProperties.checkPosThreshold &&
                Mathf.Abs(transform.position.z - _gameProperties.lastCubePosition.z) < _gameProperties.checkPosThreshold)
            {
                transform.position = new Vector3(_gameProperties.lastCubePosition.x, transform.position.y, _gameProperties.lastCubePosition.z);
            }
            else if (Mathf.Abs(transform.position.x - _gameProperties.lastCubePosition.x) < transform.localScale.x &&
                     Mathf.Abs(transform.position.z - _gameProperties.lastCubePosition.z) < transform.localScale.z)
            {
                if (GetComponent<CubeSplitter>() != null)
                {
                    GetComponent<CubeSplitter>().SplitCube();
                }
            }
            else
            {
                UIRestartGame.instance.ActivateRestartButton();
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }
}
