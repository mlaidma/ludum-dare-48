using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "CustomColumn", menuName = "CustomColumn", order = 0)]
public class CustomColumn : ScriptableObject {
    
    [SerializeField] public List<TileBase> columnTiles;
}
