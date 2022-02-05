using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
public class TilemapScript : MonoBehaviour 
{
    public List<Vector3Int> tilesToErase;
    public Tilemap myTilemap;

    public void eraseTiles()
    {
        for (int i = 0; i < tilesToErase.Count; i++)
        {
            myTilemap.SetTile(tilesToErase[i], null);
        }
    }

}
