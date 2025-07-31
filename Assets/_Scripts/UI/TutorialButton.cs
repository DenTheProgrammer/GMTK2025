using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TutorialButton : MonoBehaviour
{
    public static event Action OnClicked;

    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        _button.interactable = false;
        OnClicked?.Invoke();
    }
}