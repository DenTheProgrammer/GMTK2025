using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisualEffectsManager : MonoBehaviour
{
    [SerializeField] private Volume ppVolume;
    private Bloom _bloom;
    private ChromaticAberration _chromaticAberration;
    private DepthOfField _depthOfField;
    private LensDistortion _lensDistortion;

    private Tween _bloomTween;
    private readonly Dictionary<Type, float> _originalValues = new();

    private void Awake()
    {
        ServiceLocator.Register(this);
    }

    private void Start()
    {
        ppVolume.profile.TryGet(out _bloom);
        ppVolume.profile.TryGet(out _chromaticAberration);
        ppVolume.profile.TryGet(out _lensDistortion);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ScreenFlash();
        }
    }

    public void BumpParameter(Type type, FloatParameter parameter, float bumpAmount, float durationUp, float durationDown)
    {
        if (!_originalValues.ContainsKey(type))
            _originalValues[type] = parameter.value;

        float baseValue = _originalValues[type];
        
        DOTween.Kill(type);

        DOTween.Sequence()
            .Append(DOTween.To(() => parameter.value, x => parameter.value = x, baseValue + bumpAmount, durationUp))
            .Append(DOTween.To(() => parameter.value, x => parameter.value = x, baseValue, durationDown))
            .SetTarget(type);
    }

    public void ScreenFlash(float intensity = 1, float duration = 1f)
    {
        BumpParameter(typeof(Bloom), _bloom.intensity, 2 * intensity, duration * 0.1f, duration *0.5f);
        BumpParameter(typeof(ChromaticAberration), _chromaticAberration.intensity, 0.5f * intensity, duration * 0.2f, duration * 0.5f);
        //BumpParameter(typeof(LensDistortion), _lensDistortion.intensity, -0.2f * intensity, duration *0.1f, duration *1.5f);
    }
}
