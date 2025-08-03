using UnityEngine;

public class EffectBlinkGlow : Effect
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float intensity = 1.6f;
    [SerializeField] private float repeatTimes = 3;
    [SerializeField] private float totalTime = 1f;
    public override async Awaitable ApplyEffect()
    {
        if (targetObject == null) targetObject = this.gameObject;
        if (repeatTimes == 0)
        {
            targetObject.GetComponent<Glow>().SetGlow(intensity);
            
            await Awaitable.NextFrameAsync();
            return;
        }
        await targetObject.GetComponent<Glow>().SetGlow(intensity, repeatTimes, totalTime/repeatTimes);
    }
}