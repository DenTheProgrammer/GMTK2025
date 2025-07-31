using UnityEngine;

public class ScreenFaderTarget : MonoBehaviour
{
    public static ScreenFaderTarget Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this); //component only
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}