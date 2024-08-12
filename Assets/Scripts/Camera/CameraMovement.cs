using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Vector2 xBounds;
    [SerializeField] private Vector2 yBounds;
    [SerializeField] private float speed;
    [SerializeField] private float scrollSpeed;

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
        Scroll();
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

    private void Scroll()
    {
        if(cam.orthographic)
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        else
            cam.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    }
}
