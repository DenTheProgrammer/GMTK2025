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
        //SetGlow(glowMultiplier); //TODO: remove, DBG!
    }

    public void SetGlow(float intensity)
    {
        // SpriteRenderer
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.color /= _currentGlowAmount;
            spriteRenderer.color *= intensity;
        }
        // TextMeshProUGUI
        else if (TryGetComponent(out TextMeshProUGUI textMeshProUGUI))
        {
            Color currentColor = _material.GetColor(FaceColor);
            currentColor /= _currentGlowAmount;
            currentColor *= intensity;
            _material.SetColor(FaceColor, currentColor);
        }
        // TextMeshPro
        else if (TryGetComponent(out TextMeshPro textMeshPro))
        {
            Color currentColor = _material.GetColor(FaceColor);
            currentColor /= _currentGlowAmount;
            currentColor *= intensity;
            _material.SetColor(FaceColor, currentColor);
        }
        // UI Image
        else if (TryGetComponent(out Image image))
        {
            _material.color /= _currentGlowAmount;
            _material.color *= intensity;
        }
        // MeshRenderer
        else if (TryGetComponent(out MeshRenderer meshRenderer))
        {
             meshRenderer.material.color /= _currentGlowAmount;
             meshRenderer.material.color *= intensity;
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
            _material = textMeshProUGUI.fontMaterial;
            _originalColor = _material.GetColor(FaceColor);
        }
        // TextMeshPro
        else if (TryGetComponent(out TextMeshPro textMeshPro))
        {
            _material = textMeshPro.fontMaterial;
            _originalColor = _material.GetColor(FaceColor);
        }
        // UI Image
        else if (TryGetComponent(out Image image))
        {
            _material = image.material;
            _originalColor = _material.color;
        }
        // MeshRenderer
        else if (TryGetComponent(out MeshRenderer meshRenderer))
        {
            _material = meshRenderer.material;
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