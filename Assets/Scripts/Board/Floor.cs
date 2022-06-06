using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    [HideInInspector]
    public TilemapRenderer tilemapRenderer;
    public int order { get { return tilemapRenderer.sortingOrder; } }
    public int contentOrder;
    public Vector3Int minXY;
    public Vector3Int maxXY;
    [HideInInspector]
    public Tilemap tilemap;

    void Awake()
    {
        tilemapRenderer = this.transform.GetComponent<TilemapRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    public List<Vector3Int> LoadTiles()
    {
        List<Vector3Int> tiles = new List<Vector3Int>();
        for (int i = minXY.x; i <= maxXY.x; i++)
        {
            for (int j = minXY.y; j <= maxXY.y; j++)
            {
                Vector3Int currentPos = new Vector3Int(i, j, 0);
                if (tilemap.HasTile(currentPos))
                {
                    tiles.Add(currentPos);
                }
            }
        }
        return tiles;
    }

}