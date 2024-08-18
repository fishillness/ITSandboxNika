using Unity.VisualScripting;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private int x;
    private int y;
    private PieceType type;
    private FieldController field;
    private ColorablePiece colorable;
    private MovablePiece movable;
    private ClerablePiece clerable;

    public int X => x;
    public int Y => y;
    public PieceType Type => type;
    public FieldController Field => field;
    public ColorablePiece Colorable => colorable;
    public bool IsColorable => colorable != null;
    public MovablePiece Movable => movable;
    public bool IsMovable => movable != null;
    public ClerablePiece Clerable => clerable;
    public bool IsClerable => clerable != null;

    private void Awake()
    {
        colorable = GetComponent<ColorablePiece>();
        movable = GetComponent<MovablePiece>();
        clerable = GetComponent<ClerablePiece>();
    }

    public void Init(int x, int y, PieceType type, FieldController field)
    {
        this.x = x;
        this.y = y;
        this.type = type;
        this.field = field;
        UpdateName();
    }

    public void SetX(int x)
    {
        this.x = x;
    }
    public void SetY(int y)
    {
        this.y = y;
    }

    public void UpdateName()
    {
        gameObject.name = $"{type} Piece [{x}, {y}]"; ;
    }
}
