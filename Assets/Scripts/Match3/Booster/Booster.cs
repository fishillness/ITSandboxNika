using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private float timeForDestroyingPiecesForRockets = 0.2f;
    [SerializeField] private float timeForDestroyingPiecesForBomb = 0.1f;
    [SerializeField] private float timeForDestroyingPiecesForRaindow = 0.2f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected BoosterType type;
    private Piece piece;
    private PieceMatrixController matrixController;

    private bool isActivated = false;

    private void Awake()
    {
        piece = GetComponentInChildren<Piece>();

        if (piece.IsDestructible)
            piece.Destructible.OnPieceStartDestroying.AddListener(OnPieceStartClear);
    }

    public void SetProperties(BoosterType type, PieceMatrixController matrixController)
    {
        this.type = type;
        this.matrixController = matrixController;
    }

    public void SetBoosterSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    private void OnPieceStartClear(Piece piece)
    {
        Activate(piece);
    }

    public void Activate(Piece swapPiece)
    {
        if (isActivated) return;
        isActivated = true;

        if (type == BoosterType.HorizontalRocket)
        {
            matrixController.DeleteRow(piece.X, piece.Y, timeForDestroyingPiecesForRockets);
        }
        else if (type == BoosterType.VerticalRocket)
        {
            matrixController.DeleteColumn(piece.X, piece.Y, timeForDestroyingPiecesForRockets);
        }
        else if (type == BoosterType.MiniBomb)
        {
            matrixController.DeleteNearPiece(piece.X, piece.Y);
        }
        else if (type == BoosterType.MaxiBomb)
        {
            matrixController.DeleteManyNearPieces(piece.X, piece.Y, timeForDestroyingPiecesForBomb);
        }
        else if (type == BoosterType.Rainbow)
        {
            matrixController.DeleteAllPiecesByColor(piece.X, piece.Y, swapPiece, timeForDestroyingPiecesForRaindow);
        }
    }
}
