using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UILevelButton : MonoBehaviour, IPointerClickHandler
{
    public event UnityAction<int> OnLevelButtonClick;

    [SerializeField] private TextMeshProUGUI text;

    private int levelNumber;

    public void SetProperties(int levelNumber)
    {
        text.text = levelNumber.ToString();
        name = levelNumber.ToString();
        this.levelNumber = levelNumber;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnLevelButtonClick?.Invoke(levelNumber);
    }
}
