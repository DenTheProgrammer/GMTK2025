using UnityEngine;
public class FX
{
    private static bool _isInited = false;
    
    
    private static IAudioPlayer _audioManager;
    private static VisualEffectsManager _visualEffectsManager;

    public static void Init()
    {
        _audioManager = ServiceLocator.Get<IAudioPlayer>();
        _visualEffectsManager = ServiceLocator.Get<VisualEffectsManager>();
    }
    
    public static AudioSource PlaySound(SoundData sound, Vector3 position)
    {
        if (!_isInited) Init();
        AudioSource audioSource = _audioManager.Play(sound, position);
        return audioSource;
    }

    public static void StopSound(AudioSource audioSource)
    {
        if (!_isInited) Init();
        _audioManager.Stop(audioSource);
    }

    public static void BloomBoom(float intensity, float duration)
    {
        if (!_isInited) Init();
        
        _visualEffectsManager.ScreenFlash(intensity, duration);
    }
}
