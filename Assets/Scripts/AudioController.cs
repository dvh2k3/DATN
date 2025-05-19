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
    public void ChangeAudioVolume(float _change)
    {
        //lấy giá trị âm thanh cơ bản
        //float baseVolume = 1;

        //lấy giá trị ban đầu của âm thanh và thay đổi nó
        float currentAudioVolume = PlayerPrefs.GetFloat("soundVolume", 1f);// tải âm thanh đã lưu cuối cùng từ tùy chọn của Player prefs
        currentAudioVolume += _change;

        //kiểm tra xem âm thanh đang đạt giá trị lớn nhất hay nhỏ nhất
        if (currentAudioVolume > 1)
            currentAudioVolume = 0;
        else if (currentAudioVolume < 0)
            currentAudioVolume = 1;

        //gán giá trị cuối cùng
        //float finalVolume = currentAudioVolume * baseVolume;
        levelMusic.volume = currentAudioVolume;

        //Lưu giá gị cuối cùng vòa PlayerPrefts
        PlayerPrefs.SetFloat("soundVolume", currentAudioVolume);
    }
}
