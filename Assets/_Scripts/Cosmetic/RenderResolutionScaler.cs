using UnityEngine;

public class RenderResolutionScaler : MonoBehaviour
{
    [SerializeField] private Vector2 resolution;
    [SerializeField] private Material material;
    
    private static readonly int Resolution = Shader.PropertyToID("_Resolution");
    
    private void Awake()
    {
        SetResolution();
    }

    private void OnValidate()
    {
        SetResolution();
    }

    private void SetResolution()
    {
        Vector2 res = material.GetVector(Resolution);
        
        material.SetVector(Resolution, resolution);
    }
}
