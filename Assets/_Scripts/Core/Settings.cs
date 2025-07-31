using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private Slider musicVolume;

    private void Awake()
    {
        sfxVolume.onValueChanged.AddListener(UpdateVolume);
        musicVolume.onValueChanged.AddListener(UpdateVolume);
    }
    private void OnDestroy()
    {
        sfxVolume.onValueChanged.RemoveListener(UpdateVolume);
        musicVolume.onValueChanged.RemoveListener(UpdateVolume);
    }
    
    private void UpdateVolume(float _)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume.value) * 20f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume.value) * 20f);
    }
}
