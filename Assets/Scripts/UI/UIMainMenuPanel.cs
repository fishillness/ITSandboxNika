using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuPanel : MonoBehaviour
{
    [SerializeField] private Button cityButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        cityButton.onClick.AddListener(OpenCity);
        quitButton.onClick.AddListener(CloseCity);
    }

    private void OnDestroy()
    {
        cityButton.onClick.RemoveListener(OpenCity);
        quitButton.onClick.RemoveListener(CloseCity);
    }

    private void OpenCity()
    {
        SceneController.LoadSceneCity();
    }

    private void CloseCity()
    {
        Application.Quit();
    }
}
