using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();
    
    public static void Register<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            //Debug.LogWarning($"Service {type} is already registered.");
            return;
        }

        _services[type] = service;
    }

    public static T Get<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service;
        }
        
        foreach (Type key in _services.Keys)
        {
            var value = _services[key];
            // key is a base/interface of requested type (e.g., key = IAudioPlayer, T = AudioManager)
            // Only return if the instance actually is T
            if (key.IsAssignableFrom(type) && value is T cast)
                return cast;
        }
        
        throw new Exception($"Service {type} is not registered.");
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        _services.Clear();
    }
}