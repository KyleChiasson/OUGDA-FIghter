using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public class Tile
    {
        public Vector2Int Position;

        public Tile(int x, int y)
        {
            Position = new Vector2Int(x, y);
        }
    }

    [SerializeField]
    private GameObject HexBlock;
    [SerializeField]
    private float tileSize;

    public List<Tile> Grid;

    public void CreateGrid()
    {
        Grid = new List<Tile>();
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                Grid.Add(new Tile(x, y));

                Instantiate(HexBlock, new Vector3(x * (tileSize * Mathf.Sqrt(3)) + (y % 2 * (tileSize / 2 * Mathf.Sqrt(3))), 0, y * ((tileSize / 2) + tileSize)), Quaternion.Euler(-90, 0, 0), transform);
            }
        }
        
    }

    private void Start()
    {
        CreateGrid();
    }

}
