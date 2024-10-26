using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUICityPanel : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;


    private void Start()
    {
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OpenMainMenu);
    }

    private void OnDestroy()
    {
        if (mainMenuButton != null)
            mainMenuButton.onClick.RemoveListener(OpenMainMenu);
    }


    private void OpenMainMenu()
    {
        SceneController.LoadMainMenu();
    }
}
