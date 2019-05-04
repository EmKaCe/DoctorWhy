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

    private void Start()
    {
        Debug.Log("Hallo");
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

    public static Component GetEntityComponent(int entityID, string componentName)
    {
        string test = componentName;
        Component c = new Component();
        if (Instance.entityDictionary.ContainsKey(entityID))
        {
            GameObject g = Instance.entityDictionary[entityID];
            c = g.GetComponent(test) as MonoBehaviour;
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
