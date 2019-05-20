using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gets attached to gun sprite
/// </summary>
public class ShootingComponent : MonoBehaviour
{

    private bool activeGun = true;
    private string projectileSpritesheet;
    private Sprite projectile;
    public bool autoFire;
    private bool shooting;
    private GunComponent gun;
    private MagazineComponent mag;
    private AmmunitionComponent ammo;

    private float muzzleVelocity;
    


    // Start is called before the first frame update
    void Start()
    {
        EventManager.TriggerEvent(EventSystem.InitializingShootingComponent(), gameObject.GetComponentInParent<EntityComponent>().gameObject.GetInstanceID(), new string[] { "0" });
        projectile = Resources.Load<Sprite>("Items/" + projectileSpritesheet);
        Debug.Log(projectile);
        //work in progress: take gun component for projectile information
        gun = gameObject.GetComponent<GunComponent>() as GunComponent;       
        
        if(gun != null)
        {
            muzzleVelocity = gun.muzzleVelocity;
            mag = gun.magazine;
            if(mag != null)
            {
                ammo = mag.projectile;
                projectile = ammo.projectile;
            }
        }
        



    }

    // Update is called once per frame
    void Update()
    {
        if (activeGun)
        {
            if (autoFire)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //start coroutine
                    shooting = true;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    //stop coroutine
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


    }

    public void LoadSprite()
    {
        projectile = ammo.projectile;
    }

    public void Shoot()
    {
        if (mag.amountOfAmunition > 0)
        {
            mag.amountOfAmunition--;
            Vector3 mouse = Input.mousePosition;
            mouse = Camera.main.ScreenToWorldPoint(mouse);
            GameObject bullet = new GameObject("Projectile");
            Debug.Log(gameObject.name);
            bullet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1.5f);
            SpriteRenderer r = bullet.AddComponent<SpriteRenderer>();
            r.sprite = projectile;
            ProjectileComponent p = bullet.AddComponent<ProjectileComponent>();
            p.speed = muzzleVelocity;
            p.seconds = 10;
            p.direction = new Vector2(mouse.x, mouse.y);
            p.direction.Normalize();
        }
        

    }

    public void SetActiveGun(bool gun)
    {
        activeGun = gun;
    }

    public void SetProjectileSpritesheet(string spritesheet)
    {
        projectileSpritesheet = spritesheet;
    }
}
