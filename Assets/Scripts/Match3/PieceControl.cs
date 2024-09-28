using UnityEngine;

public class PieceControl : MonoBehaviour,
    IDependency<FieldController>
{
    private FieldController field;
    [SerializeField] private GameObject inputControll;

    #region Constructs
    public void Construct(FieldController field) => this.field = field;
    #endregion

    private Camera camera;
    private Piece selectPiece;

    //����� ����� �����i� �� ����������������
    private Vector3 mousePosition;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        //����� ����� �����i� �� ����������������
        
        mousePosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
            OnMouseButtonDown();
        if (Input.GetMouseButtonUp(0))
            OnMouseButtonUp();
        //
    }
    
    private void OnMouseButtonDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

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
                        Debug.Log("�������� �� ��������");
                    }
                    else
                    {
                        bool isAdjacent = field.IsAdjacent(piece, selectPiece);
                        bool trySwap = field.SwapPieces(piece, selectPiece);

                        UnselectPiece();

                        if (!trySwap && isAdjacent)
                        {
                            Debug.Log("������� �������� � ���, ��� ��������� �� ����4�����");
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

    private void OnMouseButtonUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

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
                    Debug.Log("������� �������� � ���, ��� ��������� �� ����4�����");
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
