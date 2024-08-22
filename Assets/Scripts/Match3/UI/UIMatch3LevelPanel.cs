using TMPro;
using UnityEngine;

public class UIMatch3LevelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLevelNumber;
    [SerializeField] private TextMeshProUGUI textMoves;
    [SerializeField] private GameObject taskGroupPrefab;
    [SerializeField] private UITaskInfo taskInfoPrefab;

    public void SetProperties(int levelNumber, int startMoves)
    {
        textLevelNumber.text = levelNumber.ToString();
        textMoves.text = levelNumber.ToString();


    }

    public void UpdateMoves(int moves)
    {
        Debug.Log($"UpdateMoves: {moves}");
        textMoves.text = moves.ToString();
    }
}
