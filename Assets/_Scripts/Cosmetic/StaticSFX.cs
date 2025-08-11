using UnityEngine;

public class StaticSFX : MonoBehaviour
{
    [SerializeField] private SoundData sound;

    private AudioSource _audioSource;
    private AudioManager _audioManager;
    private void Start()
    {
        _audioManager = ServiceLocator.Get<AudioManager>();
        _audioSource = _audioManager.Play(sound, transform.position);
    }

    private void OnDestroy()
    {
        _audioManager.Stop(_audioSource);
    }
}