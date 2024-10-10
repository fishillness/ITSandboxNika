using UnityEngine;

public class UIBottomPanelButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;

    public void HideButtons()
    {
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }
    }
}
