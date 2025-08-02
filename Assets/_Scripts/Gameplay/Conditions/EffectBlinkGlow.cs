using UnityEngine;

public class EffectBlinkGlow : Effect
{
    [SerializeField] private Glow glow;
    [SerializeField] private float intensity = 1.6f;
    [SerializeField] private float repeatTimes = 3;
    [SerializeField] private float totalTime = 1f;
    public override async Awaitable ApplyEffect()
    {
        await glow.SetGlow(intensity, repeatTimes, totalTime/repeatTimes);
    }
}