using UnityEngine;

public class DestructibleObstaclePiece : DestructiblePiece
{
    [SerializeField] private Sprite[] spriteStages;
    [SerializeField] private SpriteRenderer changeableSprite;
    
    private int currentStage;

    protected override void Awake()
    {
        base.Awake();

        isLastStage = false;
        currentStage = 0;
        changeableSprite.sprite = spriteStages[currentStage];
    }

    public override void DamagePiece(DestructionType type)
    {
        if (!IsPieceDestroyThisType(type)) return;
        if (isDestroying) return;

        if (currentStage >= spriteStages.Length - 1)
        {
            currentStage = spriteStages.Length - 1;
            changeableSprite.sprite = spriteStages[currentStage];
            DestroyPiece();

            return;
        }

        currentStage++;
        changeableSprite.sprite = spriteStages[currentStage];
        isLastStage = currentStage == spriteStages.Length - 1;
    }
}
