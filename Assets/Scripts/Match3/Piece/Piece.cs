using UnityEngine;

public class Piece : MonoBehaviour
{
    private int x;
    private int y;
    private PieceType type;

    public int X => x;
    public int Y => y;
    public PieceType Type => type;

    public void Init(int x, int y, PieceType type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
