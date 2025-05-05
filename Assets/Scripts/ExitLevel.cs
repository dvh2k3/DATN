using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public GameObject winScreen;
    public float winDely, timeToExit;

    public void WinGame()
    {
        StartCoroutine(WinGameco());
    }

    public IEnumerator WinGameco()
    {
        yield return new WaitForSeconds(winDely);
        winScreen.SetActive(true);
        AudioController.instance.PlayPlayerSFX(13);
        yield return new WaitForSeconds(timeToExit);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            WinGame();
        }
    }
}
