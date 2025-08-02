using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour
{
    [SerializeField] private bool instaKill = false;
    [SerializeField] private float damage = 10f;
    public void OnCollisionEnter2D(Collision2D other)
    {
        OnTriggerEnter2D(other.collider);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Health health)) return;

        if (instaKill)
        {
            health.Kill();
            return;
        }
        health.TakeDamage(damage);
    }
}