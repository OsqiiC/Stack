using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIRestartGame : MonoBehaviour
{
    public GameObject button;
    public static UIRestartGame instance;

    private void Awake()
    {
        instance = this;
    }

    public void ActivateRestartButton()
    {
        button.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
