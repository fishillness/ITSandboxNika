using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class ClickablePiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer selectedFrame;

    private bool IsSelect;

    private void Start()
    {
        UnselectPiece();
    }

    public void SelectPiece()
    {
        IsSelect = true;
        selectedFrame.enabled = true;
    }

    public void UnselectPiece()
    {
        IsSelect = false;
        selectedFrame.enabled = false;
    }
}
