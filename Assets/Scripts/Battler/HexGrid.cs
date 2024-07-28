using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class HexGrid : MonoBehaviour
{
    public class Tile
    {
        public Vector2Int Position;
        public bool Terrain;
        public GameObject Instance;
        public List<Tile> Adjacent;

        public Tile(int x, int y, GameObject I)
        {
            Position = new Vector2Int(x, y);
            Terrain = false;
            Instance = I;

        }

        public void SetAdjacent(List<Tile> Grid)
        {
            Adjacent = new List<Tile>();

            Tile tile;

            if (FindTile(Position.x + 1, Position.y, Grid, out tile))
            {
                Adjacent.Add(tile);
            }
            if (FindTile(Position.x - 1, Position.y, Grid, out tile))
            {
                Adjacent.Add(tile);
            }
            if (FindTile(Position.x, Position.y + 1, Grid, out tile))
            {
                Adjacent.Add(tile);
            }
            if (FindTile(Position.x, Position.y - 1, Grid, out tile))
            {
                Adjacent.Add(tile);
            }

            if (Position.y % 2 == 0)
            {
                if (FindTile(Position.x - 1, Position.y + 1, Grid, out tile))
                {
                    Adjacent.Add(tile);
                }
                if (FindTile(Position.x - 1, Position.y - 1, Grid, out tile))
                {
                    Adjacent.Add(tile);
                }
            }
            else
            {
                if (FindTile(Position.x + 1, Position.y + 1, Grid, out tile))
                {
                    Adjacent.Add(tile);
                }
                if (FindTile(Position.x + 1, Position.y - 1, Grid, out tile))
                {
                    Adjacent.Add(tile);
                }

            }
        }

        public void AddTerrain(GameObject prefab)
        {
            Terrain = true;
            
            Instance.GetComponent<MeshRenderer>().material.color = Color.gray;

            Instantiate(prefab, new Vector3(Position.x * Mathf.Sqrt(3) + (Position.y % 2 * (0.5f * Mathf.Sqrt(3))), 1, Position.y * 1.5f), Quaternion.Euler(-90, 0, 0), Instance.transform);
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

    private static bool FindTile(int x, int y, List<Tile> Grid, out Tile tile)
    {
        if (Grid.Where(i => i.Position.x == x && i.Position.y == y).Count()>0)
        {
            tile = Grid.Where(i => i.Position.x == x && i.Position.y == y).First();
            return true;
        }
        tile = null;
        return false;
    }
    public void CreateGrid()
    {
        Random.InitState(System.DateTime.Now.Millisecond + System.DateTime.Now.Second + System.DateTime.Now.Minute);

        Grid = new List<Tile>();
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                float R = Random.Range(0f, 1f);
                
               

                Vector3 LocalHex = new Vector3(x * (tileSize * Mathf.Sqrt(3)) + (y % 2 * (tileSize / 2 * Mathf.Sqrt(3))), 0, y * ((tileSize / 2) + tileSize));

                GameObject OBJ = Instantiate(HexBlock, LocalHex, Quaternion.Euler(-90, 0, 0), transform);

                Grid.Add(new Tile(x, y, OBJ));
                
                if(R <= TerrainChance)
                {
                    Grid.Last().AddTerrain(TerrainAsset);
                }

                
            }
        }

        Grid.ForEach(i => i.SetAdjacent(Grid));
    }

    private void Start()
    {
        CreateGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                if(Grid.Where(i => i.Instance == hitInfo.collider.gameObject).Count() > 0)
                {
                    Tile T = Grid.Where(i => i.Instance == hitInfo.collider.gameObject).First();
                    //T.Adjacent.ForEach(i => i.Instance.GetComponent<MeshRenderer>().material.color = Color.blue);
                }
            }
        }
    }

}
