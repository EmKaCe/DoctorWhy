using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelComponent : MonoBehaviour
{
    public GameObject present;
    public GameObject past;
    // Start is called before the first frame update
    void Start()
    {
        if(present == null || past == null)
        {
            Debug.Log("TimeTravelComponent needs a present and past object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TravelTime();
        }
    }

    private void TravelTime()
    {
        present.SetActive(!present.activeSelf);
        past.SetActive(!past.activeSelf);

    }
}
