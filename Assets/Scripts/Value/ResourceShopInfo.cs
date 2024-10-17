using UnityEngine;

[CreateAssetMenu]
public class ResourceShopInfo : ScriptableObject
{
    [SerializeField] private ValueType valueType;
    [SerializeField] private Sprite resourceImage;
    [SerializeField] private Sprite resourcePackImage;
    [SerializeField] private int resourceNumber;
    [SerializeField] private int resourceCost;

    public ValueType ValueType => valueType;
    public Sprite ResourceImage => resourceImage;
    public Sprite ResourcePackImage => resourcePackImage;
    public int ResourceNumber => resourceNumber;
    public int ResourceCost => resourceCost;
}
