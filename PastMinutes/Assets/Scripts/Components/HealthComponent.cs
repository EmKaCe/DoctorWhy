using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void TakeDamage(float damage, int enemyID)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            EventManager.TriggerEvent(EventSystem.PersonDied(), gameObject.GetComponent<EntityComponent>().entityID, new string[] { damage.ToString(), enemyID.ToString() });
            gameObject.GetComponents<Collider>().Select(c => c.enabled = false);
        }
        else
        {
            EventManager.TriggerEvent(EventSystem.DamageTaken(), gameObject.GetComponent<EntityComponent>().entityID, new string[] { damage.ToString(), enemyID.ToString() });
        }
    }

    public void Heal(float heal, int healerID)
    {
        currentHealth += heal;
        if(currentHealth > health)
        {
            currentHealth = health;
        }
        EventManager.TriggerEvent(EventSystem.PersonWasHealed(), gameObject.GetComponent<EntityComponent>().entityID, new string[] { heal.ToString(), healerID.ToString() });
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
