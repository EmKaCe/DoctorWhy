using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ShootingSystem : MonoBehaviour
{

    private Dictionary<int, ShootingComponent> shootingComponentDictionary;

    private UnityAction<int, string[]> initializingListener;
    private UnityAction<int, string[]> gunSwitchedListener;
    private UnityAction<int, string[]> attachmentAddedListener;
    private UnityAction<int, string[]> attachmentRemovedListener;
    private UnityAction<int, string[]> attachmentChangedListener;
    private UnityAction<int, string[]> triggerPulledListener;
    private UnityAction<int, string[]> reloadGunListener;
    private UnityAction<int, string[]> drawGunListener;
    private UnityAction<int, string[]> stowGunListener;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void Awake()
    {
        shootingComponentDictionary = new Dictionary<int, ShootingComponent>();
        initializingListener = new UnityAction<int, string[]>(InitializeComponent);
        gunSwitchedListener = new UnityAction<int, string[]>(SwitchGun);
        attachmentAddedListener = new UnityAction<int, string[]>(AddAttachment);
        attachmentChangedListener = new UnityAction<int, string[]>(ChangeAttachment);
        attachmentRemovedListener = new UnityAction<int, string[]>(RemoveAttachment);
        triggerPulledListener = new UnityAction<int, string[]>(PullTrigger);
        reloadGunListener = new UnityAction<int, string[]>(Reload);
        drawGunListener = new UnityAction<int, string[]>(DrawGun);
        stowGunListener = new UnityAction<int, string[]>(StowGun);
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventSystem.InitializingShootingComponent(), initializingListener);
        EventManager.StartListening(EventSystem.GunSwitched(), gunSwitchedListener);
        EventManager.StartListening(EventSystem.AttachmentAdded(), attachmentAddedListener);
        EventManager.StartListening(EventSystem.AttachmentChanged(), attachmentChangedListener);
        EventManager.StartListening(EventSystem.AttachmentRemoved(), attachmentRemovedListener);
        EventManager.StartListening(EventSystem.TriggerPulled(), triggerPulledListener);
        EventManager.StartListening(EventSystem.Reload(), reloadGunListener);
        EventManager.StartListening(EventSystem.DrawGun(), drawGunListener);
        EventManager.StartListening(EventSystem.StowGun(), stowGunListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventSystem.InitializingShootingComponent(), initializingListener);
        EventManager.StopListening(EventSystem.GunSwitched(), gunSwitchedListener);
        EventManager.StopListening(EventSystem.AttachmentAdded(), attachmentAddedListener);
        EventManager.StopListening(EventSystem.AttachmentChanged(), attachmentChangedListener);
        EventManager.StopListening(EventSystem.AttachmentRemoved(), attachmentRemovedListener);
        EventManager.StopListening(EventSystem.TriggerPulled(), triggerPulledListener);
        EventManager.StopListening(EventSystem.Reload(), reloadGunListener);
        EventManager.StopListening(EventSystem.DrawGun(), drawGunListener);
        EventManager.StopListening(EventSystem.StowGun(), stowGunListener);
    }

    private void PullTrigger(int entityID, string[] empty)
    {
        ShootingComponent activeGun = GetActiveGun(entityID);
        if (activeGun != null)
        {
            activeGun.Shoot();
        }
    }

    private void RemoveAttachment(int entityID, string[] gun)
    {

    }

    private void ChangeAttachment(int entityID, string[] gun)
    {

    }

    private void AddAttachment(int entityID, string[] gun)
    {
        throw new NotImplementedException();
    }

    private void SwitchGun(int entityID, string[] gun)
    {
        ShootingComponent activeGun = GetActiveGun(entityID);
        SwitchGun(activeGun, EntityManager.GetEntityComponent<ShootingComponent>(int.Parse(gun[0])) as ShootingComponent);
    }

    private void SwitchGun(ShootingComponent currentlyActive, ShootingComponent goal)
    {
        if (currentlyActive != null)
        {
            currentlyActive.SetActiveGun(false);
            currentlyActive.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }       
        goal.SetActiveGun(true);
        goal.gameObject.GetComponent<SpriteRenderer>().enabled = true;

    }

    public void DrawGun(int entityID, string[] slot)
    {
        ShootingComponent activeGun = GetActiveGun(entityID);
        ShootingComponent otherGun = (EntityManager.GetEntityComponent<InventoryComponent>(entityID) as InventoryComponent).GetItemInSlot(int.Parse(slot[0])).gameObject.GetComponent<ShootingComponent>();
        if(activeGun != otherGun)
        {
            SwitchGun(activeGun, otherGun);
        }
       
    }

    public void StowGun(int entityID, string[] empty)
    {

        ShootingComponent[] comps = EntityManager.GetEntityComponent<EntityComponent>(entityID).gameObject.GetComponentsInChildren<ShootingComponent>();
        foreach(ShootingComponent s in comps)
        {
            s.SetActiveGun(false);
            s.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void InitializeComponent(int entityID, string[] nothing)
    {
        ShootingComponent comp = EntityManager.GetEntityComponent<ShootingComponent>(entityID) as ShootingComponent;
        shootingComponentDictionary.Add(entityID, comp);
        
    }

    public void Reload(int entityID, string[] empty)
    {
        ShootingComponent activeGun = GetActiveGun(entityID);
        if(activeGun != null)
        {
            activeGun.Reload(GetAmmunition(entityID, activeGun.GetAmmoType(), activeGun.GetMissingAmmo()));
        }
    }

    public int GetAmmunition(int entityID, PartFindingSystem.AmmoType ammoType, int neededAmount)
    {
        return (EntityManager.GetEntityComponent<InventoryComponent>(entityID) as InventoryComponent).GetAmmunition(ammoType, neededAmount);
    }

    /// <summary>
    /// Returns active shooting component;
    /// </summary>
    /// <param name="entityID"></param>
    /// <returns></returns>
    private ShootingComponent GetActiveGun(int entityID)
    {
        ShootingComponent[] comps = EntityManager.GetEntityComponent<EntityComponent>(entityID).gameObject.GetComponentsInChildren<ShootingComponent>();
        //if (EntityManager.GetEntityComponent<EntityComponent>(entityID).gameObject.GetComponentsInChildren<ShootingComponent>().Where(s => s.IsActiveGun()) is List<ShootingComponent> res)
        //{
        //    return res[0];
        //}
        if(comps != null)
        {
            foreach(ShootingComponent s in comps)
            {
                if (s.IsActiveGun())
                {
                    return s;
                }
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
