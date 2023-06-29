using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    
    [SerializeField] private Image musicImage;
    [SerializeField] private Image soundImage;
    [SerializeField] private Image vibrationImage;
    
    private int volumeSounds;
    private int volumeMusic;
    private int vibration;
    
    void Start()
    {
        volumeSounds = PlayerPrefs.GetInt("Sounds", 1);
        volumeMusic = PlayerPrefs.GetInt("Music", 1);
        vibration = PlayerPrefs.GetInt("Vibration", 1);
        UpdateUI();
    }

    public void ChangeSoundsVolume()
    {
        volumeSounds = volumeSounds == 0 ? 1 : 0;
        PlayerPrefs.SetInt("Sounds", volumeSounds);
        UpdateUI();
    }
    
    public void ChangeVipration()
    {
        vibration = vibration == 0 ? 1 : 0;
        PlayerPrefs.SetInt("Vibration", vibration);
        UpdateUI();
    }
    
    public void ChangeMusicVolume()
    {
        volumeMusic = volumeMusic == 0 ? 1 : 0;
        PlayerPrefs.SetInt("Music", volumeMusic);
        UpdateUI();
    }

    private void UpdateUI()
    {
        musicImage.color = volumeMusic == 0 ? inactiveColor : activeColor;
        soundImage.color = volumeSounds == 0 ? inactiveColor : activeColor;
        vibrationImage.color = vibration == 0 ? inactiveColor : activeColor;
    }
    
}
