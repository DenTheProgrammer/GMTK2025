
using UnityEngine;

public class ConditionCanSee : Condition
{
    private Settings _settings;
    private void Start()
    {
        _settings = ServiceLocator.Get<Settings>();
        ResetBrigtness();
    }

    public override bool CheckCondition()
    {
        return !Mathf.Approximately(_settings.brightnessImage.color.a, 1f);
    }

    private void OnDestroy()
    {
        ResetBrigtness();
    }

    private void ResetBrigtness()
    {
        if (_settings == null || _settings.brightnessImage == null) return;
        
        _settings.brightness.value = 1f;    
        Color color = Color.black;
        color.a = 0;
        _settings.brightnessImage.color = color;
    }
}
