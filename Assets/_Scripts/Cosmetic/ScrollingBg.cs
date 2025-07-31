using UnityEngine;

public class ScrollingBg : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private Vector2 scrollDirection = Vector2.one;
    

    private void Update()
    {
        meshRenderer.materials[0].mainTextureOffset += scrollDirection * Time.deltaTime * scrollSpeed;
    }
}
