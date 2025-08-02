using UnityEngine;

public class EffectRestartLevel : Effect
{

    public override async Awaitable ApplyEffect()
    {
        await ServiceLocator.Get<SceneTransitioner>().ReloadCurrentScene();
    }
}