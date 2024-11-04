using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuButton : MonoBehaviour
{

    private Button mainMenuButton;

    private void Start()
    {
        mainMenuButton = GetComponent<Button>();

        mainMenuButton.gameObject.SetActive(SceneController.GetActiveScene() == SceneController.CitySceneTitle);

        mainMenuButton.onClick.AddListener(OpenMainMenu); 
    }

    private void OnDestroy()
    {
        mainMenuButton.onClick.RemoveListener(OpenMainMenu);
    }

    private void OpenMainMenu()
    {
        SceneController.LoadMainMenu();
    }
}
