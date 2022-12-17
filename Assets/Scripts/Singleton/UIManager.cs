using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //Game over UI
    public GameObject gameOverUI;

    public Text scoreText;

    public Text healthText;
    public Text GameOverScoreText;

    public static UIManager Instance { get; private set; }


    private void Awake()
    {
        //Singleton Logic
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void ToggleGameOverUI(bool flag) {
        gameOverUI.SetActive(flag);
    }

    public void ShowGameOverScore(string text) {
        GameOverScoreText.text = text;
    }

    public void ShowHealth(string text)
    {
        healthText.text = text;
    }
    public void ShowScore(string text)
    {
        scoreText.text = text;

    }
}
