using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

    private Text _text;
    public static int highScore = 0;
    private const string HighScoreKey = "HighScore";

    void Awake()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            highScore = PlayerPrefs.GetInt(HighScoreKey);
        }
    }
    
    void Start()
    {
        _text = this.GetComponent<Text>();
        SetHighScore(highScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHighScore(int score)
    {
        _text.text = "Рекорд: " + score;
        PlayerPrefs.SetInt(HighScoreKey, score);
    }
}
