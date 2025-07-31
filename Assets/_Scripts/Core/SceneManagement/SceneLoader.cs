using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static event Action<string> OnSceneLoaded;
    public static async Awaitable LoadSceneAsync(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        await LoadSceneAsync(scene.SceneName, mode);
    }
    
    public static async Awaitable LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        await SceneManager.LoadSceneAsync(sceneName,  mode);
        Debug.Log($"Loaded scene '{sceneName}' with mode {mode}");
        OnSceneLoaded?.Invoke(sceneName);
    }

    public static async Awaitable UnloadSceneAsync(SceneReference scene)
    {
        await UnloadSceneAsync(scene.SceneName);
    }
    public static async Awaitable UnloadSceneAsync(string scene)
    {
        await SceneManager.UnloadSceneAsync(scene);
        Debug.Log($"Unloaded scene '{scene}'");
    }
    
    
}