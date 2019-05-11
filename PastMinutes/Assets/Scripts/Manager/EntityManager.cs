using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

    private Dictionary<int, GameObject> entityDictionary;

    private static EntityManager entityManager;

    public static EntityManager Instance
    {
        get
        {
            if (!entityManager)
            {
                entityManager = FindObjectOfType(typeof(EntityManager)) as EntityManager;

                if (!entityManager)
                {
                    Debug.LogError("There needs to be one active EntityManager script on a GameObject in your scene");
                }
                else
                {
                    entityManager.Init();
                }
            }
            return entityManager;
        }
    }

    void Init()
    {
        if (entityDictionary == null)
        {
            entityDictionary = new Dictionary<int, GameObject>();
        }
    }



    public static void AddEntity(GameObject pEntity)
    {
        int id = pEntity.GetInstanceID();
        Instance.entityDictionary.Add(id, pEntity);
    }

    public static void RemoveEntity(int entityID)
    {
        Instance.entityDictionary.Remove(entityID);
    }

    /// <summary>
    /// searches only obj for component
    /// </summary>
    /// <param name="entityID"></param>
    /// <param name="componentName"></param>
    /// <returns></returns>
    public static Component GetEntityComponent(int entityID, string componentName)
    {
        Component c = new Component();
        if (Instance.entityDictionary.ContainsKey(entityID))
        {
            c = Instance.entityDictionary[entityID].GetComponent(componentName) as MonoBehaviour;
        }
        return c;
    }

    /// <summary>
    /// Searches obj and children
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entityID"></param>
    /// <returns></returns>
    public static Component GetEntityComponent<T>(int entityID)
    {
        Component c = new Component();
        if (Instance.entityDictionary.ContainsKey(entityID))
        {
            c =  Instance.entityDictionary[entityID].GetComponentInChildren<T>() as MonoBehaviour;
        }
        return c;
    }


    public static bool ContainsGameObject(int entityID)
    {
        bool containsObj;
        if (Instance.entityDictionary.ContainsKey(entityID)){
            containsObj = true;
        }
        else
        {
            containsObj = false;
        }
        return containsObj;
    }

}
