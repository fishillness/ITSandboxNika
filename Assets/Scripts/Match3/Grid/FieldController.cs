using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class FieldController : MonoBehaviour
{
    [HideInInspector] 
    public UnityEvent OnMove;
    [HideInInspector]
    public UnityEvent OnDropEnd;

    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap field;
    [SerializeField] private PiecePrefab[] piecePrefabs;
    [SerializeField] private Bound[] boundsElement;
    [SerializeField] private PieceColorDictionary colorDictionary;
    [SerializeField] private PiecesSpawnerController spawnerController;
    [SerializeField] private float droppingTime;
    [SerializeField] private PieceCounter pieceCounter;
    [SerializeField] private Match3Level level;

    [Serializable, SerializeField]
    public struct PiecePrefab
    {
        public PieceType type;
        public Piece prefab;
    }

    private Dictionary<PieceType, Piece> piecePrefabDict;
    private Piece[,] pieces;
    private BoundsInt bounds;
    private int xDim;
    private int yDim;
    private bool areMovesAllowed = true;
    private bool continueDropping;

    public bool IsDroppingContinue => continueDropping;

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
        pieceCounter.InitDictionery(piecePrefabs);

        SetBounds();

        pieces = new Piece[xDim, yDim];
        FillEmptyField();
    }

    private void Start()
    {
        spawnerController.CheckNeedOfSpawnPiece();
        StartCoroutine(DroppingPieces());
        level.OnStopMoves.AddListener(OnLevelEnd);
    }

    private void OnDestroy()
    {
        level.OnStopMoves.RemoveListener(OnLevelEnd);
    }

    private void OnLevelEnd()
    {
        areMovesAllowed = false;
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
        newPiece.Init(x, y, type);

        if (newPiece.Colorable != null)
        {
            if (!newPiece.Colorable.IsColorDictionarySet)
                newPiece.Colorable.SetColorDictionary(colorDictionary);

            newPiece.Colorable.SetColor((ColorType)UnityEngine.Random.Range(0, colorDictionary.NumberColors));
        }

        pieces[x, y] = newPiece;
        pieceCounter.AddPiece(newPiece);
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

        if (pieces[x, y].Clerable == null)
            Destroy(pieces[x, y].gameObject);
        else
            pieces[x, y].Clerable.ClearPiece();

        pieces[x, y] = null;
        if (type != PieceType.Empty)
        {
            SpawnNewPiece(x, y, PieceType.Empty);
        }
    }

    private IEnumerator DroppingPieces()
    {
        continueDropping = true;

        while (continueDropping)
        {
            yield return new WaitForSeconds(droppingTime);

            while (DropPieces())
            {
                yield return new WaitForSeconds(droppingTime);
            }

            continueDropping = ClearAllMatches();
            if (spawnerController.CheckNeedOfSpawnPiece())
                continueDropping = true;
        }

        OnDropEnd?.Invoke();
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

                    if (pieceBelow != null)
                    {
                        if (pieceBelow.Type == PieceType.Empty)
                        {
                            SwapEmptyPieceWithNonEmpty(x, y + 1, x, y);
                            spawnerController.CheckNeedOfSpawnPieceAfterTime(droppingTime);
                            isPieceDrop = true;
                        }
                    }
                    
                    if (isPieceDrop == false)
                    {
                        Piece pieceBelowRight = null;
                        Piece pieceBelowLeft = null;
                        Piece piece = null;

                        if (x + 1 < xDim && pieces[x + 1, y + 1] != null)
                        {
                            if (pieces[x + 1, y + 1].Type == PieceType.Empty)
                                pieceBelowRight = pieces[x + 1, y + 1];
                        }
                        
                        if (x - 1 >= 0 && pieces[x - 1, y + 1] != null)
                        {
                            if (pieces[x - 1, y + 1].Type == PieceType.Empty)
                                pieceBelowLeft = pieces[x - 1, y + 1];
                        }

                        if (pieceBelowRight != null && pieceBelowLeft == null)
                            piece = pieceBelowRight;
                        else if (pieceBelowLeft != null && pieceBelowRight == null)
                            piece = pieceBelowLeft;
                        else if (pieceBelowLeft != null && pieceBelowRight != null)
                        {
                            int random = UnityEngine.Random.Range(0, 2);

                            if (random == 0)
                                piece = pieceBelowRight;
                            else 
                                piece = pieceBelowLeft;
                        }

                        if (piece != null)
                        {
                            SwapEmptyPieceWithNonEmpty(piece.X, piece.Y, x, y);
                            spawnerController.CheckNeedOfSpawnPieceAfterTime(droppingTime);
                            isPieceDrop = true;
                        }
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

    private void SwapEmptyPieceWithNonEmpty(int xEmpty, int yEmpty, int xNonEmpty, int yNonEmpty)
    {
        DeletePiece(xEmpty, yEmpty);
        pieces[xNonEmpty, yNonEmpty].Movable.Move(xEmpty, yEmpty, GetPiecePositionOnWorld(xEmpty, yEmpty), droppingTime);
        pieces[xEmpty, yEmpty] = pieces[xNonEmpty, yNonEmpty];

        SpawnNewPiece(xNonEmpty, yNonEmpty, PieceType.Empty);
    }

    private List<Piece> FindMatch(Piece piece)
    {
        if (piece == null)
            return null;
        if (piece.IsColorable == false)
            return null;

        ColorType color = piece.Colorable.Color;
        List<Piece> horizontelPieces = new List<Piece>();
        List<Piece> verticalPieces = new List<Piece>();
        List<Piece> matchingPieces = new List<Piece>();

        for (int x = piece.X + 1; x < xDim; x++)
        {
            Piece horPiece = pieces[x, piece.Y];
            if (horPiece == null)
                break;
            if (!horPiece.IsColorable)
                break;
            if (horPiece == piece)
                continue;

            if (horPiece.Colorable.Color == color)
                horizontelPieces.Add(horPiece);
            else
                break;
        }

        for (int x = piece.X - 1; x >= 0; x--)
        {
            Piece horPiece = pieces[x, piece.Y];
            if (horPiece == null)
                break;
            if (!horPiece.IsColorable)
                break;
            if (horPiece == piece)
                continue;

            if (horPiece.Colorable.Color == color)
                horizontelPieces.Add(horPiece);
            else
                break;
        }

        for (int y = piece.Y + 1; y < yDim; y++)
        {
            Piece verPiece = pieces[piece.X, y];
            if (verPiece == null)
                break;
            if (!verPiece.IsColorable)
                break;
            if (verPiece == piece)
                continue;

            if (verPiece.Colorable.Color == color)
                verticalPieces.Add(verPiece);
            else
                break;
        }

        for (int y = piece.Y - 1; y >= 0; y--)
        {
            Piece verPiece = pieces[piece.X, y];
            if (verPiece == null)
                break;

            if (!verPiece.IsColorable)
                break;
            if (verPiece == piece)
                continue;

            if (verPiece.Colorable.Color == color)
                verticalPieces.Add(verPiece);
            else
                break;
        }

        matchingPieces.Add(piece);

        if (horizontelPieces.Count < 2)
            horizontelPieces.Clear();
        else
        {
            foreach(Piece horPiece in horizontelPieces)
            {
                matchingPieces.Add(horPiece);
            }
        }

        if (verticalPieces.Count < 2)
            verticalPieces.Clear();
        else
        {
            foreach (Piece verPiece in verticalPieces)
            {
                matchingPieces.Add(verPiece);
            }
        }

        if (matchingPieces.Count < 3)
            matchingPieces.Clear();

        return matchingPieces;
    }

    private bool ClearAllMatches()
    {
        bool isMatchFound = false;


        for (int y = 0; y < yDim; y++)
        {
            for (int x = 0; x < xDim; x++)
            {
                if (pieces[x, y] == null)
                    continue;

                if (!pieces[x, y].IsMovable)
                    continue;

                List<Piece> matchPPieces = new List<Piece>();
                matchPPieces = FindMatch(pieces[x, y]);

                if (matchPPieces.Count > 0)
                {
                    foreach(Piece piece in matchPPieces)
                    {
                        DeletePiece(piece.X, piece.Y);
                    }

                    isMatchFound = true;
                }
            }
        }
        return isMatchFound;
    }

    public bool IsAdjacent(Piece piece1, Piece piece2)
    {
        return (piece1.X == piece2.X && (int)Mathf.Abs(piece1.Y - piece2.Y) == 1)
            || (piece1.Y == piece2.Y && (int)Mathf.Abs(piece1.X - piece2.X) == 1);
    }

    public bool SwapPieces(Piece piece1, Piece piece2)
    {
        if (!areMovesAllowed) return false;
        if (piece1 == null || piece2 == null)
        {
            Debug.Log($"Кто-то нуловьй. piece1: {piece1}, piece2: {piece2}");
            return false;
        }
        if (piece1.IsMovable == false || piece2.IsMovable == false)
        {
            Debug.Log($"Кто-то не мувобал. piece1: {piece1.IsMovable}, piece2: {piece2.IsMovable}");
            return false;
        }
        if (IsAdjacent(piece1, piece2) == false)
        {
            Debug.Log("Не р9дом");
            return false;
        }

        pieces[piece1.X, piece1.Y] = piece2;
        pieces[piece2.X, piece2.Y] = piece1;

        Vector2Int piece1XY = new Vector2Int(piece1.X, piece1.Y);
        Vector2Int piece2XY = new Vector2Int(piece2.X, piece2.Y);

        piece1.Movable.Move(piece2XY.x, piece2XY.y, GetPiecePositionOnWorld(piece2XY.x, piece2XY.y), droppingTime);
        piece2.Movable.Move(piece1XY.x, piece1XY.y, GetPiecePositionOnWorld(piece1XY.x, piece1XY.y), droppingTime);

        List<Piece> matchPiece1 = new List<Piece>();
        matchPiece1 = FindMatch(pieces[piece1.X, piece1.Y]);

        List<Piece> matchPiece2 = new List<Piece>();
        matchPiece2 = FindMatch(pieces[piece2.X, piece2.Y]);


        if (matchPiece1.Count >= 3 || matchPiece2.Count >= 3)
        {
            StartCoroutine(DroppingPieces());
            OnMove?.Invoke();

            return true;
        }
        else
        {
            pieces[piece1.X, piece1.Y] = piece2;
            pieces[piece2.X, piece2.Y] = piece1;

            piece1.Movable.Move(piece1XY.x, piece1XY.y, GetPiecePositionOnWorld(piece1XY.x, piece1XY.y), droppingTime);
            piece2.Movable.Move(piece2XY.x, piece2XY.y, GetPiecePositionOnWorld(piece2XY.x, piece2XY.y), droppingTime);

            return false;
        }
    }
}
