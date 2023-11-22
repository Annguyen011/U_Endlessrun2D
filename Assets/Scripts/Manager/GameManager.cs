using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("# Coins")]
    public Color platformColor;
    public int coins;

    public void ResetLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
