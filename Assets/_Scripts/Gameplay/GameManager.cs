using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, float> playTimes = new ();
    private Dictionary<string, int> lastHintIndexes = new ();

    public float GetLevelTime(string sceneName)
    {
        IncreaseSceneTimer();
        return playTimes[sceneName];
    }

    public int GetLastHintIndex(string sceneName)
    {
        IncreaseSceneTimer();
        return lastHintIndexes[sceneName];
    }

    public void SetLastHintIndex(string sceneName, int index)
    {
        IncreaseSceneTimer();
        lastHintIndexes[sceneName] = index;
    }
    
    private void Awake()
    {
        ServiceLocator.Register(this);
    }

    private void Update()
    {
        IncreaseSceneTimer();

        if (Input.GetKeyDown(KeyCode.T))
        {
            DebugPrintTimes();
        }
    }

    private void IncreaseSceneTimer()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (!playTimes.ContainsKey(currentScene))
        {
            playTimes.Add(currentScene, 0);
            lastHintIndexes.Add(currentScene, -1);
        }
        
        playTimes[currentScene] += Time.deltaTime;
    }

    private void DebugPrintTimes()
    {
        foreach (var entry in playTimes)
        {
            Debug.Log(entry.Key + ": " + entry.Value);
        }
    }
}
