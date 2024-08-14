using UnityEngine;

public class MovablePiece : MonoBehaviour
{
    private Piece piece;

    private void Awake()
    {
        piece = GetComponent<Piece>();
    }

    public void Move(int newX, int newY)
    {
        piece.SetX(newX);
        piece.SetY(newY);
    }
}
