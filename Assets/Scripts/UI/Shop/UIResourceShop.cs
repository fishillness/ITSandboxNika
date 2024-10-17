using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceShop : MonoBehaviour,
    IDependency<ValueManager>
{
    [SerializeField] private GameObject resourceShopPanel;
    [SerializeField] private Image currentResourceImage;
    [SerializeField] private TextMeshProUGUI currentRecourceText;
    [SerializeField] private Image buyImage;
    [SerializeField] private TextMeshProUGUI buyNumberRecourceText;
    [SerializeField] private TextMeshProUGUI costRecourceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button closeButton;

    private ValueManager valueManager;
    private ValueType valueType;
    private int resourceNumber;

    #region Constructs
    public void Construct(ValueManager resourceManager) => this.valueManager = resourceManager;
    #endregion

    private void Start()
    {
        buyButton.onClick.AddListener(BuyButton);
    }

    private void OnDestroy()
    {
        buyButton.onClick.RemoveListener(BuyButton);
    }

    public void OpenResourceShopPanel(ResourceShopInfo resourceShopInfo)
    {
        currentResourceImage.sprite = resourceShopInfo.ResourceImage;
        buyImage.sprite = resourceShopInfo.ResourcePackImage;
        buyNumberRecourceText.text = resourceShopInfo.ResourceNumber.ToString();
        costRecourceText.text = $"{resourceShopInfo.ResourceCost} Ð";
        valueType = resourceShopInfo.ValueType;
        resourceNumber = resourceShopInfo.ResourceNumber;

        currentRecourceText.text = valueManager.GetValueByType(valueType).ToString();

        resourceShopPanel.SetActive(true);
    }

    public void BuyButton()
    {
        valueManager.AddValueByType(valueType, resourceNumber);
        currentRecourceText.text = valueManager.GetValueByType(valueType).ToString();
    }

    public void CloseResourceShopPanel()
    {
        resourceShopPanel.SetActive(false);
    }
}
