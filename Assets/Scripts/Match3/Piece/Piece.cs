using UnityEngine;

public class Piece : MonoBehaviour
{
    private int x;
    private int y;
    private PieceType type;
    private ColorablePiece colorable;
    private MovablePiece movable;
    private ClerablePiece clerable;
    private ClickablePiece clickable;
    private Booster booster;

    public int X => x;
    public int Y => y;
    public PieceType Type => type;
    public ColorablePiece Colorable => colorable;
    public bool IsColorable => colorable != null;
    public MovablePiece Movable => movable;
    public bool IsMovable => movable != null;
    public ClerablePiece Clerable => clerable;
    public bool IsClerable => clerable != null;
    public ClickablePiece Clickable => clickable;
    public bool IsClickable => clickable != null;
    public Booster Booster => booster;
    public bool IsBooster => booster != null;

    private void Awake()
    {
        colorable = GetComponent<ColorablePiece>();
        movable = GetComponent<MovablePiece>();
        clerable = GetComponent<ClerablePiece>();
        clickable = GetComponent<ClickablePiece>();
        booster = GetComponent<Booster>();
    }

    public void Init(int x, int y, PieceType type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
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
