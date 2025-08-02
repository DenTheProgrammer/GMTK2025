using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionBlock : MonoBehaviour
{
    [SerializeField] Condition condition;
    private List<Effect> effects;

    private bool _effectsInProgress;

    private void Awake()
    {
        effects = new List<Effect>(GetComponents<Effect>());
    }

    private void Update()
    {
        if (condition.CheckCondition())
        {
            if (_effectsInProgress) return;
            _effectsInProgress = true;
            
            foreach (var effect in effects)
            {
                try
                {
                    _ = effect.ApplyEffect();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            _effectsInProgress = false;
        }
    }
}