using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float health;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(float damage, int damageCauseID)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            EventSystem.PersonDied(gameObject.GetInstanceID(), new string[] { damageCauseID.ToString() });
        }
    }

    public void Heal(float heal, int healerID)
    {
        currentHealth += heal;
        if(currentHealth > health)
        {
            currentHealth = health;
        }
        EventSystem.PersonWasHealed(gameObject.GetInstanceID(), new string[] { healerID.ToString() });
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
