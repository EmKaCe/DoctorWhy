using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(FactionComponent))]
public class AttackComponent : MonoBehaviour
{
    private FactionComponent ownFaction;
    private Transform enemy;
    private bool chase;
    AStarAlgorithm a;
    // Start is called before the first frame update
    void Start()
    {
        //RenderSettings.ambientLight = Color.black;
        ownFaction = gameObject.GetComponentInParent<FactionComponent>();
        chase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (chase)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Faction own = ownFaction.GetFaction();
        FactionComponent factionComp;
        if((factionComp = collision.gameObject.GetComponentInParent<FactionComponent>()) != null)
        {
            Faction faction = factionComp.GetFaction();
            if (own.factionName.Equals(faction.factionName))
            {

            }
            if (own.hostile.Contains(faction))
            {
                Attack(collision.transform);
            }
        }
    }

    public void Attack(Transform enemy)
    {
        chase = true;
        Debug.Log(enemy.name);
        (gameObject.GetComponent<PatrollingComponent>() as PatrollingComponent).enabled = false;
        this.enemy = enemy;

    }
}
