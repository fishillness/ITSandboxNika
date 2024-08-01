using UnityEngine;
using UnityEngine.Events;

public class SaveMoney : MonoBehaviour
{
    private int countCoin;
    public static UnityEvent<int> SendCoinText = new UnityEvent<int>();
    // Start is called before the first frame update
    void Start()
    {
        countCoin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Получение количества монет для добавления/отнимания
        if(Input.GetMouseButtonDown(0))
            AddCoin(1);

        if(Input.GetMouseButtonDown(1))
            DeleteCoin(1);
    }

    void AddCoin(int coins)
    {
        countCoin += coins;
        SendCoinText.Invoke(countCoin);
    }

    void DeleteCoin(int coins)
    {
        countCoin -= coins;
        SendCoinText.Invoke(countCoin);
    }
}
