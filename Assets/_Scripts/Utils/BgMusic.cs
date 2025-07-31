using System;
using UnityEngine;

public class BgMusic : MonoBehaviour
{
    [SerializeField] private SoundData bgMusic;
    
    private AudioManager _audioManager;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioManager = ServiceLocator.Get<AudioManager>();
        _audioSource = _audioManager.Play(bgMusic, Vector3.zero);
    }

    private void OnDestroy()
    {
        if (_audioManager != null)
        {
            _audioManager.StopSound(_audioSource);
        }
    }
}
