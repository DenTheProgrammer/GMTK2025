using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public abstract Awaitable ApplyEffect();
}