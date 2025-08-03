using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private SceneReference loadingScene;
    [SerializeField] private SceneReference mainMenuScene;
    [SerializeField] private float minLoadingTime = 2f;
    [SerializeField] private LevelListSO levels;
    
    private bool _isTransitioning = false;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.N)) //Debug hotkeys
        {
            _ = TransitionToNextLevelAsync();
        }
#endif
        if (Input.GetKeyDown(KeyCode.R))
        {
            _ = ReloadCurrentScene();
        }
    }

    public async Awaitable TransitionToMenuScene()
    {
        await TransitionToSceneAsync(mainMenuScene);
    }

    public async Awaitable ReloadCurrentScene()
    {
        string curScene = SceneManager.GetActiveScene().name;
        await TransitionToSceneAsync(curScene);
    }
    
    public async Awaitable TransitionToNextLevelAsync()
    {
        int currentIndex = GetCurrentLevelIndex();
        if (currentIndex + 1 >= levels.levels.Count)
        {
            Debug.LogWarning($"Can't load next level (level index {currentIndex + 1} does not exist)");
            return;
        }
        
        await LoadLevelByIndex(currentIndex + 1);
    }
    
    public async Awaitable TransitionToSceneAsync(SceneReference scene)
    {
        await TransitionToSceneAsync(scene.SceneName);
    }

    public async Awaitable TransitionToSceneAsync(string sceneName)
    {
        if (_isTransitioning) return;
        _isTransitioning = true;
        float startTime = Time.time;
        
        await SceneLoader.LoadSceneAsync(loadingScene, LoadSceneMode.Additive);
        await screenFader.FadeOutAsync();
        await SceneLoader.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        await SceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(loadedScene);
        
        while (Time.time - startTime < minLoadingTime)
        {
            //Debug.Log($"time loading passed {Time.time - startTime}");
            await Awaitable.NextFrameAsync();
        }

        await screenFader.FadeInAsync(0.5f);
        await SceneLoader.UnloadSceneAsync(loadingScene);
        _isTransitioning = false;
    }
    
    private void Awake()
    {
        ServiceLocator.Register(this, false);
    }

    private async Awaitable LoadLevelByIndex(int levelIndex)
    {
        await TransitionToSceneAsync(levels.levels[levelIndex]);
    }
    
    private int GetCurrentLevelIndex()
    {
        int i = 0;
        string curSceneName = SceneManager.GetActiveScene().name;
        foreach (SceneReference sceneReference in levels.levels)
        {
            if (sceneReference.SceneName == curSceneName)
            {
                return i;
            }

            i++;
        }
        
        return -1;
    }
}