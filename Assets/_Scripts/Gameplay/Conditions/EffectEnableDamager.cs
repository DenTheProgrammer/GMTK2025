using UnityEngine;

public class EffectEnableDamager : Effect
{
    [SerializeField] private Damager damager;
    [SerializeField] private bool enable;

    public override Awaitable ApplyEffect()
    {
        damager.enabled = enable;
        return Awaitable.NextFrameAsync();
    }
}
