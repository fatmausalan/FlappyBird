using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public Text highScoreText;
    public Text scoreText;
    void Start()
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        int score = PlayerPrefs.GetInt("score");
        highScoreText.text = "HighScore : " + highScore;
        scoreText.text = "Score : " + score;

    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("level");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
