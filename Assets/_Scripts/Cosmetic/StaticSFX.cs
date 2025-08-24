using UnityEngine;

public class StaticSFX : MonoBehaviour
{
    [SerializeField] private SoundData sound;

    private AudioSource _audioSource;
    private IAudioPlayer _audioManager;
    private void Start()
    {
        _audioManager = ServiceLocator.Get<IAudioPlayer>();
        _audioSource = _audioManager.Play(sound);
    }

    private void OnDestroy()
    {
        _audioManager.Stop(_audioSource);
    }
}