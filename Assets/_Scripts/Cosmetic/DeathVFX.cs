using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DeathVFX : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Health>().OnBeforeDeath += OnBeforeDeath;
    }

    private void OnDestroy()
    {
        GetComponent<Health>().OnBeforeDeath -= OnBeforeDeath;
    }
    
    private async void OnBeforeDeath(GameObject obj)
    {
        obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        obj.GetComponent<PlayerController>().EnableControls(false);
        obj.GetComponent<Collider2D>().enabled = false;
        Debug.Log($"Before Death: {obj.name}");
        await Awaitable.WaitForSecondsAsync(2f);
        DestroyImmediate(obj);
        Debug.Log($"After Death");
        _ = ServiceLocator.Get<SceneTransitioner>().ReloadCurrentScene();
    }
}
