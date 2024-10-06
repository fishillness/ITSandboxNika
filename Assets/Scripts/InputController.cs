using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector3 mousePosition;
    private bool mouseButton;
    private bool mouseButtonDown;
    private bool mouseButtonUp;
    private Vector3 scroll;
    private Touch touchOne;
    private Touch touchZero;

    public Vector3 MousePosition => mousePosition;
    public bool MouseButton => mouseButton;
    public bool MouseButtonDown => mouseButtonDown;
    public bool MouseButtonUp => mouseButtonUp;
    public Vector3 Scroll => scroll;
    public Touch TouchOne => touchOne;
    public Touch TouchZero => touchZero;

    private void Update()
    {
        GetMouseInput();
        if (Input.touchCount == 2)
        {
            GetTouchInput();
        }
    }

    private void GetMouseInput()
    {
        mousePosition = Input.mousePosition;
        mouseButtonDown = Input.GetMouseButtonDown(0);
        mouseButton = Input.GetMouseButton(0);
        mouseButtonUp = Input.GetMouseButtonUp(0);
        scroll = Input.mouseScrollDelta;
    }
    
    private void GetTouchInput()
    {
        touchOne = Input.GetTouch(1);
        touchZero = Input.GetTouch(0);
    }
}
