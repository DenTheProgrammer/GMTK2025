using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour
{
    [SerializeField] private bool instaKill = false;
    [SerializeField] private SoundData damageSound;
    [SerializeField] private float damage = 10f;
    
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = ServiceLocator.Get<AudioManager>();
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        OnTriggerStay2D(other.collider);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!enabled) return;
        if (!other.TryGetComponent(out Health health)) return;

        if (damageSound)
        {
            _audioManager.Play(damageSound, transform.position);
        }
        if (instaKill)
        {
            health.Kill();
            return;
        }
        health.TakeDamage(damage);
    }
}