using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TEST_WebGL : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        button.onClick.AddListener(Click);
    }

    private void Click()
    {
        int a = (Random.Range(0, 1000));
        text.text = a.ToString();
    }
}
