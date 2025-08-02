using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HintSystem : MonoBehaviour
{
    [SerializeField] private Transform hintsContainer;
    [SerializeField] private float hintReminderEvery = 120f;
    [SerializeField] private float firstHintReminderAfter = 30f;
    [SerializeField] private float hintReminderVisibilityTime = 3f;
    [SerializeField] private int hintReminderBlinkTimes = 3;
    [SerializeField] private CanvasGroup hintReminder;
    [SerializeField] private Transform confirmationWindow;
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Button cancelBtn;
    [SerializeField] private TextMeshProUGUI hintConfirmText;
    
    
    private List<Transform> _hints = new ();
    private GameManager _gameManager;
    private float _lastTimeReminded;
    private string _currentSceneName;
    private float _hintReminderAlpha;
    private void Start()
    {
        foreach (Transform child in hintsContainer)
        {
            _hints.Add(child);
        }
        foreach (var hint in _hints)
        {
            hint.gameObject.SetActive(false);
        }

        _lastTimeReminded = firstHintReminderAfter - hintReminderEvery;
        _gameManager = ServiceLocator.Get<GameManager>();
        _currentSceneName = SceneManager.GetActiveScene().name;
        
        cancelBtn.onClick.AddListener(OnCancelButtonClick);
        confirmBtn.onClick.AddListener(OnConfirmButtonClick);
        
        hintsContainer.gameObject.SetActive(true);
        _hintReminderAlpha = hintReminder.alpha; 
        hintReminder.alpha = 0f;
        ShowConfirmationWindow(false);
        
        UpdateHintsVisibility();
    }

    private void OnConfirmButtonClick()
    {
        ShowConfirmationWindow(false);
        ShowNextHint();
    }

    private void ShowNextHint()
    {
        int lastHintShown = _gameManager.GetLastHintIndex(_currentSceneName);
        
        _gameManager.SetLastHintIndex(_currentSceneName, lastHintShown + 1);
        
        UpdateHintsVisibility();
    }

    private void UpdateHintsVisibility()
    {
        int lastVisibleHint = _gameManager.GetLastHintIndex(_currentSceneName);
        Debug.Log($"lastVisibleHint for scene {_currentSceneName} index: {lastVisibleHint}");
        int i = 0;
        
        foreach (var hint in _hints)
        {
            if (i <= lastVisibleHint)
            {
                hint.gameObject.SetActive(true);
            }

            i++;
        }
    }

    private void OnDestroy()
    {
        cancelBtn.onClick.RemoveListener(OnCancelButtonClick);
        confirmBtn.onClick.RemoveListener(OnConfirmButtonClick);
    }

    private void OnCancelButtonClick()
    {
        ShowConfirmationWindow(false);
    }

    private void Update()
    {
        if (_gameManager.GetLevelTime(_currentSceneName) - _lastTimeReminded > hintReminderEvery)
        {
            _lastTimeReminded = _gameManager.GetLevelTime(_currentSceneName);
            _ = ShowHintReminder();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleConfirmVisibility();
        }
    }

    private void ToggleConfirmVisibility()
    { 
        ShowConfirmationWindow(!confirmationWindow.gameObject.activeSelf);
    }

    private void ShowConfirmationWindow(bool show)
    {
        int hintCount = _hints.Count;
        int nextHintIndex = _gameManager.GetLastHintIndex(_currentSceneName) + 2;
        
        if (nextHintIndex - 1 >= hintCount) return;
        
        confirmationWindow.gameObject.SetActive(show);

        hintConfirmText.text = $"Show hint? ({nextHintIndex}/{hintCount})";
    }

    private async Awaitable ShowHintReminder()
    {
        int hintCount = _hints.Count;
        int nextHintIndex = _gameManager.GetLastHintIndex(_currentSceneName) + 2;
        
        if (nextHintIndex - 1 >= hintCount) return;
        
        int iterations = hintReminderBlinkTimes;
        float halfDuration = hintReminderVisibilityTime / iterations / 2f;

        for (int i = 0; i < iterations; i++)
        {
            await hintReminder
                .DOFade(_hintReminderAlpha, halfDuration)
                .AsyncWaitForCompletion(); // Fade In
            
            await hintReminder
                .DOFade(0f, halfDuration)
                .AsyncWaitForCompletion(); // Fade Out
        }
    }

}
