using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AITestScript : MonoBehaviour
{
    public Tilemap map;
    public GridLayout layout;

    // Start is called before the first frame update
    void Start()
    {
        PathfindingAlgorithm p = new PathfindingAlgorithm(layout, map);
        p.FindPath(transform.position, new Vector3(2, 2, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
