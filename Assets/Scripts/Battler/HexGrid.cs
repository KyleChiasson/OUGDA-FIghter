using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public class Tile
    {
        public Vector2Int Position;
    }

    [SerializeField]
    private GameObject HexBlock;

    public List<Tile> Grid;
    
}
