using UnityEngine;

public class ConditionButtonPressedRestartLevel : Condition
{
    [SerializeField] private KeyCode button;
    [SerializeField] private Glow glow;
    
    private bool conditionAlreadyPassed;
    public override bool CheckCondition()
    {
        if (conditionAlreadyPassed)
        {
            return false;
        }
        return Input.GetKeyDown(button);
    }

    public override async Awaitable HandleConditionPassed()
    {
        conditionAlreadyPassed = true;
        await glow.SetGlow(1.6f, 3, 0.33f);
        await ServiceLocator.Get<SceneTransitioner>().ReloadCurrentScene();
        conditionAlreadyPassed = false;
    }
}