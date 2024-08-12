using UnityEngine;

public class InpuController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 startPosition;
    [SerializeField]
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            startPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        else if (Input.GetMouseButton(0))
        {
            float pos = cam.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x;
            transform.position = new Vector3(transform.position.x - pos, transform.position.y, transform.position.z);
        }
    }
}
