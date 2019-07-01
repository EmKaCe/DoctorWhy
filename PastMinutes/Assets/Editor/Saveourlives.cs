using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DialogTree;

public class Saveourlives : MonoBehaviour
{
    public NodeSaver[] nodes;
    public StartBehaviourNode[] behaviour;
    
    // Start is called before the first frame update
    void Start()
    {
        int baseDialog = 0, startDialog = 0, pre = 0, npc = 0, exit = 0, dial = 0, beh = 0, connection = 0, sel = 0, seq = 0, con = 0, goTo = 0, star = 0, it = 0, inv = 0, ene = 0, att = 0;
        int total = 0;
        foreach(NodeSaver node in nodes)
        {
            Object[] save = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(node));
            
            total += save.Length;
            foreach (Object o in save)
            {
                
                if (o.GetType().Equals(typeof(ConnectionPointSave)))
                {
                    connection++;
                    //(o as ConnectionPointSave).style.normal = null;
                    //(o as ConnectionPointSave).style.active = null;
                    (o as ConnectionPointSave).style = new GUIStyle();
                    (o as ConnectionPointSave).style.name = "Test";


                }
                if (o.GetType().Equals(typeof(BaseDialogNode)))
                {
                    baseDialog++;
                    (o as BaseDialogNode).style = new GUIStyle();
                    (o as BaseDialogNode).defaultNodeStyle = new GUIStyle();
                    (o as BaseDialogNode).selectedNodeStyle = new GUIStyle();
                    (o as BaseDialogNode).inPointStyle = new GUIStyle();
                    (o as BaseDialogNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(StartDialogNode)))
                {
                    startDialog++;
                    (o as StartDialogNode).style = new GUIStyle();
                    (o as StartDialogNode).defaultNodeStyle = new GUIStyle();
                    (o as StartDialogNode).selectedNodeStyle = new GUIStyle();
                    (o as StartDialogNode).inPointStyle = new GUIStyle();
                    (o as StartDialogNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(PrerequisiteNode)))
                {
                    pre++;
                    (o as PrerequisiteNode).style = new GUIStyle();
                    (o as PrerequisiteNode).defaultNodeStyle = new GUIStyle();
                    (o as PrerequisiteNode).selectedNodeStyle = new GUIStyle();
                    (o as PrerequisiteNode).inPointStyle = new GUIStyle();
                    (o as PrerequisiteNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(NPCActionNode)))
                {
                    npc++;
                    (o as NPCActionNode).style = new GUIStyle();
                    (o as NPCActionNode).defaultNodeStyle = new GUIStyle();
                    (o as NPCActionNode).selectedNodeStyle = new GUIStyle();
                    (o as NPCActionNode).inPointStyle = new GUIStyle();
                    (o as NPCActionNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(ExitNode)))
                {
                    exit++;
                    (o as ExitNode).style = new GUIStyle();
                    (o as ExitNode).defaultNodeStyle = new GUIStyle();
                    (o as ExitNode).selectedNodeStyle = new GUIStyle();
                    (o as ExitNode).inPointStyle = new GUIStyle();
                    (o as ExitNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(DialogNode)))
                {
                    dial++;
                    (o as DialogNode).style = new GUIStyle();
                    (o as DialogNode).defaultNodeStyle = new GUIStyle();
                    (o as DialogNode).selectedNodeStyle = new GUIStyle();
                    (o as DialogNode).inPointStyle = new GUIStyle();
                    (o as DialogNode).outPointStyle = new GUIStyle();
                    
                }
                AssetDatabase.SaveAssets();
            }
        }
        foreach (StartBehaviourNode start in behaviour)
        {
            Object[] save = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(start));
            foreach(Object o in save)
            {
                if (o.GetType().Equals(typeof(BaseBehaviourNode)))
                {
                    beh++;
                    (o as BaseBehaviourNode).style = new GUIStyle();
                    (o as BaseBehaviourNode).defaultNodeStyle = new GUIStyle();
                    (o as BaseBehaviourNode).selectedNodeStyle = new GUIStyle();
                    (o as BaseBehaviourNode).inPointStyle = new GUIStyle();
                    (o as BaseBehaviourNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(Selector)))
                {
                    sel++;
                    (o as Selector).style = new GUIStyle();
                    (o as Selector).defaultNodeStyle = new GUIStyle();
                    (o as Selector).selectedNodeStyle = new GUIStyle();
                    (o as Selector).inPointStyle = new GUIStyle();
                    (o as Selector).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(Sequence)))
                {
                    seq++;
                    (o as Sequence).style = new GUIStyle();
                    (o as Sequence).defaultNodeStyle = new GUIStyle();
                    (o as Sequence).selectedNodeStyle = new GUIStyle();
                    (o as Sequence).inPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(ConditionalSequence)))
                {
                    con++;
                    (o as ConditionalSequence).style = new GUIStyle();
                    (o as ConditionalSequence).defaultNodeStyle = new GUIStyle();
                    (o as ConditionalSequence).selectedNodeStyle = new GUIStyle();
                    (o as ConditionalSequence).inPointStyle = new GUIStyle();
                    (o as ConditionalSequence).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(StartBehaviourNode)))
                {
                    star++;
                    (o as StartBehaviourNode).style = new GUIStyle();
                    (o as StartBehaviourNode).defaultNodeStyle = new GUIStyle();
                    (o as StartBehaviourNode).selectedNodeStyle = new GUIStyle();
                    (o as StartBehaviourNode).inPointStyle = new GUIStyle();
                    (o as StartBehaviourNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(Iterator)))
                {
                    it++;
                    (o as Iterator).style = new GUIStyle();
                    (o as Iterator).defaultNodeStyle = new GUIStyle();
                    (o as Iterator).selectedNodeStyle = new GUIStyle();
                    (o as Iterator).inPointStyle = new GUIStyle();
                    (o as Iterator).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(Inverter)))
                {
                    inv++;
                    (o as Inverter).style = new GUIStyle();
                    (o as Inverter).defaultNodeStyle = new GUIStyle();
                    (o as Inverter).selectedNodeStyle = new GUIStyle();
                    (o as Inverter).inPointStyle = new GUIStyle();
                    (o as Inverter).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(EnemyNear)))
                {
                    ene++;
                    (o as EnemyNear).style = new GUIStyle();
                    (o as EnemyNear).defaultNodeStyle = new GUIStyle();
                    (o as EnemyNear).selectedNodeStyle = new GUIStyle();
                    (o as EnemyNear).inPointStyle = new GUIStyle();
                    (o as EnemyNear).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(Attack)))
                {
                    att++;
                    (o as Attack).style = new GUIStyle();
                    (o as Attack).defaultNodeStyle = new GUIStyle();
                    (o as Attack).selectedNodeStyle = new GUIStyle();
                    (o as Attack).inPointStyle = new GUIStyle();
                    (o as Attack).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(GoToBehaviourNode)))
                {
                    goTo++;
                    (o as GoToBehaviourNode).style = new GUIStyle();
                    (o as GoToBehaviourNode).defaultNodeStyle = new GUIStyle();
                    (o as GoToBehaviourNode).selectedNodeStyle = new GUIStyle();
                    (o as GoToBehaviourNode).inPointStyle = new GUIStyle();
                    (o as GoToBehaviourNode).outPointStyle = new GUIStyle();
                    
                }
                AssetDatabase.SaveAssets();
            }
        }
        Debug.Log("Done");
        Debug.Log("ConnectionNode: " + connection);
        Debug.Log("BaseDialogNodes: " + baseDialog);
        Debug.Log("StartDialogNode: " + startDialog);
        Debug.Log("Prerequisite: " + pre);
        Debug.Log("NPCAction: " + npc);
        Debug.Log("ExitNode: " + exit);
        Debug.Log("DialogNode: " + dial);
        Debug.Log(connection + baseDialog + startDialog + pre + npc + exit + dial + " of " + total.ToString());
        Debug.Log("BaseBehaviourNode: " + beh);
        Debug.Log("ConditionalNode: " + con);
        Debug.Log("StartNode: " + star);
        Debug.Log("Selector: " + sel);
        Debug.Log("Sequence: " + seq);
        Debug.Log("Iterator: " + it);
        Debug.Log("Inverter: " + inv);
        Debug.Log("EnemyNearby: " + ene);
        Debug.Log("AttackNode: " + att);
        Debug.Log("GoToNode: " + goTo);
        AssetDatabase.SaveAssets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void FixIt()
    {
        string[] names = new string[] {"action", "AIDialog","BrokenTerraformerDialog","BuildladderDialog", "DrNevtonDialog", "DrShroomDialog", "EntranceScientistDialog", "FalseEndDialog","FarmerDialog","ForceResetDialog", "HelpfulScientistDialog", "LabCoatPickUpDialog", "LaserDisableDialog", "PickUpDrRNoteDialog", "PickUpTimeGauntletDialog" , "ScientistDialog", "ScientistZombieDialog",  "ShroomDialog", "SusieDialog",  "TerraformerDialog", "TimeScientistDialog", "VeggieDialog", "WinGameDialog", "ZombieDialog", "ZombiePartDialogPickup"};
        foreach (string name in names)
        {


            Object[] nodes = AssetDatabase.LoadAllAssetsAtPath("Assets/Dialogs/DialogTrees/" + name + ".asset");
            if(nodes.Length == 0)
            {
                Debug.Log(name + "does not exist");
            }
            int baseDialog = 0, startDialog = 0, pre = 0, npc = 0, exit = 0, dial = 0, beh = 0, connection = 0, sel = 0, seq = 0, con = 0, goTo = 0, star = 0, it = 0, inv = 0, ene = 0, att = 0;
            int total = 0;
            foreach (Object o in nodes)
            {
                //Object[] save = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(node));
                //Debug.Log(save.Length);
                //total += save.Length;
                //foreach (Object o in save)
                //{

                if (o.GetType().Equals(typeof(ConnectionPointSave)))
                {
                    connection++;
                    //(o as ConnectionPointSave).style.normal = null;
                    //(o as ConnectionPointSave).style.active = null;
                    (o as ConnectionPointSave).style = new GUIStyle();
                    (o as ConnectionPointSave).style.name = "Test";


                }
                if (o.GetType().Equals(typeof(BaseDialogNode)))
                {
                    baseDialog++;
                    (o as BaseDialogNode).style = new GUIStyle();
                    (o as BaseDialogNode).defaultNodeStyle = new GUIStyle();
                    (o as BaseDialogNode).selectedNodeStyle = new GUIStyle();
                    (o as BaseDialogNode).inPointStyle = new GUIStyle();
                    (o as BaseDialogNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(StartDialogNode)))
                {
                    startDialog++;
                    (o as StartDialogNode).style = new GUIStyle();
                    (o as StartDialogNode).defaultNodeStyle = new GUIStyle();
                    (o as StartDialogNode).selectedNodeStyle = new GUIStyle();
                    (o as StartDialogNode).inPointStyle = new GUIStyle();
                    (o as StartDialogNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(PrerequisiteNode)))
                {
                    pre++;
                    (o as PrerequisiteNode).style = new GUIStyle();
                    (o as PrerequisiteNode).defaultNodeStyle = new GUIStyle();
                    (o as PrerequisiteNode).selectedNodeStyle = new GUIStyle();
                    (o as PrerequisiteNode).inPointStyle = new GUIStyle();
                    (o as PrerequisiteNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(NPCActionNode)))
                {
                    npc++;
                    (o as NPCActionNode).style = new GUIStyle();
                    (o as NPCActionNode).defaultNodeStyle = new GUIStyle();
                    (o as NPCActionNode).selectedNodeStyle = new GUIStyle();
                    (o as NPCActionNode).inPointStyle = new GUIStyle();
                    (o as NPCActionNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(ExitNode)))
                {
                    exit++;
                    (o as ExitNode).style = new GUIStyle();
                    (o as ExitNode).defaultNodeStyle = new GUIStyle();
                    (o as ExitNode).selectedNodeStyle = new GUIStyle();
                    (o as ExitNode).inPointStyle = new GUIStyle();
                    (o as ExitNode).outPointStyle = new GUIStyle();
                }
                if (o.GetType().Equals(typeof(DialogNode)))
                {
                    dial++;
                    (o as DialogNode).style = new GUIStyle();
                    (o as DialogNode).defaultNodeStyle = new GUIStyle();
                    (o as DialogNode).selectedNodeStyle = new GUIStyle();
                    (o as DialogNode).inPointStyle = new GUIStyle();
                    (o as DialogNode).outPointStyle = new GUIStyle();

                }
                AssetDatabase.SaveAssets();
            }
        }
        //}
        //Object behaviour
        //foreach (StartBehaviourNode start in behaviour)
        //{
        //    Object[] save = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(start));
        //    foreach (Object o in save)
        //    {
        //        if (o.GetType().Equals(typeof(BaseBehaviourNode)))
        //        {
        //            beh++;
        //            (o as BaseBehaviourNode).style = new GUIStyle();
        //            (o as BaseBehaviourNode).defaultNodeStyle = new GUIStyle();
        //            (o as BaseBehaviourNode).selectedNodeStyle = new GUIStyle();
        //            (o as BaseBehaviourNode).inPointStyle = new GUIStyle();
        //            (o as BaseBehaviourNode).outPointStyle = new GUIStyle();
        //        }
        //        if (o.GetType().Equals(typeof(Selector)))
        //        {
        //            sel++;
        //            (o as Selector).style = new GUIStyle();
        //            (o as Selector).defaultNodeStyle = new GUIStyle();
        //            (o as Selector).selectedNodeStyle = new GUIStyle();
        //            (o as Selector).inPointStyle = new GUIStyle();
        //            (o as Selector).outPointStyle = new GUIStyle();
        //        }
        //        if (o.GetType().Equals(typeof(Sequence)))
        //        {
        //            seq++;
        //            (o as Sequence).style = new GUIStyle();
        //            (o as Sequence).defaultNodeStyle = new GUIStyle();
        //            (o as Sequence).selectedNodeStyle = new GUIStyle();
        //            (o as Sequence).inPointStyle = new GUIStyle();
        //        }
        //        if (o.GetType().Equals(typeof(ConditionalSequence)))
        //        {
        //            con++;
        //            (o as ConditionalSequence).style = new GUIStyle();
        //            (o as ConditionalSequence).defaultNodeStyle = new GUIStyle();
        //            (o as ConditionalSequence).selectedNodeStyle = new GUIStyle();
        //            (o as ConditionalSequence).inPointStyle = new GUIStyle();
        //            (o as ConditionalSequence).outPointStyle = new GUIStyle();
        //        }
        //        if (o.GetType().Equals(typeof(StartBehaviourNode)))
        //        {
        //            star++;
        //            (o as StartBehaviourNode).style = new GUIStyle();
        //            (o as StartBehaviourNode).defaultNodeStyle = new GUIStyle();
        //            (o as StartBehaviourNode).selectedNodeStyle = new GUIStyle();
        //            (o as StartBehaviourNode).inPointStyle = new GUIStyle();
        //            (o as StartBehaviourNode).outPointStyle = new GUIStyle();
        //        }
        //        if (o.GetType().Equals(typeof(Iterator)))
        //        {
        //            it++;
        //            (o as Iterator).style = new GUIStyle();
        //            (o as Iterator).defaultNodeStyle = new GUIStyle();
        //            (o as Iterator).selectedNodeStyle = new GUIStyle();
        //            (o as Iterator).inPointStyle = new GUIStyle();
        //            (o as Iterator).outPointStyle = new GUIStyle();
        //        }
        //        if (o.GetType().Equals(typeof(Inverter)))
        //        {
        //            inv++;
        //            (o as Inverter).style = new GUIStyle();
        //            (o as Inverter).defaultNodeStyle = new GUIStyle();
        //            (o as Inverter).selectedNodeStyle = new GUIStyle();
        //            (o as Inverter).inPointStyle = new GUIStyle();
        //            (o as Inverter).outPointStyle = new GUIStyle();
        //        }
        //        if (o.GetType().Equals(typeof(EnemyNear)))
        //        {
        //            ene++;
        //            (o as EnemyNear).style = new GUIStyle();
        //            (o as EnemyNear).defaultNodeStyle = new GUIStyle();
        //            (o as EnemyNear).selectedNodeStyle = new GUIStyle();
        //            (o as EnemyNear).inPointStyle = new GUIStyle();
        //            (o as EnemyNear).outPointStyle = new GUIStyle();
        //        }
        //        if (o.GetType().Equals(typeof(Attack)))
        //        {
        //            att++;
        //            (o as Attack).style = new GUIStyle();
        //            (o as Attack).defaultNodeStyle = new GUIStyle();
        //            (o as Attack).selectedNodeStyle = new GUIStyle();
        //            (o as Attack).inPointStyle = new GUIStyle();
        //            (o as Attack).outPointStyle = new GUIStyle();
        //        }
        //        if (o.GetType().Equals(typeof(GoToBehaviourNode)))
        //        {
        //            goTo++;
        //            (o as GoToBehaviourNode).style = new GUIStyle();
        //            (o as GoToBehaviourNode).defaultNodeStyle = new GUIStyle();
        //            (o as GoToBehaviourNode).selectedNodeStyle = new GUIStyle();
        //            (o as GoToBehaviourNode).inPointStyle = new GUIStyle();
        //            (o as GoToBehaviourNode).outPointStyle = new GUIStyle();

        //        }
        //        AssetDatabase.SaveAssets();
        //    }
        //}
        //Debug.Log("Done");
        //Debug.Log("ConnectionNode: " + connection);
        //Debug.Log("BaseDialogNodes: " + baseDialog);
        //Debug.Log("StartDialogNode: " + startDialog);
        //Debug.Log("Prerequisite: " + pre);
        //Debug.Log("NPCAction: " + npc);
        //Debug.Log("ExitNode: " + exit);
        //Debug.Log("DialogNode: " + dial);
        //Debug.Log(connection + baseDialog + startDialog + pre + npc + exit + dial + " of " + total.ToString());
        //Debug.Log("BaseBehaviourNode: " + beh);
        //Debug.Log("ConditionalNode: " + con);
        //Debug.Log("StartNode: " + star);
        //Debug.Log("Selector: " + sel);
        //Debug.Log("Sequence: " + seq);
        //Debug.Log("Iterator: " + it);
        //Debug.Log("Inverter: " + inv);
        //Debug.Log("EnemyNearby: " + ene);
        //Debug.Log("AttackNode: " + att);
        //Debug.Log("GoToNode: " + goTo);
        AssetDatabase.SaveAssets();
    }
}
