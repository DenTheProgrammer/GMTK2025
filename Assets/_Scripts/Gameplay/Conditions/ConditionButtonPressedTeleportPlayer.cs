using UnityEngine;

public class ConditionButtonPressedTeleportPlayer : Condition
{
    [SerializeField] private KeyCode button;
    [SerializeField] private Glow glow;
    [SerializeField] private Transform teleportTo;
    [SerializeField] private PlayerController playerController;
    
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
        playerController.transform.position = teleportTo.position;
        conditionAlreadyPassed = false;
    }
}