using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldController : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap field;
    [SerializeField] private PiecePrefab[] piecePrefabs;
    [SerializeField] private Bound[] boundsElement;

    [Serializable, SerializeField]
    private struct PiecePrefab
    {
        public PieceType type;
        public Piece prefab;
    }

    private Dictionary<PieceType, Piece> piecePrefabDict;
    private Piece[,] pieces;
    private BoundsInt bounds;
    private int xDim;
    private int yDim;

    private void Start()
    {
        piecePrefabDict = new Dictionary<PieceType, Piece>();
        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePrefabDict.ContainsKey(piecePrefabs[i].type))
            {
                piecePrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }

        SetBounds();

        pieces = new Piece[xDim, yDim];
        FillEmptyField();
    }

    private void SetBounds()
    {
        Vector3Int boundUp = field.WorldToCell(boundsElement[0].transform.position);
        Vector3Int boundBottom = field.WorldToCell(boundsElement[1].transform.position);

        bounds.xMin = boundUp.x;
        bounds.xMax = boundBottom.x;
        bounds.yMin = boundBottom.y;
        bounds.yMax = boundUp.y;
        xDim = bounds.xMax - bounds.xMin + 1;
        yDim = bounds.yMax - bounds.yMin + 1;
    }

    private void FillEmptyField()
    {
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                if (field.GetTile(new Vector3Int(GetXInGridPos(x), GetYInGridPos(y), 0)) != null)
                {
                    pieces[x, y] = SpawnNewPiece(x, y, PieceType.Empty);
                }
            }
        }
    }

    public Piece SpawnNewPiece(int x, int y, PieceType type)
    {
        Vector3 pos = field.CellToWorld(new Vector3Int(GetXInGridPos(x), GetYInGridPos(y), 0));
        pos.x += grid.cellSize.x / 2;
        pos.y += grid.cellSize.y / 2;
        Piece newPiece = Instantiate(piecePrefabDict[type],
            pos, Quaternion.identity);
        newPiece.transform.parent = transform;
        newPiece.name = $"Piece [{x}, {y}]";
        newPiece.Init(x, y, type);

        return newPiece;
    }

    private int GetXInGridPos(int x)
    {
        return (x + bounds.xMin);
    }

    private int GetYInGridPos(int y)
    {
        return (bounds.yMax - y);
    }

    public bool CheckTypeOfPieceInGrid(int x, int y, PieceType type)
    {
        if (pieces[x, y] == null)
            return false;

        return pieces[x, y].Type == type;
    }

    public void DeletePiece(int x, int y)
    {
        PieceType type = pieces[x, y].Type;

        Destroy(pieces[x, y]);
        pieces[x, y] = null;

        if (type != PieceType.Empty)
        {
            pieces[x, y] = SpawnNewPiece(x, y, PieceType.Empty);
        }
    }
}
