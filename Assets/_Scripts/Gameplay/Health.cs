using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    public event Action<GameObject> OnBeforeDeath;
    
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth  = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Kill()
    {
        TakeDamage(_currentHealth);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        OnBeforeDeath?.Invoke(gameObject);
    }
}