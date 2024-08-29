using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Animator), typeof(Piece))]
public class ClerablePiece : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<Piece> OnPieceClear;
    [HideInInspector]
    public UnityEvent<Piece> OnPieceStartClear;

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
        OnPieceStartClear?.Invoke(piece);
    }

    public void OnClearAnimationEnd()
    {
        OnPieceClear?.Invoke(piece);
        Destroy(piece.gameObject);
    }
}
