using System;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private SoundData jumpSound;
    
    private AudioManager _audioManager;
    private void Start()
    {
        _audioManager = ServiceLocator.Get<AudioManager>();
        PlayerController.OnJump += OnJump;
    }

    private void OnDestroy()
    {
        PlayerController.OnJump -= OnJump;
    }

    private void OnJump(bool isJumpingStart)
    {
        if (isJumpingStart)
        {
            _audioManager.Play(jumpSound, transform.position);
        }
    }
}
