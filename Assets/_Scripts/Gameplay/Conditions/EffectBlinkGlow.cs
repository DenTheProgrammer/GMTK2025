using UnityEngine;

[RequireComponent(typeof(Glow))]
public class EffectBlinkGlow : Effect
{
    [SerializeField] private float intensity = 1.6f;
    [SerializeField] private float repeatTimes = 3;
    [SerializeField] private float totalTime = 1f;
    public override async Awaitable ApplyEffect()
    {
        await GetComponent<Glow>().SetGlow(intensity, repeatTimes, totalTime/repeatTimes);
    }
}