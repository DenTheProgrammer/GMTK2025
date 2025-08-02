using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();
    
    public static void Register<T>(T service, bool canOverride = false)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type) && !canOverride)
        {
            //Debug.LogWarning($"Service {type} is already registered.");
            return;
        }

        _services[type] = service;
    }
    
    /*public static void Unregister<T>(T service)
    {
        var type = typeof(T);
        _services.Remove(type);
    }*/

    public static T Get<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service;
        }

        throw new Exception($"Service {type} is not registered.");
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        _services.Clear();
    }
}