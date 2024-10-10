using UnityEngine;
using UnityEngine.UI;

public class NotepadController : MonoBehaviour,
    IDependency<InputController>
{
    [SerializeField] private GameObject m_StorePage;
    [SerializeField] private GameObject m_ResourcePage;
    [SerializeField] private GameObject m_ShelterPage;
    [SerializeField] private GameObject m_InfoPage;

    [SerializeField] private GameObject m_StoreButton;
    [SerializeField] private GameObject m_ResourceButton;
    [SerializeField] private GameObject m_ShelterButton;
    [SerializeField] private GameObject m_InfoButton;

    [Space(10)]  
    [SerializeField] private GameObject m_Notepad;

    [Header("ButtonSprites")]
    [SerializeField] private Sprite defaultButtomSprite;
    [SerializeField] private Sprite selectButtonSprite;
    
    private InputController m_InputController;
    private GameObject currentPage;
    private GameObject currentButton;

    private Image storeSprite;
    private Image resourceSprite;
    private Image shelterSprite;
    private Image inforSprite;

    #region Constructs
    public void Construct(InputController m_InputController) => this.m_InputController = m_InputController;
    #endregion

    private void Start()
    {
        FindImage();
    }

    public void OpenStorePage()
    {
        ResetPage();        
        m_StorePage.SetActive(true);
        //m_StoreButton.SetActive(false);
        ChangeButtonsSprite(m_StoreButton);
        currentButton = m_StoreButton;
        currentPage = m_StorePage;
    }
    public void OpenResourcePage()
    {
        ResetPage();
        m_ResourcePage.SetActive(true);
        //m_ResourceButton.SetActive(false);
        ChangeButtonsSprite(m_ResourceButton);
        currentButton = m_ResourceButton;
        currentPage = m_ResourcePage;
    }
    public void OpenShelterPage()
    {
        ResetPage();
        m_ShelterPage.SetActive(true);
        //m_ShelterButton.SetActive(false);
        ChangeButtonsSprite(m_ShelterButton);
        currentButton = m_ShelterButton;
        currentPage = m_ShelterPage;
    }
    public void OpenInfoPage()
    {
        ResetPage();
        m_InfoPage.SetActive(true);
        //m_InfoButton.SetActive(false);
        ChangeButtonsSprite(m_InfoButton);
        currentButton = m_InfoButton;
        currentPage = m_InfoPage;
    }

    private void ResetPage()
    {
        if (currentPage != null)
        {
            currentPage.SetActive(false);
        }
        if (currentButton != null)
        {
            currentButton.SetActive(true);
        }
    }

    public void OpenNotepad()
    {
        m_InputController.enabled = false;
        m_Notepad.SetActive(true);
        OpenStorePage();
    }

    public void CloseNotepad()
    {
        m_InputController.enabled = true;
        m_Notepad.SetActive(false);
    }

    private void FindImage()
    {
        storeSprite = m_StoreButton.GetComponentInChildren<Image>();
        resourceSprite = m_ResourceButton.GetComponentInChildren<Image>();
        shelterSprite = m_ShelterButton.GetComponentInChildren<Image>();
        inforSprite = m_InfoButton.GetComponentInChildren<Image>();
    }

    private void ChangeButtonsSprite(GameObject selectButton)
    {
        storeSprite.sprite = defaultButtomSprite;
        resourceSprite.sprite = defaultButtomSprite;
        shelterSprite.sprite = defaultButtomSprite;
        inforSprite.sprite = defaultButtomSprite;

        Image selectButtonImage = selectButton.GetComponentInChildren<Image>();
        selectButtonImage.sprite = selectButtonSprite;
    }
}
