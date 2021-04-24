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
    [SerializeField] int floorDropChance = 15;
    [SerializeField] int lookAhead = 3;
    [SerializeField] int lookBack = 5;
    [SerializeField] TileBase tile;

    [SerializeField] Camera cam;

    private int worldHeight = 4;
    private int spawnWidth = 6;

    LastColumn lastColumn;

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
        Debug.Log("lastColumn.ahead: " + (lastColumn.ahead.x, lastColumn.ahead.y).ToString());
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

            DrawColumn(ahead);
            DrawColumn(behind);

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

            if(DropFloor())
            {
                y -= 1;   
            }

            x += 1;

            Vector2Int newColumn = new Vector2Int(x, y);

            DrawColumn(newColumn);
            lastColumn.ahead = newColumn;
        }
    }

    private void DrawColumn(Vector2Int coord)
    {
        for(int i = 0; i < worldHeight; i++)
        {
            Vector2Int newTile = new Vector2Int(coord.x, coord.y + i);
            tilemap.SetTile(new Vector3Int(newTile.x, newTile.y, 0), tile);
        }
    }

    private bool DropFloor()
    {
        int random = rng.Next(0, 100);

        Debug.Log("Floordrop:" + floorDropChance);  
        return random < floorDropChance;
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
