using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image CurrentLiveImage;
    [SerializeField]
    private Text _GameOverText;
    [SerializeField]
    private Text _RestartText;
    [SerializeField]
    private Text _StartText;
    [SerializeField]
    private GameManager _MyGameManager;



    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0" ;

        _GameOverText.enabled = false;
        _RestartText.enabled = false;

        if (_MyGameManager == null) {
            Debug.Log("Game Manger not Assigned");
        }

        Destroy(_StartText.gameObject, 8f);

    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void ChangeScoreUI(int score) {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentPlayerLives) {

        CurrentLiveImage.sprite = _livesSprites[currentPlayerLives ];
        if (currentPlayerLives <= 0) { 
            StartCoroutine(SetGameOverActive());
            _RestartText.enabled = true;
            _MyGameManager.GetComponent<GameManager>().GameOver();
        }
    }

    IEnumerator SetGameOverActive()
    {
        while (true)
        {
            _GameOverText.enabled = true;
            yield return new WaitForSeconds(0.4f);
            _GameOverText.enabled = false;
            yield return new WaitForSeconds(0.4f);
        }
    }

   

    
}
