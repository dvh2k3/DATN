using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private GameObject gameWin;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //nếu màn hình đã tạm dừng thì bỏ và ngược lại
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }
    public void GameWin()
    {
        gameWin.SetActive(true);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        AudioController.instance.PlayPlayerSFX(2);
    }
    //Game over function
    public void NextLevel()
    {
        int nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextSceneLoad);
        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }

        //if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        //{
        //    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        //}
    }
    public void Restart()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();// thoát game chỉ hoạt động khi đang build
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;// thoát khỏi chế độ phát
        #endif
    }

    public void PauseGame(bool status)
    {
        //nếu trạng thái tồn tại thì pause và ngược lại
        pauseScreen.SetActive(status);
        // khi trạng thái pause diễn ra thì màn hình game đứng im và ngược lại
        if(status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void AudioVolume()
    {
        AudioController.instance.ChangeAudioVolume(0.2f);
        float volume = PlayerPrefs.GetFloat("soundVolume", 1f);
        int percent = Mathf.RoundToInt(volume * 100);
        soundVolumeText.text = "SOUNDS: " + percent;
    }
}
