using UnityEngine;
using UnityEngine.UI;

public class ConditionUiButtonPressed : Condition
{
    [SerializeField] private Button button;

    private bool _wasPressedThisFrame;
    private void Awake()
    {
        button.onClick.AddListener(ButtonPressed);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(ButtonPressed);
    }

    public override bool CheckCondition()
    {
        if (_wasPressedThisFrame)
        {
            _wasPressedThisFrame = false;
            return true;
        }

        return false;
    }

    private void ButtonPressed()
    {
        _wasPressedThisFrame = true;
    }
}
