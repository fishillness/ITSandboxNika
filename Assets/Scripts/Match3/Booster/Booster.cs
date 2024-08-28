using UnityEngine;

public class Booster : MonoBehaviour
{
    private BoosterType type;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetBoosterType(BoosterType type)
    {
        this.type = type;
    }

    public void SetBoosterSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
