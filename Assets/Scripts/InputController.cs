using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector3 mousePosition;
    private bool mouseButton;
    private bool mouseButtonDown;

    public Vector3 MousePosition => mousePosition;
    public bool MouseButton => mouseButton;
    public bool MouseButtonDown => mouseButtonDown; 

    private void Update()
    {
        GetMouseInput();
    }

    private void GetMouseInput()
    {
        mousePosition = Input.mousePosition;
        mouseButtonDown = Input.GetMouseButtonDown(0);
        mouseButton = Input.GetMouseButton(0);
    }
}
