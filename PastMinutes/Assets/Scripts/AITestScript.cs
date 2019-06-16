using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AITestScript : MonoBehaviour
{
    public Tilemap map;
    public GridLayout layout;
    public List<Tilemap> colliders;

    // Start is called before the first frame update
    void Start()
    {
        //PathfindingAlgorithm p = new PathfindingAlgorithm(layout, map);
        //p.FindPath(transform.position, new Vector3(2, 2, 0));
        AStarAlgorithm a = new AStarAlgorithm(layout, map, colliders);
        a.FindPath(new Vector3(90, 60, 0), new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
