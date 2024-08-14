using UnityEngine;

public class Piece : MonoBehaviour
{
    private int x;
    private int y;
    private PieceType type;
    private ColorablePiece colorable;
    private MovablePiece movable;

    public int X => x;
    public int Y => y;
    public PieceType Type => type;
    public ColorablePiece Colorable => colorable;
    public bool IsColorable => colorable != null;
    public MovablePiece Movable => movable;
    public bool IsMovable => movable != null;

    private void Awake()
    {
        colorable = GetComponent<ColorablePiece>();
        movable = GetComponent<MovablePiece>();
    }

    public void Init(int x, int y, PieceType type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }

    public void SetX(int x)
    {
        this.x = x;
    }
    public void SetY(int y)
    {
        this.y = y;
    }
}
