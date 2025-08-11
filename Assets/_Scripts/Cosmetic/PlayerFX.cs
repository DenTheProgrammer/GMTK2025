using UnityEngine;

public class PlayerFX : MonoBehaviour
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

        if (!isJumpingStart) //on landing
        {
            FX.ScreenShake(Vector2.down, 0.25f, 0.125f);
        }
    }
}
