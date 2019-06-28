using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPanelUI : MonoBehaviour
{
    public GameObject SlotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 12; i++)
        {
            Instantiate(SlotPrefab).transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
