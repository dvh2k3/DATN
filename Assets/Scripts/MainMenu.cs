using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string startScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ResetLevels();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void ResetLevels()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Level Reseted");
        }
    }
}
