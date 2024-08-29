using UnityEngine;

public class Booster : MonoBehaviour
{
    protected BoosterType type;
    private SpriteRenderer spriteRenderer;
    private Piece piece;
    private PieceMatrixController matrixController;

    private bool isActivated = false;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        piece = GetComponentInChildren<Piece>();

        if (piece.IsClerable)
            piece.Clerable.OnPieceStartClear.AddListener(OnPieceStartClear);
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
            matrixController.DeleteRow(piece.X, piece.Y);
        }
        else if (type == BoosterType.VerticalRocket)
        {
            matrixController.DeleteColumn(piece.X, piece.Y);
        }
        else if (type == BoosterType.MiniBomb)
        {
            matrixController.DeleteNearPiece(piece.X, piece.Y);
        }
        else if (type == BoosterType.MaxiBomb)
        {
            matrixController.DeleteManyNearPieces(piece.X, piece.Y);
        }
        else if (type == BoosterType.Rainbow)
        {
            matrixController.DeleteAllPiecesByColor(piece.X, piece.Y, swapPiece);
        }
    }
}
