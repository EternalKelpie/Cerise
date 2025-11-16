using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{

    public Tilemap walkableMap;

    public Vector2Int gridSize;    
    public Vector2Int gridOrigin;  

    private bool[,] walkableTiles;

    void Start()
    {
        if (walkableMap == null)
        {
            return;
        }

        BoundsInt bounds = walkableMap.cellBounds;

        gridOrigin = new Vector2Int(bounds.xMin, bounds.yMin);
        gridSize = new Vector2Int(bounds.size.x, bounds.size.y);

        walkableTiles = new bool[gridSize.x, gridSize.y];


        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(bounds.xMin + x, bounds.yMin + y, 0);
                TileBase tile = walkableMap.GetTile(tilePos);
                walkableTiles[x, y] = (tile != null);
            }
        }
    }

    public bool IsWalkable(Vector2Int gridPos)
    {
 
        Vector2Int local = gridPos - gridOrigin;
        if (local.x < 0 || local.y < 0 || local.x >= gridSize.x || local.y >= gridSize.y)
            return false;

        return walkableTiles[local.x, local.y];
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector3Int cell = walkableMap.WorldToCell(worldPos);
        return new Vector2Int(cell.x, cell.y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        Vector3 world = walkableMap.CellToWorld(new Vector3Int(gridPos.x, gridPos.y, 0));
        return world + new Vector3(0.5f, 0.5f, 0f);
    }
}
