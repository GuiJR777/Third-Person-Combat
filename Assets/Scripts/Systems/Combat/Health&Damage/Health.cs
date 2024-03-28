using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damageValue)
    {
        if (isDead) return;

        currentHealth -= damageValue;

        Debug.Log($"{name} lose {damageValue} HP");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            Debug.Log($"{name} is dead");
        }
    }

}
