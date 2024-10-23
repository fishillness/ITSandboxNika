using UnityEngine;
using UnityEngine.UI;

public class VideoSceneCanvas : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(OpenCity);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OpenCity);
    }

    private void OpenCity()
    {
        SceneController.LoadSceneCity();
    }
}
