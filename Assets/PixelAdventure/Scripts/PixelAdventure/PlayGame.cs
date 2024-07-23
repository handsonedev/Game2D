using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void LoadScence()
    {
        SceneManager.LoadScene("level1");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartGame");
    }
}
