using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

struct LastColumn
{
    public Vector2Int ahead;
    public Vector2Int behind;
}

public class Level : MonoBehaviour
{
    [SerializeField] private int floorDropChance = 35;
    [SerializeField] private int woodColumnChance = 25;

    [SerializeField] private int lookAhead = 3;

    [SerializeField] private TileBase tile;
    [SerializeField] private CustomColumn woodColumn;

    [SerializeField] private Camera cam;

    private int worldHeight = 4;
    private int spawnWidth = 6;

    private LastColumn lastColumn;

    private System.Random rng;
    
    private Tilemap tilemap;


    // Start is called before the first frame update
    void Start()
    {
        Init();
        GenerateSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAhead();
        //Debug.Log("lastColumn.ahead: " + (lastColumn.ahead.x, lastColumn.ahead.y).ToString());
    }

    private void Init()
    {
        rng = new System.Random();
        tilemap = GetComponent<Tilemap>();
        tilemap.ClearAllTiles();

        lastColumn.ahead = new Vector2Int(0, 0);
        lastColumn.behind = new Vector2Int(0, 0);
    }

    private void GenerateSpawn()
    {
        for(int x = 0; x < spawnWidth; x++)
        {
            Vector2Int ahead = new Vector2Int(x, 0);
            Vector2Int behind = new Vector2Int(-x - 1, 0);

            DrawColumn(ahead.x, ahead.y);
            DrawColumn(behind.x, behind.y);

            lastColumn.ahead = ahead;
            lastColumn.behind = behind;
        }
    }

    private void MoveAhead()
    {
        int viewportAhead = GetViewportAhead();

        // Generate Ahead
        if (lastColumn.ahead.x - viewportAhead < lookAhead)
        {
            int x = lastColumn.ahead.x;
            int y = lastColumn.ahead.y;
            x += 1;

            if (DropFloor())
            {
                y -= 1;
                DrawColumn(x, y);
            }
            else if(GenerateWoodColumn())
            {
                DrawWoodColumn(x, y);
            }
            else
            {
                DrawColumn(x, y);
            }

            lastColumn.ahead = new Vector2Int(x, y);
        }
    }

    private void DrawColumn(int x, int y)
    {
        for(int i = 0; i < worldHeight; i++)
        {
            Vector3Int newTileCoord = new Vector3Int(x, y + i, 0);
            tilemap.SetTile(newTileCoord, tile);
        }
    }

    private void DrawWoodColumn(int x, int y)
    {

        Debug.Log("Draw Wood Column: " + woodColumn.columnTiles.Count);

        for(int i = 0; i < woodColumn.columnTiles.Count; i++)
        {
            Vector3Int newTileCoord = new Vector3Int(x, y + i, 0);
            tilemap.SetTile(newTileCoord, woodColumn.columnTiles[i]);
        }
    }

    private bool DropFloor()
    {
        int random = rng.Next(0, 100);
        return random < floorDropChance;
    }

    private bool GenerateWoodColumn()
    {
        int random = rng.Next(0, 100);
        return random < woodColumnChance;
    }

    private int GetViewportAhead()
    {
        return Mathf.FloorToInt(cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane)).x);
    }

    private int GetViewportBehind()
    {
        return Mathf.FloorToInt(cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x);
    }
}
