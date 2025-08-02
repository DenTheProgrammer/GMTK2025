using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public Slider sfxVolume;
    [SerializeField] private Slider musicVolume;
    [Header("Fun Settings")]
    [SerializeField] public Slider brightness;
    [SerializeField] private SpriteRenderer brightnessImage;
    [SerializeField] public Toggle colorblindToggle;
    [SerializeField] private Volume ppVolume;
    
    private ColorAdjustments _colorAdjustments;
    
    private void Awake()
    {
        sfxVolume.onValueChanged.AddListener(UpdateVolume);
        musicVolume.onValueChanged.AddListener(UpdateVolume);
        brightness.onValueChanged.AddListener(UpdateBrightness);
        colorblindToggle.onValueChanged.AddListener(UpdateColorblind);
        ppVolume.profile.TryGet(out _colorAdjustments);
        
        ServiceLocator.Register(this, false);
    }

    private void UpdateColorblind(bool isOn)
    {
        _colorAdjustments.saturation.value = isOn ? -100f : 0f;
    }

    private void UpdateBrightness(float value)
    {
        Color tmp = brightnessImage.color;
        tmp.a = 1-value;
        brightnessImage.color = tmp;
    }

    private void OnDestroy()
    {
        sfxVolume.onValueChanged.RemoveListener(UpdateVolume);
        musicVolume.onValueChanged.RemoveListener(UpdateVolume);
        brightness.onValueChanged.RemoveListener(UpdateBrightness);
        colorblindToggle.onValueChanged.RemoveListener(UpdateColorblind);
    }
    
    private void UpdateVolume(float _)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume.value) * 20f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume.value) * 20f);
    }
}
