using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button closeButton;
    
    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    private void Awake()
    {
        ServiceLocator.Register(this, false);
        settingsPanel.SetActive(false);
        closeButton.onClick.AddListener(ToggleSettingsPanel);
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(ToggleSettingsPanel);
    }

}
