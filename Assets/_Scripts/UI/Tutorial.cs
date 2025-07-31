using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels = new ();
    [SerializeField] private SceneReference gameScene;
    [SerializeField] private Button skipButton;

    private int _currentPanel;
    private void Awake()
    {
        TutorialButton.OnClicked += TutorialButtonOnClicked;
        skipButton?.onClick.AddListener(SkipButtonPressed);
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        StartTutorial();
    }

    private void OnDestroy()
    {
        TutorialButton.OnClicked -= TutorialButtonOnClicked;
        skipButton?.onClick.RemoveListener(SkipButtonPressed);
    }

    private void SkipButtonPressed()
    {
        EndTutorial();
    }
    private void TutorialButtonOnClicked()
    {
        Next();
    }

    private void StartTutorial()
    {
        _currentPanel = 0;
        panels[_currentPanel].SetActive(true);
    }

    private void Next()
    {
        panels[_currentPanel].SetActive(false);
        _currentPanel++;
        if (_currentPanel >= panels.Count)
        {
            EndTutorial();
            return;
        }
        panels[_currentPanel].SetActive(true);
    }

    private void EndTutorial()
    {
        _ = ServiceLocator.Get<SceneTransitioner>().TransitionToSceneAsync(gameScene);
    }
}