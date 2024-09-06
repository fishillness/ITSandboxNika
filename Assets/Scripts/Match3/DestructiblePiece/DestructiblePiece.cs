using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DestructiblePiece : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<Piece> OnPieceDestroy;
    [HideInInspector]
    public UnityEvent<Piece> OnPieceStartDestroying;

    [SerializeField] private DestructionType[] destructionTypeWhichDestroyPiece;
    protected Animator animator;
    protected Piece piece;
    protected bool isDestroying = false;
    protected string destroyTriggerName = "Destroy";

    public bool IsDestroying => isDestroying;

    private void Awake()
    {
        piece = GetComponent<Piece>();
        animator = GetComponent<Animator>();
    }

    public void DestroyPiece(DestructionType type)
    {
        if (!IsPieceDestroyThisType(type)) return;
        if (isDestroying) return;

        isDestroying = true;
        OnPieceStartDestroying?.Invoke(piece);
        animator.SetTrigger(destroyTriggerName);
    }

    public bool IsPieceDestroyThisType(DestructionType type)
    {
        return destructionTypeWhichDestroyPiece.Contains(type);
    }

    public void OnDestroyAnimationEnd()
    {
        OnPieceDestroy?.Invoke(piece);
        Destroy(piece.gameObject);
    }

    public void DestroyImmediately()
    {
        isDestroying = true;
        OnDestroyAnimationEnd();
    }
}
