using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public string[] sentences;
    public int currentSentence;
    public bool justStarted;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogPanel.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(1))
            {
                if(!justStarted)
                {
                    currentSentence++;
                    if(currentSentence >= sentences.Length)
                    {
                        dialogPanel.SetActive(false);
                    }
                    else
                    {
                        dialogText.text = sentences[currentSentence];
                    }
                }
                else
                {
                    justStarted = false;
                }
            }
        }
    }
    public void ShowDialog(string[] newLines)
    {
        sentences = newLines;
        currentSentence = 0;
        dialogText.text = sentences[currentSentence];
        dialogPanel.SetActive(true);
        justStarted = true;
    }
}
