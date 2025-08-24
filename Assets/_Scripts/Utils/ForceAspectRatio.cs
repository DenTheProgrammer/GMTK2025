using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup panel;
    [Header("Settings")]
    [SerializeField] private List<Vector2Int> validAspects = new ();
    
    public event Action<Vector2Int> OnAspectRatioChanged;
    
    private Camera _camera;
    private Vector2Int _targetAspect;
    private float _targetAspectRatio;
    private CancellationTokenSource _cts;
    
    void Awake() 
    {
        _camera = Camera.main;

        _cts = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);
        ChooseSuitableAspectRatio();
        ApplyAspectRatio();
    }

    private void ChooseSuitableAspectRatio()
    {
        float currentScreenAspect = (float)Screen.width / Screen.height;
        
        Vector2Int mostSuitableAspect = validAspects[0];
        float mostSuitableAspectRatio = validAspects[0].x / (float)validAspects[0].y;

        for (int i = 1; i < validAspects.Count; i++)
        {
            float aspectRatio = validAspects[i].x / (float)validAspects[i].y;
            if (Mathf.Abs(aspectRatio - currentScreenAspect) < Mathf.Abs(mostSuitableAspectRatio - currentScreenAspect))
            {
                mostSuitableAspect = validAspects[i];
                mostSuitableAspectRatio = mostSuitableAspect.x / (float)mostSuitableAspect.y;
            }
        }

        if (mostSuitableAspect != _targetAspect)
        {
            Vector2Int previousAspect = _targetAspect;
            _targetAspect = mostSuitableAspect;
            _targetAspectRatio = mostSuitableAspectRatio;
            if (previousAspect == Vector2Int.zero) return; //don't do anything on startup
            
            OnAspectRatioChanged?.Invoke(_targetAspect);
            Debug.Log($"Aspect ratio changed: {_targetAspect}");
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);
            }
            _ = HandleAspectChanged(_cts.Token, _targetAspect).RunSafe();
        }
    }

    private async Awaitable HandleAspectChanged(CancellationToken token, Vector2Int newAspect)
    {
        text.text = $"{newAspect.x}:{newAspect.y}";
        float elapsedTime = 0;
        float fadeTimer = 3f;
        panel.alpha = 1f;
        
        do
        { 
            await Awaitable.NextFrameAsync(token);
            elapsedTime += Time.deltaTime;
            panel.alpha = Mathf.Lerp(panel.alpha, 0f, elapsedTime/fadeTimer);
        }while(elapsedTime < fadeTimer && !token.IsCancellationRequested);
        panel.alpha = 0f;
    }

    private void Update()
    {
        ChooseSuitableAspectRatio();
        ApplyAspectRatio();
    }

    private void ApplyAspectRatio()
    {
        // determine the game window's current aspect ratio
        float windowAspect = Screen.width / (float)Screen.height;

        // this amount should scale current viewport height
        float scaleHeight = windowAspect / _targetAspectRatio;
        
        // if scaled height is less than current height, add letterbox
        if (scaleHeight < 1.0f)
        {  
            Rect rect = _camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        
            _camera.rect = rect;
        }
        else // add pillarBox
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = _camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            _camera.rect = rect;
        }
    }
}
