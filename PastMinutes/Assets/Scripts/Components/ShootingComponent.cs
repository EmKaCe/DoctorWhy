using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gets attached to gun sprite
/// </summary>
public class ShootingComponent : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private GunComponent activeGun;
    private string projectileSpritesheet;
    private Sprite projectile;
    public bool autoFire;
    private bool shooting;
    private GunComponent gun;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.TriggerEvent(EventSystem.InitializingShootingComponent(), gameObject.GetComponentInParent<EntityComponent>().gameObject.GetInstanceID(), new string[] { "0" });
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        projectile = Resources.Load<Sprite>("Items/" + projectileSpritesheet);
        Debug.Log(projectile);
        //work in progress: take gun component for projectile information
        gun = gameObject.GetComponent<GunComponent>() as GunComponent;

    }

    // Update is called once per frame
    void Update()
    {
        if (autoFire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shooting = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                shooting = false;
            }
            if (shooting)
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        
        
    }

    public void LoadSprite()
    {
        projectile = Resources.Load<Sprite>("Items/" + projectileSpritesheet);
    }

    public void Shoot()
    {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        GameObject bullet = new GameObject("Projectile");
        SpriteRenderer r = bullet.AddComponent<SpriteRenderer>();
        r.sprite = projectile;
        ProjectileComponent p = bullet.AddComponent<ProjectileComponent>();
        p.speed = 2;
        p.seconds = 10;
        p.direction = new Vector2(mouse.x, mouse.y);
        p.direction.Normalize();

    }

    public void SetActiveGun(GunComponent gun)
    {
        activeGun = gun;
    }

    public void SetProjectileSpritesheet(string spritesheet)
    {
        projectileSpritesheet = spritesheet;
    }
}
