using System;
using UnityEngine;

[RequireComponent(typeof(Glow))]
public class SplashGlowup : MonoBehaviour
{
    [SerializeField] private float intensity = 2f;
    [SerializeField] private float totalTime = 3f;
    
    private float _currentIntensity;
    private float _currentTime;
    private Glow _glow;
    
    private void Awake()
    {
        _glow = GetComponent<Glow>();
        Glowup();
    }
    
    private async void Glowup()
    {
        try
        {
            while (_currentIntensity < intensity)
            {
                _currentTime += Time.deltaTime;
                _currentIntensity = Mathf.Lerp(1, intensity, _currentTime / totalTime);
                _glow.SetGlow(_currentIntensity);
                await Awaitable.NextFrameAsync(destroyCancellationToken);
            }
            _currentTime = 0;
        
            while (_currentIntensity > 1)
            {
                _currentTime += Time.deltaTime;
                _currentIntensity = Mathf.Lerp(intensity, 1, _currentTime / totalTime);
                _glow.SetGlow(_currentIntensity);
                await Awaitable.NextFrameAsync(destroyCancellationToken);
            }
        }
        catch (Exception e)
        {
            if (e.GetType() == typeof(OperationCanceledException)) return;
            Debug.LogException(e);
        }
    }
}
