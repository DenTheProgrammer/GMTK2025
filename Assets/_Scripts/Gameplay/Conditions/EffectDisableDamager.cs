using UnityEngine;

public class EffectDisableDamager : Effect
{
    [SerializeField] private Damager damager;


    public override Awaitable ApplyEffect()
    {
        Destroy(damager);
        return Awaitable.NextFrameAsync();
    }
}
