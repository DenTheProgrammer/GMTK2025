using UnityEngine;

public class EffectTeleport : Effect
{
    [SerializeField] Transform target;
    [SerializeField] Transform teleportTo;
    
    public override async Awaitable ApplyEffect()
    {
        target.position = teleportTo.position;
        await Awaitable.NextFrameAsync();
    }
}