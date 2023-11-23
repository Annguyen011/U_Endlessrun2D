using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("# InGame info")]
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Image heartEmpty;
    [SerializeField] private Image heartFull;
    private float distance;
    private int coin;

    [Header("# Pause info")]
    [SerializeField] private bool gamePause;


    [Header("# Shop info")]
    [SerializeField] private TextMeshProUGUI coinShopText;

    [Header("# EndGame info")]
    [SerializeField] private TextMeshProUGUI coinEndText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("# Score info")]
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI coinHasText;

    #region Components

    private Player player;

    #endregion

    private void Start()
    {
        player = GameManager.instance.player;

        lastScoreText.text ="Last score :"  + PlayerPrefs.GetFloat("score").ToString("#,#");
        scoreText.text = "Best score :" + PlayerPrefs.GetFloat("score").ToString("#,#");
        highScoreText.text = PlayerPrefs.GetFloat("highScore").ToString("#,#");

        InvokeRepeating("UpdateInfo", 0, .15f);
    }

    public void PauseGame()
    {
        if (gamePause)
        {
            Time.timeScale = 1.0f;
            gamePause = false;
        }
        else if (!gamePause)
        {
            Time.timeScale = 0f;
            gamePause = true;
        }
    }

    private void UpdateInfo()
    {
        distance = GameManager.instance.distance;
        coin = GameManager.instance.coins;

        if (distance > 0)
            distanceText.text = distance.ToString("#,#") + " m";

        if (coin > 0)
            coinText.text = coin.ToString("#,#");

        heartEmpty.enabled = !player.extraLife;
        heartFull.enabled = player.extraLife;
    }

    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        coinHasText.text = PlayerPrefs.GetFloat("coin").ToString("#,#");
        coinShopText.text = PlayerPrefs.GetFloat("coin").ToString("#,#");
        coinEndText.text = PlayerPrefs.GetFloat("coin").ToString("#,#");

        uiMenu.SetActive(true);
    }

    public void StartGame()
    {
        GameManager.instance.UnlockPlayer();
    }

    public void ResetGameButton()=> GameManager.instance.ResetLevel();

    
}
