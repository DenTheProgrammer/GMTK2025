using System.Collections.Generic;
using UnityEngine;

public class EffectObjectsSetActive : Effect
{
    [SerializeField] private bool activate;
    [SerializeField] private List<GameObject> objectsToActivate;
    public override async Awaitable ApplyEffect()
    {
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(activate);
        }
        await Awaitable.NextFrameAsync();
    }
}
