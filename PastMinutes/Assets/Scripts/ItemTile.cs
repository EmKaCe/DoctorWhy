using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/ItemTile")]
public class ItemTile : TileBase
{

    public Sprite sprite;

    public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go)
    {
        SpriteRenderer r = go.AddComponent<SpriteRenderer>();
        r.sprite = sprite;
        r.sortingOrder = (int)go.transform.position.y * -10;
        return true;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/ItemTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Item Tile", "New Item Tile", "Asset", "Save Item Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ItemTile>(), path);
    }
#endif
}
