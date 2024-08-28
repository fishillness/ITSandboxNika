using UnityEngine;

public class ColorablePiece : MonoBehaviour
{
    private ColorType color;
    private SpriteRenderer spriteRenderer;
    private PieceColorDictionary colorDictionary;

    public ColorType Color => color;
    public bool IsColorDictionarySet => colorDictionary != null;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

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
