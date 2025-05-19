using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;// âm thanh khi di chuyển mũi tên
    [SerializeField] private AudioClip interactSound;// âm thanh chọn một button
    private RectTransform rect;
    private int currentPosition;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //Di chuyển mũi tên để chọn button
        if(Input.GetKeyDown(KeyCode.UpArrow)) 
            ChangePosition(-1);
        if(Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);
        // Tương tác với các tùy chọn
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            Interact();
    }
    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if(_change != 0)
        {
            AudioController.instance.PlayUiSFX(7);
        }
        if(currentPosition < 0)
        {
            currentPosition = options.Length -1;
        }else if(currentPosition > options.Length -1) 
        {
            currentPosition = 0;
        }
        //gán vị trí trục Y của tùy chọn SelectionArrow
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }
    private void Interact()
    {
        AudioController.instance.PlayUiSFX(0);

        //truy cập thành phần nút trên mỗi tùy chọn và gọi hàm của nó
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
