using UnityEngine;
using UnityEngine.Events;

public class SaveMoney : MonoBehaviour
{
    private int countCoin;
    public static UnityEvent<int> SendCoinText = new UnityEvent<int>();
    private bool send;
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

        if (Input.GetMouseButtonDown(1))
        {
            send = DeleteCoin(1);
            Debug.Log(send.ToString());
        }
    }

    void AddCoin(int coins)
    {
        countCoin += coins;
        SendCoinText.Invoke(countCoin);
    }

    bool DeleteCoin(int coins)
    {
        if (countCoin - coins >= 0)
        {
            countCoin -= coins;
            SendCoinText.Invoke(countCoin);
            return true;
        }

        return false;
    }
}
