using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    private List<Condition> conditions;
    private void Awake()
    {
        conditions = new List<Condition>(GetComponentsInChildren<Condition>());
    }

    private void LateUpdate()
    {
        _ = CheckConditions();
    }

    private async Awaitable CheckConditions()
    {
        foreach (var condition in conditions)
        {
            if (condition.CheckCondition())
            {
                try
                {
                    await condition.HandleConditionPassed();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                break;
            }
        }
    }
}
