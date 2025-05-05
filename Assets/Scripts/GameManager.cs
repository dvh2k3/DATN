using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentCoins;
    public TMP_Text coinText;

    public GameObject gameOverScreen;
    public float gameOverDely, timeToRespawn;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCoins = SaveManager.instance.activeSave.currentCoins;
        UpdateCoin();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoin();
    }

    public void GetCoins(int coinToGive)
    {
        currentCoins += coinToGive;
    }

    public void UpdateCoin()
    {
        coinText.text = currentCoins.ToString();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverco());
    }

    public IEnumerator GameOverco()
    {
        yield return new WaitForSeconds(gameOverDely);
        gameOverScreen.SetActive(true);
        AudioController.instance.PlayPlayerSFX(2);
        yield return new WaitForSeconds(timeToRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
