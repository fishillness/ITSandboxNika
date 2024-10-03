using UnityEngine;
using UnityEngine.UI;

public class GUICityPanel : MonoBehaviour
{
    [SerializeField] private Button match3Button;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        match3Button.onClick.AddListener(OpenMatch3);
        mainMenuButton.onClick.AddListener(OpenMainMenu);
    }

    private void OnDestroy()
    {
        match3Button.onClick.RemoveListener(OpenMatch3);
        mainMenuButton.onClick.RemoveListener(OpenMainMenu);
    }

    private void OpenMatch3()
    {
        SceneController.LoadMatch3();
    }

    private void OpenMainMenu()
    {
        SceneController.LoadMainMenu();
    }
}
