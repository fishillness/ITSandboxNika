using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Vector2 xBounds;
    [SerializeField] private Vector2 yBounds;
    [SerializeField] private float speed;

    private Camera cam;
    private Vector2 startPosition;
    private float xTargetPos;
    private float yTargetPos;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (inputController.MouseButtonDown)
            startPosition = cam.ScreenToWorldPoint(inputController.MousePosition);
        else if (inputController.MouseButton)
        {
            float xPos = cam.ScreenToWorldPoint(inputController.MousePosition).x - startPosition.x;
            xTargetPos = Mathf.Clamp(transform.position.x - xPos, xBounds.x, yBounds.y);

            float yPos = cam.ScreenToWorldPoint(inputController.MousePosition).y - startPosition.y;
            yTargetPos = Mathf.Clamp(transform.position.y - yPos, yBounds.x, xBounds.y);
        }
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, xTargetPos, speed * Time.deltaTime),
            Mathf.Lerp(transform.position.y, yTargetPos, speed * Time.deltaTime), transform.position.z);
    }
}
