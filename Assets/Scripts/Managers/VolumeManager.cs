using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicVolumeSlider", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolumeSlider", volume);
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolumeSlider", volume);
    }
    private void Start()
    {
        LoadSliderValues();
    }
    private void LoadSliderValues()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeSlider", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumeSlider", 1f);
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolumeSlider", 1f);

        // Re-apply mixer values so audio matches UI
        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
    }

}
