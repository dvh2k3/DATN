using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public GameObject winScreen;
    public float winDely, timeToExit;
    public int nextSceneLoad;
    public Animator anim;
    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1 ;
        anim = GetComponent<Animator>();
    }
    public void WinGame()
    {
        StartCoroutine(WinGameco());
    }

    public IEnumerator WinGameco()
    {
        yield return new WaitForSeconds(winDely);
        winScreen.SetActive(true);
        AudioController.instance.PlayPlayerSFX(13);
        //yield return new WaitForSeconds(timeToExit);

        //SceneManager.LoadScene(nextSceneLoad);
        //if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        //{
        //    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("Open", true);   
            WinGame();
        }
    }
    
}
