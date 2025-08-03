using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusic : MonoBehaviour
{
    [SerializeField] private SoundData bgMusic;
    [SerializeField] private SceneReference startFromScene;
        
    
    private AudioManager _audioManager;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioManager = ServiceLocator.Get<AudioManager>();
        SceneLoader.OnSceneLoaded += SceneLoaderOnSceneLoaded;
        TryStartMusic(SceneManager.GetActiveScene().name);
    }

    private void SceneLoaderOnSceneLoaded(string sceneName)
    {
        TryStartMusic(sceneName);
    }

    private void TryStartMusic(string currentSceneName)
    {
        if (_audioSource != null) return;
        if (currentSceneName == startFromScene)
        {
            _audioSource = _audioManager.Play(bgMusic, Vector3.zero);
        }
    }

    private void OnDestroy()
    {
        if (_audioManager != null)
        {
            _audioManager.StopSound(_audioSource);
        }
        SceneLoader.OnSceneLoaded -= SceneLoaderOnSceneLoaded;
    }
}
