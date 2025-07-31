using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SceneReference startingScene;
    [SerializeField] private float waitTime = 2f;

    private async void Start()
    {
        await Awaitable.WaitForSecondsAsync(waitTime);
        await ServiceLocator.Get<SceneTransitioner>().TransitionToMenuScene();
    }
}
