using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuPanel : MonoBehaviour
{
    [SerializeField] private Button cityButton;

    private void Start()
    {
        cityButton.onClick.AddListener(OpenCity);
    }

    private void OnDestroy()
    {
        cityButton.onClick.RemoveListener(OpenCity);
    }

    private void OpenCity()
    {
        SceneController.LoadSceneCity();
    }
}
