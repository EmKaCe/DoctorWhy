using UnityEngine;

public class EntityComponent : MonoBehaviour {


    public int entityID;
    private int test;

    // Use this for initialization
    void Start () {
        
        if (!EntityManager.ContainsGameObject(gameObject.GetInstanceID())){
            EntityManager.AddEntity(gameObject);          
        }
        entityID = gameObject.GetInstanceID();
    }

    void Awake()
    {

        if (!EntityManager.ContainsGameObject(gameObject.GetInstanceID()))
        {
            EntityManager.AddEntity(gameObject);
        }

    }

    /// <summary>
    /// Removes entity from EntityManager
    /// </summary>
    public void Delete()
    {
        EntityManager.RemoveEntity(gameObject.GetInstanceID());
    }

    public void OnDestroy()
    {
        EntityManager.RemoveEntity(gameObject.GetInstanceID());
    }

}
