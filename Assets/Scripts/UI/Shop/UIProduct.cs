using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProduct : MonoBehaviour,
    IDependency<ValueManager>
{
    [SerializeField] private ResourceShopInfo resourceShopInfo;
    [SerializeField] private Image product;
    [SerializeField] private TextMeshProUGUI number;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Button buyButton;

    private ValueManager valueManager;

    #region Constructs
    public void Construct(ValueManager resourceManager) => this.valueManager = resourceManager;
    #endregion

    private void Start()
    {
        SetProperties();
        buyButton.onClick.AddListener(Buy);
    }

    private void OnDestroy()
    {
        buyButton.onClick.RemoveListener(Buy);
    }

    private void SetProperties()
    {
        product.sprite = resourceShopInfo.ResourcePackImage;
        number.text = resourceShopInfo.ResourceNumber.ToString();
        cost.text = $"{resourceShopInfo.ResourceCost} Ð";
    }

    private void Buy()
    {
        valueManager.AddValueByType(resourceShopInfo.ValueType, resourceShopInfo.ResourceNumber);
    }
}
