using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] lines;
    public bool canActive;
    

    // Update is called once per frame
    void Update()
    {
        if(canActive && Input.GetMouseButtonUp(1) && !DialogManager.Instance.dialogPanel.activeInHierarchy) 
        {
            DialogManager.Instance.ShowDialog(lines);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActive = false;
        }
    }
}
