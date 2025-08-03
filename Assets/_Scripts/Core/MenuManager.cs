using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private SceneReference firstPlayScene;

    private void Awake()
    {
        playButton.onClick.AddListener(HandlePlayButtonClicked);
        settingsButton.onClick.AddListener(HandleSettingsButtonClicked);
    }

    private void HandleSettingsButtonClicked()
    {
        ServiceLocator.Get<SettingsUI>().ToggleSettingsPanel();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(HandlePlayButtonClicked);
        settingsButton.onClick.RemoveListener(HandleSettingsButtonClicked);
    }

    private void HandlePlayButtonClicked()
    {
        playButton.interactable = false;
        //await ServiceLocator.Get<SceneTransitioner>().TransitionToSceneAsync(firstPlayScene);
    }
}
