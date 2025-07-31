using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        InitTeleportPlayer();
    }

    private void InitTeleportPlayer()
    {
        PlayerSpawnPoint spawnPoint = FindAnyObjectByType<PlayerSpawnPoint>();
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }
}