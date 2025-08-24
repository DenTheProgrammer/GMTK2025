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
            FX.PlaySound(jumpSound);
        }

        if (!isJumpingStart) //on landing
        {
            //FX.ScreenShake(Vector2.down, 0.07f, 0.125f);
        }
    }
}
