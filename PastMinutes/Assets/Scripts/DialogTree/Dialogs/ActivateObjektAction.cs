using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ActivateObjektAction", menuName = "NPCAction/ActivateObjektAction")]
public class ActivateObjektAction : NPCAction
{
    public GameObject obj;
    public override void Act()
    {
        Instantiate(obj.gameObject).SetActive(true);
    }
}
