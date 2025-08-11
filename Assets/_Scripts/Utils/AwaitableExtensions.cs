using System;
using UnityEngine;

public static class AwaitableExtensions
{
    // Fire-and-forget with centralized exception handling
    public static async Awaitable RunSafe(this Awaitable awaitable, Action<Exception> onException = null)
    {
        try
        {
            await awaitable;
        }
        catch (OperationCanceledException)
        {
            Debug.Log($"Object destroyed, async cancelled");
        }
        catch (Exception ex)
        {
            onException?.Invoke(ex);
            Debug.LogException(ex);
        }
    }
}