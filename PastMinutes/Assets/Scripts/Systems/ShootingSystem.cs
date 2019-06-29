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
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventSystem.InitializingShootingComponent(), initializingListener);
        EventManager.StartListening(EventSystem.GunSwitched(), gunSwitchedListener);
        EventManager.StartListening(EventSystem.AttachmentAdded(), attachmentAddedListener);
        EventManager.StartListening(EventSystem.AttachmentChanged(), attachmentChangedListener);
        EventManager.StartListening(EventSystem.AttachmentRemoved(), attachmentRemovedListener);
        EventManager.StartListening(EventSystem.TriggerPulled(), triggerPulledListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventSystem.InitializingShootingComponent(), initializingListener);
        EventManager.StopListening(EventSystem.GunSwitched(), gunSwitchedListener);
        EventManager.StopListening(EventSystem.AttachmentAdded(), attachmentAddedListener);
        EventManager.StopListening(EventSystem.AttachmentChanged(), attachmentChangedListener);
        EventManager.StopListening(EventSystem.AttachmentRemoved(), attachmentRemovedListener);
        EventManager.StopListening(EventSystem.TriggerPulled(), triggerPulledListener);
    }

    private void PullTrigger(int entityID, string[] gun)
    {
        throw new NotImplementedException();
    }

    private void RemoveAttachment(int entityID, string[] gun)
    {
        throw new NotImplementedException();
    }

    private void ChangeAttachment(int entityID, string[] gun)
    {
        throw new NotImplementedException();
    }

    private void AddAttachment(int entityID, string[] gun)
    {
        throw new NotImplementedException();
    }

    private void SwitchGun(int entityID, string[] gun)
    {
        throw new NotImplementedException();
    }

    private void InitializeComponent(int entityID, string[] nothing)
    {
        ShootingComponent comp = EntityManager.GetEntityComponent<ShootingComponent>(entityID) as ShootingComponent;
        shootingComponentDictionary.Add(entityID, comp);
        
    }

    public void Reload(int entityID, string[] nothing)
    {

    }

    /// <summary>
    /// Returns active shooting component;
    /// </summary>
    /// <param name="entityID"></param>
    /// <returns></returns>
    private ShootingComponent GetActiveGun(int entityID)
    {
        return  EntityManager.GetEntityComponent<EntityComponent>(entityID).gameObject.GetComponentsInChildren<ShootingComponent>().Where(s => s.IsActiveGun()).First();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
