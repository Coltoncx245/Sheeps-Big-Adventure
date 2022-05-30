using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void RunCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
