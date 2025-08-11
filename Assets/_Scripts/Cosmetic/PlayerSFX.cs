using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private SoundData jumpSound;

    private void Start()
    {
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
            FX.PlaySound(jumpSound, transform.position);
        }
    }
}
