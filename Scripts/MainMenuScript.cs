using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    //Event that will be called when button is pressed

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
