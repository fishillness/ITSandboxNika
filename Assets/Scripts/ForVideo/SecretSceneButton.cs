using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SecretSceneButton : MonoBehaviour
{
    private const string oldSity = "OldSity";
    private const string newSity = "NewCity";
    private const string animal = "LavaWufi";
    private const string go = "Go";

    [SerializeField] private Button oldSityButton;
    [SerializeField] private Button newSityButton;
    [SerializeField] private Button animalButton;
    [SerializeField] private Button goButton;

    private void Awake()
    {
        oldSityButton.onClick.AddListener(OpenOldSity);
        newSityButton.onClick.AddListener(OpenNewSity);
        animalButton.onClick.AddListener(OpenAnimal);
        goButton.onClick.AddListener(OpenGo);
    }

    private void OnDestroy()
    {
        oldSityButton.onClick.RemoveListener(OpenOldSity);
        newSityButton.onClick.RemoveListener(OpenNewSity);
        animalButton.onClick.RemoveListener(OpenAnimal);
        goButton.onClick.RemoveListener(OpenGo);
    }

    private void OpenOldSity()
    {
        SceneManager.LoadScene(oldSity);
    }

    private void OpenNewSity()
    {
        SceneManager.LoadScene(newSity);
    }

    private void OpenAnimal()
    {
        SceneManager.LoadScene(animal);
    }

    private void OpenGo()
    {
        SceneManager.LoadScene(go);
    }
}
