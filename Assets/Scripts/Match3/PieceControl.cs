using UnityEngine;

public class PieceControl : MonoBehaviour,
    IDependency<FieldController>, IDependency<InputController>
{
    private FieldController field;

    private InputController inputControll;

    #region Constructs
    public void Construct(FieldController field) => this.field = field;
    public void Construct(InputController inputControll) => this.inputControll = inputControll;
    #endregion

    private Piece selectPiece;
    private Vector3 mousePosition;

    private void Awake()
    {
        if (inputControll == null)
            GlobalGameDependenciesContainer.Instance.Rebind(this);
    }

    private void Update()
    {
        mousePosition = inputControll.MousePosition;

        if (inputControll.IsTouchCountEquals2)
        {
            OnDown(inputControll.TouchZero.position);
            OnUp(inputControll.TouchOne.position);
        }
        else
        {
            if (inputControll.MouseButtonDown)
                OnDown(mousePosition);
            if (inputControll.MouseButtonUp)
                OnUp(mousePosition);
        }
    }
    
    private void OnDown(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position), Vector2.zero);

        if (hit.collider != null && field.IsLevelEnd && field.AreMovesAllowed)
        {
            Piece piece = hit.collider.GetComponent<Piece>();

            if (piece != null)
            {
                if (piece.IsClickable)
                {
                    if (selectPiece == null) 
                        SelectPiece(piece);
                    else if (piece == selectPiece)
                    {
                        Debug.Log("ƒаблклик не прописан");
                    }
                    else
                    {
                        bool isAdjacent = field.IsAdjacent(piece, selectPiece);
                        bool trySwap = field.SwapPieces(piece, selectPiece);

                        UnselectPiece();

                        if (!trySwap && isAdjacent)
                        {
                            Debug.Log("ƒобавит анимацию о том, что спавпнуть не полу4илось");
                        }

                        if (!isAdjacent)
                        {
                            SelectPiece(piece);
                        }
                    }
                }
            }
        }
        else
        {
            UnselectPiece();
        }
    }

    private void OnUp(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position), Vector2.zero);

        if (hit.collider != null)
        {
            Piece piece = hit.collider.GetComponent<Piece>();

            if (piece != null )
            {
                if (selectPiece == piece)
                    return;

                if (selectPiece == null)
                    return;

                bool isAdjacent = field.IsAdjacent(piece, selectPiece);
                bool trySwap = field.SwapPieces(piece, selectPiece);

                if (!trySwap && isAdjacent)
                {
                    Debug.Log("ƒобавит анимацию о том, что спавпнуть не полу4илось");
                }

                if (trySwap)
                    UnselectPiece();
            }
        }
    }

    private void SelectPiece(Piece piece)
    {
        if (selectPiece != null)
            UnselectPiece();

        piece.Clickable.SelectPiece();
        selectPiece = piece;
    }

    private void UnselectPiece()
    {
        if (selectPiece == null)
            return;

        selectPiece.Clickable.UnselectPiece();
        selectPiece = null;
    }
}
