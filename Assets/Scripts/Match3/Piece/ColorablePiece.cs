using UnityEngine;

public class ColorablePiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private ColorType color;
    private PieceColorDictionary colorDictionary;

    public ColorType Color => color;
    public bool IsColorDictionarySet => colorDictionary != null;

    public void SetColorDictionary(PieceColorDictionary colorDictionary)
    {
        this.colorDictionary = colorDictionary;
    }

    public void SetColor(ColorType color)
    {
        var sprite = colorDictionary.GetSpriteByT(color);
        this.color = color;

        if (sprite != null)
            spriteRenderer.sprite = sprite;
    }
}
