using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexGrid : MonoBehaviour
{
    /// <summary> A class used to represent a hexagon tile of a grid </summary>
    public class Tile
    {
        /// <summary> The position of the tile </summary>
        public Vector2Int Position;
        /// <summary> Is the tile a piece of terrain </summary>
        public bool Terrain = false;
        /// <summary> The in-game representation of the tile </summary>
        public GameObject Instance;
        /// <summary> All tiles that are adjcent to this tile </summary>
        public List<Tile> Adjacent = new List<Tile>();

        /// <summary> The function that we use to create a new tile </summary>
        public Tile(int x, int y, GameObject I)
        {
            Position = new Vector2Int(x, y);
            Instance = I;
        }

        /// <summary> Finds all adjcent tiles in the list </summary>
        public void SetAdjacent(List<Tile> Grid)
        {
            ///clears the adjcent tiles
            Adjacent.Clear();

            ///Get all adjcent tiles
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

        /// <summary> Sets this tile to a terrain tile </summary>
        public void SetTerrain(GameObject prefab)
        {
            Terrain = true;
            
            ///Set the color
            Instance.GetComponent<MeshRenderer>().material.color = Color.gray;

            ///Add the rock
            Instantiate(prefab, new Vector3(Position.x * Mathf.Sqrt(3) + (Position.y % 2 * (0.5f * Mathf.Sqrt(3))), 1, Position.y * 1.5f), Quaternion.Euler(-90, 0, 0), Instance.transform);
        }
    }

    /// <summary> A prefab of the grid space </summary>
    [SerializeField]
    private GameObject HexBlock;
    /// <summary> A prefab of the rock for terain </summary>
    [SerializeField]
    private GameObject TerrainAsset;
    /// <summary> The size of the grid </summary>
    [SerializeField]
    private Vector2Int GridSize;
    /// <summary> The chance a tile has of being terain </summary>
    [SerializeField]
    private float TerrainChance;

    /// <summary> The list of all tiles in the grid </summary>
    public List<Tile> Grid = new List<Tile>();

    /// <summary> Finds a tile with specific coordinates in a list of tiles </summary>
    private static bool FindTile(int x, int y, List<Tile> Grid, out Tile tile)
    {
        ///Does the searching
        if (Grid.Where(i => i.Position.x == x && i.Position.y == y).Count()>0)
        {
            ///if found return true and the tile
            tile = Grid.Where(i => i.Position.x == x && i.Position.y == y).First();
            return true;
        }
        tile = null;
        return false;
    }

    /// <summary> Generates a new grid </summary>
    public void CreateGrid()
    {
        ///initialize the random number generator
        Random.InitState(System.DateTime.Now.Millisecond + System.DateTime.Now.Second + System.DateTime.Now.Minute);

        ///Clear the grid of all tiles
        Grid.Clear();

        ///For each x and y in the grid size
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {      
                ///Make a new tile in the correct position
                Tile t = new Tile(x, y, Instantiate(HexBlock, new Vector3(x * Mathf.Sqrt(3) + (y % 2 * (.5f * Mathf.Sqrt(3))), 0, y * 1.5f), Quaternion.Euler(-90, 0, 0), transform));

                ///Add the tile to grid
                Grid.Add(t);
                
                ///Randomly Add terrain
                if(Random.Range(0f, 1f) <= TerrainChance)
                {
                    t.SetTerrain(TerrainAsset);
                }
            }
        }

        ///Set the adjcency of all tiles in grid
        Grid.ForEach(i => i.SetAdjacent(Grid));
    }

    private void Start()
    {
        ///create grid when starting the scene
        CreateGrid();
    }

    /// <summary> Every frame </summary>
    private void Update()
    {
        ///Check if the left mouse button was pressed
        if (Input.GetMouseButtonDown(0))
        {
            ///If we can draw a line from the mouse position to an object
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                ///If that object was part of our grid
                if(Grid.Where(i => i.Instance == hitInfo.collider.gameObject).Count() > 0)
                {
                    ///Get that object
                    Tile T = Grid.Where(i => i.Instance == hitInfo.collider.gameObject).First();
                    ///Test line that colors all spaces around the clicked tile
                    //T.Adjacent.ForEach(i => i.Instance.GetComponent<MeshRenderer>().material.color = Color.blue);
                }
            }
        }
    }

}
