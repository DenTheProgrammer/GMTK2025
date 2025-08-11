using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShakeManager : MonoBehaviour
{
    private Vector3 _startLocalPosition;
    private Vector3 _currentOffset = Vector3.zero;
    
    private void Awake()
    {
        ServiceLocator.Register(this);
        _startLocalPosition = transform.localPosition;
    }

    public async Awaitable TriggerShake(Vector2 direction, float strength, float duration)
    {
        float timePassed = 0;

        while (!destroyCancellationToken.IsCancellationRequested)
        {
            if (timePassed < duration / 2) //start shake
            {
                _currentOffset = Vector3.Lerp(Vector3.zero, direction * strength, timePassed / (duration / 2));
            }
            else if  ((timePassed >= duration / 2) && (timePassed < duration)) //return shake
            {
                _currentOffset = Vector3.Lerp(direction * strength, Vector3.zero, (timePassed - (duration / 2)) / (duration / 2));
            }
            else
            {
                _currentOffset = Vector3.zero;  
                break;
            }

            Debug.Log($"Current offset = {_currentOffset}, start local position = {_startLocalPosition}");
            transform.localPosition = _startLocalPosition + _currentOffset;
            await Awaitable.NextFrameAsync(destroyCancellationToken);
            timePassed += Time.deltaTime;
        }
        transform.localPosition = _startLocalPosition + _currentOffset;
    }

    private void OnDestroy()
    {
        Debug.Log($"On destroy camera shake manager");
        transform.localPosition = _startLocalPosition;
    }
}
