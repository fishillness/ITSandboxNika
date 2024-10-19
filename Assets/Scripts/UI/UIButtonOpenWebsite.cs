using UnityEngine;
using UnityEngine.UI;

public class UIButtonOpenWebsite : MonoBehaviour
{
    private const string nikaWebSiteLink = "https://fond-nika.ru/";

    [SerializeField] private Button openWebsiteButton;

    private void Start()
    {
        openWebsiteButton.onClick.AddListener(OpenWebSite);
    }

    private void OnDestroy()
    {
        openWebsiteButton.onClick.RemoveListener(OpenWebSite);
    }

    private void OpenWebSite()
    {
        Application.OpenURL(nikaWebSiteLink);
    }
}
