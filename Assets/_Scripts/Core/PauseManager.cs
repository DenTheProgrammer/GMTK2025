using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Transform pausePanel;
    [SerializeField] private Button pauseCloseButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;
    
    
    private bool _isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void Awake()
    {
        pausePanel.gameObject.SetActive(false);
        pauseCloseButton.onClick.AddListener(TogglePause);
        settingsButton.onClick.AddListener(ToggleSettingsPanel);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void ToggleSettingsPanel()
    {
        ServiceLocator.Get<SettingsUI>().ToggleSettingsPanel();
    }

    private void ReturnToMainMenu()
    {
        TogglePause();
        _ = ServiceLocator.Get<SceneTransitioner>().TransitionToMenuScene();
    }

    private void OnDestroy()
    {
        pauseCloseButton.onClick.RemoveListener(TogglePause);
        settingsButton.onClick.RemoveListener(ToggleSettingsPanel);
        mainMenuButton.onClick.RemoveListener(ReturnToMainMenu);
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;
        pausePanel.gameObject.SetActive(_isPaused);
        
        //Time.timeScale = _isPaused ? 0 : 1;
    }
}
