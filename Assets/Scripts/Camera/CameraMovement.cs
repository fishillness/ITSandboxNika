using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Vector2 xBounds;
    [SerializeField] private Vector2 yBounds;
    [SerializeField] private float speed;
    [SerializeField] private float zoomSpeed, minZoom, maxZoom, smoothTime;
    [SerializeField] private float startZoom;

    private Camera cam;
    private Vector2 startPosition;
    private float xTargetPos;
    private float yTargetPos;
    private float zoomTarget;
    private float velocity;
    private Touch touchOne;
    private Touch touchZero;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = startZoom;
        zoomTarget = cam.orthographicSize;
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

        if (Input.touchCount == 2)
        {
            touchZero = inputController.TouchZero;
            touchOne = inputController.TouchOne;

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

            float distTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
            float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistTouch - distTouch;

            ScrollTouch(difference * 0.01f);
        }
    }

    private void Scroll()
    {
        if (cam.orthographic)
        {
            zoomTarget -= inputController.Scroll.y * zoomSpeed;
            zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoomTarget, ref velocity, smoothTime);
        }
        else
        {
            
        }
    }

    private void ScrollTouch(float increment)
    {
        zoomTarget = Mathf.Clamp(zoomTarget - increment, minZoom, maxZoom);
    }
}
