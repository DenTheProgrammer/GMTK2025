using System.Collections.Generic;
using UnityEngine;

public class PersistentRoot : MonoBehaviour
{
    [SerializeField] List<Behaviour> spammingComponents;
    private static PersistentRoot _instance;
    
    private void Awake()
    {
        ToggleComponents(false);
        
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        ToggleComponents(true);
        DontDestroyOnLoad(gameObject);
    }

    private void ToggleComponents(bool enable)
    {
        foreach (var behaviour in spammingComponents)
        {
            behaviour.enabled = enable;
        }
    }
}
