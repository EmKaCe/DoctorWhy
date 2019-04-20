using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

    private Dictionary<int, GameObject> entityDictionary;

    private static EntityManager entityManager;

    public static EntityManager instance
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
        instance.entityDictionary.Add(id, pEntity);
    }

    public static void RemoveEntity(int entityID)
    {
        instance.entityDictionary.Remove(entityID);
    }

    public static Component GetComponent(int entityID, string componentName)
    {
        string test = componentName;
        Component c = new Component();
        if (instance.entityDictionary.ContainsKey(entityID))
        {
            GameObject g = instance.entityDictionary[entityID];
            c = g.GetComponent(test) as MonoBehaviour;
        }
        return c;
    }


    public static bool ContainsGameObject(int entityID)
    {
        bool containsObj;
        if (instance.entityDictionary.ContainsKey(entityID)){
            containsObj = true;
        }
        else
        {
            containsObj = false;
        }
        return containsObj;
    }

}
