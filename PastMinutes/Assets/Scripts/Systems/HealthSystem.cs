using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{

    UnityAction<int, string[]> takeDamageListener;
    UnityAction<int, string[]> healingListener;
    UnityAction<int, string[]> deathListener;

    private void Awake()
    {
        takeDamageListener = new UnityAction<int, string[]>(TakeDamage);
        healingListener = new UnityAction<int, string[]>(Heal);
        deathListener = new UnityAction<int, string[]>(KillEntity);
    }


    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(EventSystem.TakeDamage(), takeDamageListener);
        EventManager.StartListening(EventSystem.HealPerson(), healingListener);
        EventManager.StartListening(EventSystem.PersonDied(), deathListener);
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventSystem.TakeDamage(), takeDamageListener);
        EventManager.StartListening(EventSystem.HealPerson(), healingListener);
        EventManager.StartListening(EventSystem.PersonDied(), deathListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventSystem.TakeDamage(), takeDamageListener);
        EventManager.StopListening(EventSystem.HealPerson(), healingListener);
        EventManager.StopListening(EventSystem.PersonDied(), deathListener);
    }


    
    public void TakeDamage(int entityID, string[] damageAndDealer)
    {
        float.TryParse(damageAndDealer[0], out float damage);
        int.TryParse(damageAndDealer[1], out int dealerID);
        HealthComponent h = EntityManager.GetEntityComponent<HealthComponent>(entityID) as HealthComponent;
        if(h != null)
        {
            h.TakeDamage(damage, dealerID);
        }
        
    }

    public void Heal(int entityID, string[] healAndHealer)
    {
        float.TryParse(healAndHealer[0], out float heal);
        int.TryParse(healAndHealer[1], out int dealerID);
        (EntityManager.GetEntityComponent<HealthComponent>(entityID) as HealthComponent).Heal(heal, dealerID);
    }

    public void KillEntity(int entityID, string[] damageAndDealer)
    {

        HealthComponent h = EntityManager.GetEntityComponent<HealthComponent>(entityID) as HealthComponent;
        if(h != null)
        {
            Destroy(h.gameObject);
        }     
        EntityManager.RemoveEntity(entityID);
    }
}
