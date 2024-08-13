using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PiecesSpawnerController : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap piecesSpawnersTilemap;
    [SerializeField] private Bound[] boundsElement;
    [SerializeField] private FieldController gridController;
    [SerializeField] private PiecesSpawner spawnerPrefab;

    private List<PiecesSpawner> piecesSpawners;
    private BoundsInt bounds;
    private int xDim;
    private int yDim;

    private void Start()
    {
        piecesSpawners = new List<PiecesSpawner>();

        SetBounds();
        CreateSpawners();
    }

    private void Update()
    {

    }

    private void SetBounds()
    {
        Vector3Int boundUp = piecesSpawnersTilemap.WorldToCell(boundsElement[0].transform.position);
        Vector3Int boundBottom = piecesSpawnersTilemap.WorldToCell(boundsElement[1].transform.position);

        bounds.xMin = boundUp.x;
        bounds.xMax = boundBottom.x;
        bounds.yMin = boundBottom.y;
        bounds.yMax = boundUp.y;
        xDim = bounds.xMax - bounds.xMin + 1;
        yDim = bounds.yMax - bounds.yMin + 1;
    }

    private void CreateSpawners()
    {
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                if (piecesSpawnersTilemap.GetTile(new Vector3Int(GetXInGridPos(x), GetYInGridPos(y), 0)) != null)
                {
                    Vector3 pos = piecesSpawnersTilemap.CellToWorld(new Vector3Int(GetXInGridPos(x), GetYInGridPos(y), 0));
                    pos.x += grid.cellSize.x / 2;
                    pos.y += grid.cellSize.y / 2;
                    PiecesSpawner spawner = Instantiate(spawnerPrefab, pos, Quaternion.identity);
                    spawner.transform.parent = transform;
                    spawner.name = $"PiecesSpawners [{x}, {y}]";
                    spawner.Init(x, y);
                    piecesSpawners.Add(spawner);
                }
            }
        }
    }

    private int GetXInGridPos(int x)
    {
        return (x + bounds.xMin);
    }

    private int GetYInGridPos(int y)
    {
        return (bounds.yMax - y);
    }

    private void CheckNeedOfSpawnPiece()
    {
        foreach (var spawner in piecesSpawners)
        {
            if (gridController.CheckTypeOfPieceInGrid(spawner.X, spawner.Y, PieceType.Empty))
            {
                gridController.DeletePiece(spawner.X, spawner.Y);
                gridController.SpawnNewPiece(spawner.X, spawner.Y, PieceType.Normal);
            }
        }
    }
}
