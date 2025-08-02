using UnityEngine;

public class NOT_Condition : Condition
{
    [SerializeField] private Condition condition;

    public override bool CheckCondition()
    {
        return !condition.CheckCondition();
    }
}