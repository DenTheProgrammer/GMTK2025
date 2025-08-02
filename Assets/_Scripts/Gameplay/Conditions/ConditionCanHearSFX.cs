using UnityEngine;

public class ConditionCanHearSFX : Condition
{
    private Settings _settings;
    private void Start()
    {
        _settings = ServiceLocator.Get<Settings>();
        _settings.sfxVolume.value = 1;
    }

    public override bool CheckCondition()
    {
        bool result = _settings.sfxVolume.value > 0.01f;
        //Debug.Log($"sfx volume {_settings.sfxVolume.value}, result: {result}");
        return result;
    }

    private void OnDestroy()
    {
        _settings.sfxVolume.value = 1;
    }
}