using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("# Platform info")]
    public Color platformColor;

    [Header("# Player")]
    public Player player;
    public Color playerColor = Color.white;

    [Header("# Score info")]
    public int coins;
    public float distance = 0f;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        LoadColor();
    }


    private void Update()
    {
        if (player.transform.position.x > distance)
        {
            distance = player.transform.position.x;
        }
    }

    public void SaveColor(float r, float b, float g, float a)
    {
        PlayerPrefs.SetFloat("ColorR", r);
        PlayerPrefs.SetFloat("ColorB", b);
        PlayerPrefs.SetFloat("ColorG", g);
        PlayerPrefs.SetFloat("ColorA", a);
    }

    private void LoadColor()
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(PlayerPrefs.GetFloat("ColorR"), PlayerPrefs.GetFloat("ColorG"),
            PlayerPrefs.GetFloat("ColorB"), PlayerPrefs.GetFloat("ColorA", 1));
    }

    public void UnlockPlayer() => player.playerUnlock = true;

    public void ResetLevel()
    {
        SaveInfo();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    

    public void SaveInfo()
    {
        int saveCoin = PlayerPrefs.GetInt("coin");

        PlayerPrefs.SetInt("coin", coins + saveCoin);

        float score = distance * coins;

        PlayerPrefs.SetFloat("distance", distance);

        if (PlayerPrefs.GetFloat("highScore") < score)
        {
            PlayerPrefs.SetFloat("highScore", score);
        }
    }
}
