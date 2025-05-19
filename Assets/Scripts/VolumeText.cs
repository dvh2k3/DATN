using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeKey;      // Key để lấy từ PlayerPrefs, ví dụ: "soundVolume"
    [SerializeField] private string textIntro;      // Phần đầu, ví dụ: "Sound: "
    private TextMeshProUGUI txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UpdateVolumeText(); //Cập nhật mỗi lần UI hiển thị
    }

    private void Update()
    {
        UpdateVolumeText(); // (Không bắt buộc nếu chỉ cập nhật lúc cần)
    }

    private void UpdateVolumeText()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeKey, 1f) * 100f;
        txt.text = textIntro + Mathf.RoundToInt(volumeValue).ToString();
    }
}
