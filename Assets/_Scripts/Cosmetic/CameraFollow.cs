using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Player

    [Header("Follow Settings")]
    public float smoothSpeed = 2f; // camera follow speed
    public Vector2 maxOffset = new Vector2(2f, 1f); // limits for camera offset from start

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Get player position relative to start
        Vector3 relativePosition = target.position - _startPosition;

        // Normalize to range -1..1 for offset direction
        float normalizedX = Mathf.Clamp(relativePosition.x / 10f, -1f, 1f);
        float normalizedY = Mathf.Clamp(relativePosition.y / 10f, -1f, 1f);

        // Calculate desired offset
        Vector3 desiredOffset = new Vector3(normalizedX * maxOffset.x, normalizedY * maxOffset.y, 0f);

        // Target position
        Vector3 desiredPosition = _startPosition + desiredOffset;
        desiredPosition.z = _startPosition.z;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}