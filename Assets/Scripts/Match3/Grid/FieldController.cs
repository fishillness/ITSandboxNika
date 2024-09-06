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
    [SerializeField] private PiecesSpawnerController spawnerController;
    [SerializeField] private float droppingTime;
    [SerializeField] private PieceCounter pieceCounter; //
    [SerializeField] private Match3Level level;
    [SerializeField] private PieceMatrixController matrixController;

    [Serializable, SerializeField]
    public struct PiecePrefab
    {
        public PieceType type;
        public Piece prefab;
    }

    public struct MatchingPieces
    {
        public List<Piece> matchPieces;
        public BoosterType boosterType;
    }

    private Dictionary<PieceType, Piece> piecePrefabDict;
    private BoundsInt bounds;
    private int xDim;
    private int yDim;
    private bool isLevelEnd = true;
    private bool areMovesAllowed = true;
    private bool continueDropping;
    
    public bool IsDroppingContinue => continueDropping;
    public Dictionary<PieceType, Piece>  PiecePrefabDict => piecePrefabDict;
    public float DroppingTime => droppingTime;  
    public bool IsLevelEnd => isLevelEnd;
    public bool AreMovesAllowed => areMovesAllowed;

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

        matrixController.InitMatrix(xDim, yDim);
        FillEmptyField();
    }

    private void Start()
    {
        spawnerController.CheckNeedOfSpawnPiece();
        StartDropPieces();
        level.OnStopMoves.AddListener(OnLevelEnd);
    }

    private void OnDestroy()
    {
        level.OnStopMoves.RemoveListener(OnLevelEnd);
    }

    private void OnLevelEnd()
    {
        isLevelEnd = false;
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
                    matrixController.SpawnNewPiece(x, y, PieceType.Empty);
                }
                else
                    matrixController.SetPieceNull(x, y);
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

    public void StartDropPieces()
    {
        if (IsDroppingContinue) return;
        if (matrixController.IsThereActiveBooster) return;
            
        StartCoroutine(DroppingPieces());
    }

    private IEnumerator DroppingPieces()
    {
        continueDropping = true;
        areMovesAllowed = false;

        while (continueDropping)
        {
            yield return new WaitForSeconds(droppingTime);

            while (DropPieces())
            {
                yield return new WaitForSeconds(droppingTime);

                if (spawnerController.CheckNeedOfSpawnPiece())
                    continueDropping = true;
            }

            continueDropping = ClearAllMatches();
            if (spawnerController.CheckNeedOfSpawnPiece())
                continueDropping = true;
        }

        areMovesAllowed = true;
        OnDropEnd?.Invoke();
    }

    private bool DropPieces()
    {
        bool isPieceDrop = false;

        for (int y = yDim - 2; y >= 0; y--)
        {
            for (int x = 0; x < xDim; x++)
            {
                if (matrixController.Pieces[x, y] == null)
                    continue;

                if (matrixController.Pieces[x, y].IsMovable)
                {
                    Piece pieceBelow = matrixController.Pieces[x, y + 1];

                    if (pieceBelow != null)
                    {
                        if (pieceBelow.Type == PieceType.Empty)
                        {
                            matrixController.SwapEmptyPieceWithNonEmpty(x, y + 1, x, y);
                            //spawnerController.CheckNeedOfSpawnPieceAfterTime(droppingTime);
                            isPieceDrop = true;
                        }
                    }
                    
                    if (isPieceDrop == false)
                    {
                        Piece pieceBelowRight = null;
                        Piece pieceBelowLeft = null;
                        Piece piece = null;

                        if (x + 1 < xDim && matrixController.Pieces[x + 1, y + 1] != null)
                        {
                            if (matrixController.Pieces[x + 1, y + 1].Type == PieceType.Empty)
                                pieceBelowRight = matrixController.Pieces[x + 1, y + 1];
                        }
                        
                        if (x - 1 >= 0 && matrixController.Pieces[x - 1, y + 1] != null)
                        {
                            if (matrixController.Pieces[x - 1, y + 1].Type == PieceType.Empty)
                                pieceBelowLeft = matrixController.Pieces[x - 1, y + 1];
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
                            matrixController.SwapEmptyPieceWithNonEmpty(piece.X, piece.Y, x, y);
                            //spawnerController.CheckNeedOfSpawnPieceAfterTime(droppingTime);
                            isPieceDrop = true;
                        }
                    }
                }
            }
        }

        return isPieceDrop;
    }

    public Vector2 GetPiecePositionOnWorld(int x, int y)
    {
        Vector3 pos = field.CellToWorld(new Vector3Int(GetXInGridPos(x), GetYInGridPos(y), 0));
        pos.x += grid.cellSize.x / 2;
        pos.y += grid.cellSize.y / 2;

        return pos;
    }

    private MatchingPieces FindMatch(Piece piece)
    {
        MatchingPieces matching = new MatchingPieces();
        matching.boosterType = BoosterType.None;
        if (piece == null)
        {
            matching.matchPieces = null;
            return matching;
        }
        if (piece.IsColorable == false)
        {
            matching.matchPieces = null;
            return matching;
        }

        ColorType color = piece.Colorable.Color;
        List<Piece> horizontelPieces = new List<Piece>();
        List<Piece> verticalPieces = new List<Piece>();
        List<Piece> matchingPieces = new List<Piece>();

        for (int x = piece.X + 1; x < xDim; x++)
        {
            Piece horPiece = matrixController.Pieces[x, piece.Y];
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
            Piece horPiece = matrixController.Pieces[x, piece.Y];
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
            Piece verPiece = matrixController.Pieces[piece.X, y];
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
            Piece verPiece = matrixController.Pieces[piece.X, y];
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

        matching.boosterType = IdentifyBooster(piece, ref horizontelPieces, verticalPieces);

        if (matching.boosterType != BoosterType.MiniBomb)
        {
            if (horizontelPieces.Count < 2)
                horizontelPieces.Clear();

            if (verticalPieces.Count < 2)
                verticalPieces.Clear();
        }


        foreach (Piece horPiece in horizontelPieces)
        {
            matchingPieces.Add(horPiece);
        }

        foreach (Piece verPiece in verticalPieces)
        {
            matchingPieces.Add(verPiece);
        }


        if (matchingPieces.Count < 3)
            matchingPieces.Clear();

        matching.matchPieces = matchingPieces;
        return matching;
    }

    private bool ClearAllMatches()
    {
        bool isMatchFound = false;


        for (int y = 0; y < yDim; y++)
        {
            for (int x = 0; x < xDim; x++)
            {
                if (matrixController.Pieces[x, y] == null)
                    continue;

                if (!matrixController.Pieces[x, y].IsMovable)
                    continue;

                MatchingPieces matchPieces = new MatchingPieces();
                matchPieces = FindMatch(matrixController.Pieces[x, y]);

                if (matchPieces.matchPieces != null
                    && matchPieces.matchPieces.Count > 0)
                {
                    matrixController.DeleteSomePieces(matchPieces.matchPieces, DestructionType.ByMatch);

                    isMatchFound = true;
                }

                if (matchPieces.boosterType != BoosterType.None)
                {
                    matrixController.SpawnNewBooster(matchPieces.matchPieces[0].X, matchPieces.matchPieces[0].Y, matchPieces.boosterType);
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
        if (!isLevelEnd) return false;
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

        matrixController.SwapPiecesOnlyInMatrix(piece1, piece2);

        Vector2Int piece1XY = new Vector2Int(piece1.X, piece1.Y);
        Vector2Int piece2XY = new Vector2Int(piece2.X, piece2.Y);

        piece1.Movable.Move(piece2XY.x, piece2XY.y, GetPiecePositionOnWorld(piece2XY.x, piece2XY.y), droppingTime);
        piece2.Movable.Move(piece1XY.x, piece1XY.y, GetPiecePositionOnWorld(piece1XY.x, piece1XY.y), droppingTime);

        MatchingPieces matchPiece1 = new MatchingPieces();
        matchPiece1 = FindMatch(matrixController.Pieces[piece1.X, piece1.Y]);

        MatchingPieces matchPiece2 = new MatchingPieces();
        matchPiece2 = FindMatch(matrixController.Pieces[piece2.X, piece2.Y]);



        if ((matchPiece1.matchPieces != null && matchPiece1.matchPieces.Count >= 3) 
            || (matchPiece2.matchPieces != null && matchPiece2.matchPieces.Count >= 3)
            || piece1.IsBooster || piece2.IsBooster)
        {
            if (matchPiece1.matchPieces != null)
            {
                matrixController.DeleteSomePieces(matchPiece1.matchPieces, DestructionType.ByMatch);
            }
            if (matchPiece2.matchPieces != null)
            {
                matrixController.DeleteSomePieces(matchPiece2 .matchPieces, DestructionType.ByMatch);
            }

            if (matchPiece1.boosterType != BoosterType.None)
            {
                matrixController.SpawnNewBooster(matchPiece1.matchPieces[0].X, matchPiece1.matchPieces[0].Y, matchPiece1.boosterType);
            }
            else if (matchPiece2.boosterType != BoosterType.None)
            {
                matrixController.SpawnNewBooster(matchPiece2.matchPieces[0].X, matchPiece2.matchPieces[0].Y, matchPiece2.boosterType);
            }

            if (piece1.IsBooster)
                piece1.Booster.Activate(piece2);
            if (piece2.IsBooster) 
                piece2.Booster.Activate(piece1);

            StartDropPieces();
            OnMove?.Invoke();

            return true;
        }
        else
        {
            matrixController.SwapPiecesOnlyInMatrix(piece1, piece2);

            piece1.Movable.Move(piece1XY.x, piece1XY.y, GetPiecePositionOnWorld(piece1XY.x, piece1XY.y), droppingTime);
            piece2.Movable.Move(piece2XY.x, piece2XY.y, GetPiecePositionOnWorld(piece2XY.x, piece2XY.y), droppingTime);

            return false;
        }
    }
    
    private BoosterType IdentifyBooster(Piece piece, ref List<Piece> horizontelPieces, List<Piece> verticalPieces)
    {
        BoosterType boosterType = BoosterType.None;

        /*
        // Check the box
        for (int i = 0; i < horizontelPieces.Count; i++)
        {
            for (int j = 0; j < verticalPieces.Count; j++)
            {
                Piece diagPiece;
                if (piece.X + 1 < xDim && piece.Y + 1 < yDim)
                {
                    diagPiece = pieces[piece.X + 1, piece.Y + 1];
                    if (horizontelPieces[i].X == piece.X + 1 && verticalPieces[j].Y == piece.Y + 1
                        && diagPiece != null)
                    {
                        if (diagPiece.IsColorable)
                        {
                            if (diagPiece.Colorable.Color == piece.Colorable.Color)
                            {
                                boosterType = BoosterType.MiniBomb;
                                horizontelPieces.Add(diagPiece);
                            }
                        }
                    }
                }

                if (piece.X - 1 >= 0 && piece.Y + 1 < yDim)
                {
                    diagPiece = pieces[piece.X - 1, piece.Y + 1];
                    if (horizontelPieces[i].X == piece.X - 1 && verticalPieces[j].Y == piece.Y + 1
                        && diagPiece != null)
                    {
                        if (diagPiece.IsColorable)
                        {
                            if (diagPiece.Colorable.Color == piece.Colorable.Color)
                            {
                                boosterType = BoosterType.MiniBomb;
                                horizontelPieces.Add(diagPiece);
                            }
                        }
                    }
                }

                if (piece.X + 1 < xDim && piece.Y - 1 >= 0)
                {
                    diagPiece = pieces[piece.X + 1, piece.Y - 1];
                    if (horizontelPieces[i].X == piece.X + 1 && verticalPieces[j].Y == piece.Y - 1
                        && diagPiece != null)
                    {
                        if (diagPiece.IsColorable)
                        {
                            if (diagPiece.Colorable.Color == piece.Colorable.Color)
                            {
                                boosterType = BoosterType.MiniBomb;
                                horizontelPieces.Add(diagPiece);
                            }
                        }
                    }
                }

                if (piece.X - 1 >= 0 && piece.Y - 1 >= 0)
                {
                    diagPiece = pieces[piece.X - 1, piece.Y - 1];
                    if (horizontelPieces[i].X == piece.X - 1 && verticalPieces[j].Y == piece.Y - 1
                        && diagPiece != null)
                    {
                        if (diagPiece.IsColorable)
                        {
                            if (diagPiece.Colorable.Color == piece.Colorable.Color)
                            {
                                boosterType = BoosterType.MiniBomb;
                                horizontelPieces.Add(diagPiece);
                            }
                        }
                    }
                }
            }
        }
        */
        
        // Check the line of four
        if (verticalPieces.Count == 3 && horizontelPieces.Count < 1)
        {
            boosterType = BoosterType.VerticalRocket;
        }

        if (horizontelPieces.Count == 3 && verticalPieces.Count < 1)
        {
            boosterType = BoosterType.HorizontalRocket;
        }

        // Check the T or L shape
        if (verticalPieces.Count >= 2 && horizontelPieces.Count >= 2)
        {
            boosterType = BoosterType.MaxiBomb;
        }

        /*
        // Check the line of five
        if (verticalPieces.Count == 4 || horizontelPieces.Count == 4)
        {
            boosterType = BoosterType.Rainbow;
        }
        */
        return boosterType;
    }
}
