using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldController : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap field;
    [SerializeField] private PiecePrefab[] piecePrefabs;
    [SerializeField] private Bound[] boundsElement;
    [SerializeField] private PieceColorDictionary colorDictionary;
    [SerializeField] private PiecesSpawnerController spawnerController;
    [SerializeField] private float droppingTime;

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

    private void Awake()
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

    private void Start()
    {
        spawnerController.CheckNeedOfSpawnPiece();
        StartCoroutine(DroppingPieces());
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
                    SpawnNewPiece(x, y, PieceType.Empty);
                }
                else
                    pieces[x, y] = null;
            }
        }
    }

    public void SpawnNewPiece(int x, int y, PieceType type)
    {
        Piece newPiece = Instantiate(piecePrefabDict[type],
            GetPiecePositionOnWorld(x, y), Quaternion.identity);
        newPiece.transform.parent = transform;
        newPiece.name = $"{type} Piece [{x}, {y}]";
        newPiece.Init(x, y, type);

        if (newPiece.Colorable != null)
        {
            if (!newPiece.Colorable.IsColorDictionarySet)
                newPiece.Colorable.SetColorDictionary(colorDictionary);

            newPiece.Colorable.SetColor((ColorType)UnityEngine.Random.Range(0, colorDictionary.NumberColors));
        }

        pieces[x, y] = newPiece;
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

        Destroy(pieces[x, y].gameObject);
        pieces[x, y] = null;

        if (type != PieceType.Empty)
        {
            SpawnNewPiece(x, y, PieceType.Empty);
        }
    }

    private IEnumerator DroppingPieces()
    {
        while (DropPieces())
        {
            yield return new WaitForSeconds(droppingTime);
        }
    }

    private bool DropPieces()
    {
        bool isPieceDrop = false;

        for (int y = yDim - 2; y >= 0; y--)
        {
            for (int x = 0; x < xDim; x++)
            {
                if (pieces[x, y] == null)
                    continue;

                if (pieces[x, y].IsMovable)
                {
                    Piece pieceBelow = pieces[x, y + 1];

                    if (pieceBelow.Type == PieceType.Empty)
                    {
                        DeletePiece(x, y + 1);
                        pieces[x, y].Movable.Move(x, y + 1, GetPiecePositionOnWorld(x, y + 1), droppingTime);
                        pieces[x, y + 1] = pieces[x, y];
                        pieces[x, y + 1].name = $"{pieces[x, y + 1].Type} Piece [{x}, {y + 1}]";

                        SpawnNewPiece(x, y, PieceType.Empty);
                        spawnerController.CheckNeedOfSpawnPieceAfterTime(droppingTime);
                        isPieceDrop = true;
                    }
                }
            }
        }
        
        return isPieceDrop;
    }

    private Vector2 GetPiecePositionOnWorld(int x, int y)
    {
        Vector3 pos = field.CellToWorld(new Vector3Int(GetXInGridPos(x), GetYInGridPos(y), 0));
        pos.x += grid.cellSize.x / 2;
        pos.y += grid.cellSize.y / 2;

        return pos;
    }
}
