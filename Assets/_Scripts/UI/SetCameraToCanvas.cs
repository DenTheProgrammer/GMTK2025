using UnityEngine;

public class SetCameraToCanvas : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
