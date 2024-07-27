using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexGrid : MonoBehaviour
{
    public class Tile
    {
        public Vector2Int Position;
        public bool Terrain;
        public GameObject Instance;

        public Tile(int x, int y, bool T, GameObject I)
        {
            Position = new Vector2Int(x, y);
            Terrain = T;
            Instance = I;

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
                
               

                Vector3 LocalHex = new Vector3(x * (tileSize * Mathf.Sqrt(3)) + (y % 2 * (tileSize / 2 * Mathf.Sqrt(3))), 0, y * ((tileSize / 2) + tileSize));

                GameObject OBJ = Instantiate(HexBlock, LocalHex, Quaternion.Euler(-90, 0, 0), transform);
                if(R <= TerrainChance)
                {
                    OBJ.GetComponent<MeshRenderer>().material.color = Color.gray;

                    Instantiate(TerrainAsset, new Vector3(0, 1f, 0) + LocalHex, Quaternion.Euler(-90, 0, 0), OBJ.transform);
                }

                Grid.Add(new Tile(x, y, R <= TerrainChance, OBJ));
            }
        }
        
    }

    private void Start()
    {
        CreateGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left click");

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                Debug.Log("raycast");

                Tile T = Grid.Where(i => i.Instance == hitInfo.collider.gameObject).First();
                
                if (T != null)
                {
                    Debug.Log("not null");
                    
                    T.Instance.GetComponent<MeshRenderer>().material.color = Color.blue;
                }
            }
        }
    }

}
