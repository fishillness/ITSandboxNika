using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Animator), typeof(Piece))]
public class ClerablePiece : MonoBehaviour
{
    public UnityEvent<Piece> OnPieceClear;

    private Animator animator;
    private Piece piece;
    private bool isClearing = false;
    private string clearTriggerName = "Clear";

    public bool IsClearing => isClearing;

    private void Awake()
    {
        piece = GetComponent<Piece>();
        animator = GetComponent<Animator>();
    }

    public void ClearPiece()
    {
        if (isClearing) return;

        isClearing = true;
        animator.SetTrigger(clearTriggerName);
    }

    public void OnClearAnimationEnd()
    {
        OnPieceClear?.Invoke(piece);
        piece.Field.DeletePiece(piece.X, piece.Y);
        //Destroy(piece.gameObject);
    }
}
