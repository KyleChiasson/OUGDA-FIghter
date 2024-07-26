using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public class Tile
    {
        public Vector2Int Position;
        public bool Terrain;

        public Tile(int x, int y, bool T)
        {
            Position = new Vector2Int(x, y);
            Terrain = T;

        }
    }

    [SerializeField]
    private GameObject HexBlock;
    [SerializeField]
    private GameObject TerrainAsset;
    [SerializeField]
    private float tileSize;
    [SerializeField]
    private Vector2Int GridSize;
    [SerializeField]
    private float TerrainChance;

    public List<Tile> Grid;

    public void CreateGrid()
    {
        Random.InitState(System.DateTime.Now.Millisecond + System.DateTime.Now.Second + System.DateTime.Now.Minute);

        Grid = new List<Tile>();
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                float R = Random.Range(0f, 1f);
                
                Grid.Add(new Tile(x, y, R <= TerrainChance));

                GameObject OBJ = Instantiate(HexBlock, new Vector3(x * (tileSize * Mathf.Sqrt(3)) + (y % 2 * (tileSize / 2 * Mathf.Sqrt(3))), 0, y * ((tileSize / 2) + tileSize)), Quaternion.Euler(-90, 0, 0), transform);
                if(R <= TerrainChance)
                {
                    OBJ.GetComponent<MeshRenderer>().material.color = Color.gray;

                    Instantiate(TerrainAsset, new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 0), OBJ.transform);
                }
                
            }
        }
        
    }

    private void Start()
    {
        CreateGrid();
    }

}
