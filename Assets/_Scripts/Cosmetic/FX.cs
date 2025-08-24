using UnityEngine;
public class FX
{
    private static bool _isInited = false;
    
    private static IAudioPlayer _audioManager;
    private static VisualEffectsManager _visualEffectsManager;
    private static CameraShakeManager _cameraShakeManager;

    public static void Init()
    {
        _audioManager = ServiceLocator.Get<IAudioPlayer>();
        _visualEffectsManager = ServiceLocator.Get<VisualEffectsManager>();
        _cameraShakeManager = ServiceLocator.Get<CameraShakeManager>();
    }
    
    public static AudioSource PlaySound(SoundData sound, Transform spawnTransform = null, bool follow = false)
    {
        if (!_isInited) Init();
        AudioSource audioSource = _audioManager.Play(sound, spawnTransform, follow);
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

    public static void ScreenShake(Vector2 direction, float strength, float duration)
    {
        if (!_isInited) Init();
        
        _ = _cameraShakeManager.TriggerShake(direction, strength, duration).RunSafe();
    }
}
