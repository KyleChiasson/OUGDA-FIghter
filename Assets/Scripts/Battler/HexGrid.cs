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

        public Vector3 WorldPosition;
        /// <summary> Is the tile a piece of terrain </summary>
        public bool Terrain = false;
        /// <summary> The in-game representation of the tile </summary>
        public GameObject Instance;
        /// <summary> All tiles that are adjcent to this tile </summary>
        public List<Tile> Adjacent = new List<Tile>();

        /// <summary> The function that we use to create a new tile </summary>
        public Tile(int x, int y, GameObject I, Transform parent)
        {
            Position = new Vector2Int(x, y);
            WorldPosition = new Vector3(x * Mathf.Sqrt(3) + (y % 2 * (.5f * Mathf.Sqrt(3))), 0, y * 1.5f);
            Instance = Instantiate(I, WorldPosition, Quaternion.Euler(-90, 0, 0), parent);
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
            Instantiate(prefab, WorldPosition + Vector3.up, Quaternion.Euler(-90, 0, 0), Instance.transform);
        }
    }

    /// "[SerializeField]" puts it in the Unity inspector
    
    /// <summary> A prefab of the grid space </summary>
    [SerializeField]
    private GameObject HexBlock;
    /// <summary> A prefab of the rock for terain </summary>
    [SerializeField]
    private GameObject TerrainAsset;
    /// <summary> The size of the grid </summary>
    [SerializeField]
    private Vector2Int GridSize;
    /// <summary> The chance a tile has of being terrain </summary>
    [SerializeField]
    private float TerrainChance;
    /// <summary> Chance of a tile being terrain when adjacent to terrain </summary>
    [SerializeField]
    private float AdjTerrainChance;
    /// <summary> A prefab of the player </summary>
    [SerializeField]
    private GameObject Player;
    /// <summary> Physically place the player in the correct spot </summary>
    private GameObject Plr;
    /// <summary> The tile where the player spawns </summary>
    private Tile PlayerPosition;
    /// <summary> Knows if x or y is smaller in GridSize </summary>
    private bool ShortSidesX = true;


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
                Tile t = new Tile(x, y, HexBlock, transform);

                ///Add the tile to grid
                Grid.Add(t);
            }
        }

        ///Set the adjcency of all tiles in grid
        Grid.ForEach(i => i.SetAdjacent(Grid));

        ///set shorter side
        if (GridSize.y < GridSize.x)
        {
            ShortSidesX = false;
        }

        /// Write out the logic, then come back to this
        //if (ShortSidesX == true)

        ///Set terrain
        Grid.ForEach(i =>
        {
            

            if (i.Adjacent.Where(A => A.Terrain).Count() == 0)
            {
                ///Randomly add terrain
                if (Random.Range(0f, 1f) <= TerrainChance)
                {
                    i.SetTerrain(TerrainAsset);
                }
            }
            else
            {
                ///Randomly add adjacent terrain
                if (Random.Range(0f, 1f) <= AdjTerrainChance)
                {
                    i.SetTerrain(TerrainAsset);
                }
            }
        });
    }

    private void Start()
    {
        ///create grid when starting the scene
        CreateGrid();

        ///set player spawn tile
        PlayerPosition = Grid.Where(i => !i.Terrain).First();

        Plr = Instantiate(Player, PlayerPosition.WorldPosition + Vector3.up, Quaternion.identity);
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

                    if (!T.Terrain && T.Adjacent.Contains(PlayerPosition))
                    {
                        PlayerPosition = T;
                        Plr.transform.position = PlayerPosition.WorldPosition + Vector3.up;
                    }
                    else
                    {
                        Debug.Log("You can't park there, sir");
                    }
                    
                }
            }
        }


    }

}
