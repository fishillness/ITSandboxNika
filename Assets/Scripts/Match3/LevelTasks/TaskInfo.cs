using System;
using UnityEngine;

[Serializable, SerializeField]
public class TaskInfo
{
    [SerializeField] private TaskInfoType taskType;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int count;
    [SerializeField] private PieceType type;

    /// <summary>
    /// If taskType = By Color
    /// </summary>
    [SerializeField] private ColorType colorType;

    private int currentCount;

    public PieceType Type => type;
    public Sprite Sprite => sprite;
    public int Count => count;
    public int CurrentCount => currentCount;
    public TaskInfoType TaskType => taskType;
    public ColorType ColorType => colorType;

    public void SetProperties()
    {
        currentCount = count;
    }

    public bool RemovePiece()
    {
        if (currentCount == 0)
            return false;

        bool isLastPiece = false;
        currentCount--;

        if (currentCount == 0)
            isLastPiece = true;

        return isLastPiece;
    }
}
