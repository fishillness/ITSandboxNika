using UnityEngine;

public class temporaty_linkOnStore : MonoBehaviour
{
    private static temporaty_linkOnStore instance;
    public static temporaty_linkOnStore Instance => instance;

    [SerializeField] private Store store;

    public Store Store => store;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


}
