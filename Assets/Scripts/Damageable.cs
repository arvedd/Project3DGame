using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public EnemySpawner spawner;

    void Start()
    {
        currentHealth = maxHealth;
    }
    

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (CompareTag("Enemy"))
        {
            StopAllCoroutines();
            spawner.Spawn();
            Destroy(gameObject);
        }
        else if (CompareTag("Player"))
        {
            Debug.Log("Mati");
        }
    }
}
