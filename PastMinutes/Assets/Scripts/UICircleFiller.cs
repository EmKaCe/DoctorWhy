using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICircleFiller : MonoBehaviour
{

    public Image fillingCircle;
    public float fillingValue;
    // Start is called before the first frame update
    void Start()
    {
        if(fillingCircle == null)
        {
            Debug.Log("Circle to fill not set. Please add gameObject to fillingCircle.");
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        fillingCircle.fillAmount = fillingValue;
    }
}
