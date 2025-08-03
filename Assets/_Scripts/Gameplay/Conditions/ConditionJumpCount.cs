using UnityEngine;

public class ConditionJumpCount : Condition
{
    [SerializeField] private int requiredJumpCount;
    
    private int _jumpCount;
    private void Start()
    {
        PlayerController.OnJump += OnJump;
    }

    private void OnDestroy()
    {
        PlayerController.OnJump -= OnJump;
    }

    private void OnJump(bool inJump)
    {
        if (inJump == true)
        {
            _jumpCount++;
            Debug.Log("Jump Count: " + _jumpCount);
        }
    }

    public override bool CheckCondition()
    {
        return _jumpCount >= requiredJumpCount;
    }
}
