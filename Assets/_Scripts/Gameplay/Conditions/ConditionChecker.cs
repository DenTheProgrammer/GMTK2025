/*
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    private List<ConditionBlock> conditionBlocks;
    private void Awake()
    {
        conditionBlocks = new List<ConditionBlock>(GetComponentsInChildren<ConditionBlock>());
    }

    private void LateUpdate()
    {
        _ = CheckConditions();
    }

    private async Awaitable CheckConditions()
    {
        foreach (var conditionBlock in conditionBlocks)
        {
            if (conditionBlock.co)
            {
                try
                {
                    await conditionBlock.HandleConditionPassed();
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
*/
