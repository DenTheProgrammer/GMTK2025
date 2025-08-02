using System.Threading.Tasks;
using UnityEngine;

public class ConditionButtonPressed : Condition
{
    [SerializeField] private KeyCode button;

    public override bool CheckCondition()
    {
        return Input.GetKeyDown(button);
    }
}