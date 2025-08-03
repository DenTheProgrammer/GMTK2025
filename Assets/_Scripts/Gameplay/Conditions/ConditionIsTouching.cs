using System.Collections.Generic;
using UnityEngine;

public class ConditionIsTouching : Condition
{
    [SerializeField] private Collider2D object1;
    [SerializeField] private List<Collider2D> objects;


    public override bool CheckCondition()
    {
        foreach (var obj in objects)
        {
            if (obj.IsTouching(object1))
            {
                return true;
            }
            
        }
        return false;
    }
}
