using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gets attached to gun sprite
/// </summary>
public class ShootingComponent : MonoBehaviour
{

    private bool activeGun;
    private string projectileSpritesheet;
    private Sprite projectile;
    public bool autoFire;
    private bool shooting;
    private GunComponent gun;
    private MagazineComponent mag;
    private AmmunitionComponent ammo;

    private float muzzleVelocity;

    private float rotationZ;

    public GameObject baseProjectile;
    


    // Start is called before the first frame update
    void Start()
    {
        EventManager.TriggerEvent(EventSystem.InitializingShootingComponent(), gameObject.GetComponentInParent<EntityComponent>().gameObject.GetInstanceID(), new string[] { "0" });
        projectile = Resources.Load<Sprite>("Items/" + projectileSpritesheet);
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
            rotationZ = CalculateRotation(Input.mousePosition, transform.position);
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationZ));
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
            Vector3 rotation = new Vector3(0, 0, rotationZ);
            //Create bullet
            GameObject bullet = Instantiate(baseProjectile);
            //GameObject bullet = new GameObject("Projectile");
            //Pos equals gunPos
            bullet.transform.position = new Vector3(gameObject.transform.position.x + gun.xOffset, gameObject.transform.position.y + gun.yOffset, -1.5f);
            //Adding Sprite
            SpriteRenderer r = bullet.GetComponent<SpriteRenderer>();
            r.sprite = projectile;
            //SetRotation;
            r.transform.Rotate(rotation);
            //Shoot bullet
            ProjectileComponent p = bullet.GetComponent<ProjectileComponent>();
            p.ownerID = gameObject.GetComponentInParent<EntityComponent>().entityID;
            //damage: mass times speed
            p.damage = ammo.weight * Mathf.Pow(gun.muzzleVelocity, 2) / 2;
            p.speed = muzzleVelocity;
            p.seconds = 10;
            p.direction = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
            p.direction.Normalize();
        }
        

    }

    private float CalculateRotation(Vector3 mousePos, Vector3 playerPos)
    {
        mousePos = Input.mousePosition;
        mousePos.z = 0;
        playerPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - playerPos.x;
        mousePos.y = mousePos.y - playerPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        return angle;
    }

    public void SetActiveGun(bool gun)
    {
        activeGun = gun;
    }

    public bool IsActiveGun()
    {
        return activeGun;
    }

    public void SetProjectileSpritesheet(string spritesheet)
    {
        projectileSpritesheet = spritesheet;
    }
}
