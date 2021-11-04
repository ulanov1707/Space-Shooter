using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver ;


    private void Start()
    {
        _isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
    }
    public void GameOver() {

        _isGameOver = true;
        Debug.Log("Game OVer called");
        
    }
    void Restart()
    {
        if (_isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game");
            }

        }
    }
}
