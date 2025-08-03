using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Glow : MonoBehaviour
{
    [SerializeField] private float glowMultiplier = 1.2f;

    private static readonly int FaceColor = Shader.PropertyToID("_FaceColor");

    private Material _material;
    private Color _originalColor;
    private float _currentGlowAmount = 1f;

    private void Start()
    {
        CacheStartValues();
        SetGlow(glowMultiplier);
    }

    private void Update()
    {
#if UNITY_EDITOR
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            _ = SetGlow(1.6f, 3, 0.33f);
        }
#endif
    }

    public async Awaitable SetGlow(float intensity, float repeat, float duration)
    {
        for (int i = 0; i < repeat; i++)
        {
            SetGlow(intensity);
            await Awaitable.WaitForSecondsAsync(duration/2f);
            SetGlow(1);   
            await Awaitable.WaitForSecondsAsync(duration/2f);
        }
    }

    public void SetGlow(float intensity)
    {
        // SpriteRenderer
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            float alpha = spriteRenderer.color.a;
            Color color = spriteRenderer.color;
            color /= _currentGlowAmount;
            color *= intensity;
            
            color.a = alpha;
            spriteRenderer.color = color;
        }
        // TextMeshProUGUI
        else if (TryGetComponent(out TextMeshProUGUI textMeshProUGUI))
        {
            Color currentColor = _material.GetColor(FaceColor);
            float alpha = currentColor.a;
            currentColor /= _currentGlowAmount;
            currentColor *= intensity;
            currentColor.a = alpha;
            _material.SetColor(FaceColor, currentColor);
        }
        // TextMeshPro
        else if (TryGetComponent(out TextMeshPro textMeshPro))
        {
            Color currentColor = _material.GetColor(FaceColor);
            float alpha = currentColor.a;
            currentColor /= _currentGlowAmount;
            currentColor *= intensity;
            currentColor.a = alpha;
            _material.SetColor(FaceColor, currentColor);
        }
        // UI Image
        else if (TryGetComponent(out Image image))
        {
            Color currentColor = _material.color;
            float alpha = currentColor.a;
            currentColor /= _currentGlowAmount;
            currentColor *= intensity;
            currentColor.a = alpha;
            _material.color = currentColor;
        }
        // MeshRenderer
        else if (TryGetComponent(out MeshRenderer meshRenderer))
        {
            Color currentColor = _material.color;
            float alpha = currentColor.a;
            currentColor /= _currentGlowAmount;
            currentColor *= intensity;
            currentColor.a = alpha;
            _material.color = currentColor;
        }
        _currentGlowAmount = intensity;
    }
    
    private void CacheStartValues()
    {
        // SpriteRenderer
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            _originalColor = spriteRenderer.color;
        }
        // TextMeshProUGUI
        else if (TryGetComponent(out TextMeshProUGUI textMeshProUGUI))
        {
            _material = new Material(textMeshProUGUI.fontMaterial);
            textMeshProUGUI.fontMaterial = _material;
            _originalColor = _material.GetColor(FaceColor);
        }
        // TextMeshPro
        else if (TryGetComponent(out TextMeshPro textMeshPro))
        {
            _material = new Material(textMeshPro.fontMaterial);
            textMeshPro.fontMaterial = _material;
            _originalColor = _material.GetColor(FaceColor);
        }
        // UI Image
        else if (TryGetComponent(out Image image))
        {
            _material = new Material(image.material);
            image.material = _material;
            _originalColor = _material.color;
        }
        // MeshRenderer
        else if (TryGetComponent(out MeshRenderer meshRenderer))
        {
            _material = new Material(meshRenderer.material);
            meshRenderer.material = _material;
            _originalColor = _material.color;
        }
    }

    void OnDestroy()
    {
        RestoreValues();
    }

    private void RestoreValues()
    {
        // SpriteRenderer
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.color = _originalColor;
        }
        // TextMeshProUGUI
        else if (TryGetComponent(out TextMeshProUGUI textMeshProUGUI))
        {
            if (_material == null) return;
            _material.SetColor(FaceColor, _originalColor);
        }
        // TextMeshPro
        else if (TryGetComponent(out TextMeshProUGUI textMeshPro))
        {
            if (_material == null) return;
            _material.SetColor(FaceColor, _originalColor);
        }
        // UI Image
        else if (TryGetComponent(out Image image))
        {
            if (_material == null) return;
            _material.color = _originalColor;
        }
        // MeshRenderer
        else if (TryGetComponent(out MeshRenderer meshRenderer))
        {
            if (_material == null) return;
            _material.color = _originalColor;
        }
    }
}