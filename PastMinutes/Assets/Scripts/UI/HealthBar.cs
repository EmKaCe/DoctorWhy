using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public HealthComponent healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = healthComponent.health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = healthComponent.GetCurrentHealth();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            healthComponent.TakeDamage(10f, 0);
            Debug.Log(healthComponent.GetCurrentHealth());
        }
    }
}
