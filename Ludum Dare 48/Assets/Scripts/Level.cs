using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField] private string seed = "evergreen";
    [SerializeField] int worldLenght = 25;

    [SerializeField] private int floorDropChance = 25;

    [SerializeField] TileBase tile;

    private int worldHeight = 4;
    
    private int floorX = 0;
    private int floorY = 0;

    private System.Random rng;

    private Tilemap tilemap;
    

    // Start is called before the first frame update
    void Start()
    {
        Init();
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        rng = new System.Random();
        tilemap = GetComponent<Tilemap>();
    }

    public void GenerateLevel()
    {
        for(int x = floorX; x < worldLenght; x++)
        {
            GetNextColumn();
        }
    }

    private void GetNextColumn()
    {
        int random = rng.Next(0, 100);

        if(random < floorDropChance)
        {
            floorY -= 1;
        }

        for(int i = 0; i < worldHeight; i++)
        {
            tilemap.SetTile(new Vector3Int(floorX, floorY + i, 0), tile);
        }

        floorX += 1;
    }
}
