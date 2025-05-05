using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource mainMenuMusic;
    public AudioSource levelMusic;
    public AudioSource BossMusic;
    public AudioSource[] playersfx;
    public AudioSource[] enemyDeathsfx;
    public AudioSource[] uisfx;
    public AudioSource[] effectsfx;

    public void PlayMainMenuMusic()
    {
        levelMusic.Stop();
        BossMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        levelMusic.Play();
        BossMusic.Stop();
        mainMenuMusic.Stop();
    }
    public void PlayBossMusic()
    {
        levelMusic.Stop();
        BossMusic.Play();
        mainMenuMusic.Stop();
    }

    public void PlayPlayerSFX(int sfxPlayer)
    {
        playersfx[sfxPlayer].Stop();
        playersfx[sfxPlayer].Play();
    }

    public void PlayEnemyDeathSFX(int sfxEnemy)
    {
        enemyDeathsfx[sfxEnemy].Stop();
        enemyDeathsfx[sfxEnemy].Play();
    }

    public void PlayUiSFX(int sfxUi)
    {
        uisfx[sfxUi].Stop();
        uisfx[sfxUi].Play();
    }

    public void PlayEffectSFX(int sfxEffect)
    {
        effectsfx[sfxEffect].Stop();
        effectsfx[sfxEffect].Play();
    }
}
