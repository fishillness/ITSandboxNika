using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum InputControllerModes
{
    NotepadMode,
    ConstructionMode,
    CityMode, 
    DialogMode
}

public class InputController : MonoBehaviour
{  
    public event UnityAction ClickEventInConstructionMode;
    public event UnityAction ClickEventInDialogMode;
    public event UnityAction<InputControllerModes> OnInputControllerModeChanges;

    public Vector3 MousePosition => mousePosition;
    public bool MouseButton => mouseButton;
    public bool MouseButtonDown => mouseButtonDown;
    public bool MouseButtonUp => mouseButtonUp;
    public Vector3 Scroll => scroll;
    public Touch TouchOne => touchOne;
    public Touch TouchZero => touchZero;
    public bool IsTouchCountEquals2 => Input.touchCount == 2;
    public InputControllerModes InputControllerMode => inputControllerMode;
    
    private InputControllerModes inputControllerMode;
    private Vector3 mousePosition;
    private bool mouseButton;
    private bool mouseButtonDown;
    private bool mouseButtonUp;
    private Vector3 scroll;
    private Touch touchOne;
    private Touch touchZero;

    private void Update()
    {
        if (inputControllerMode == InputControllerModes.ConstructionMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (CheckingTheUITouch() == false)
                {
                    ClickEventInConstructionMode?.Invoke();
                }                
            }
        }

        if (inputControllerMode == InputControllerModes.DialogMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickEventInDialogMode?.Invoke();
            }
        }
        
        if (inputControllerMode != InputControllerModes.NotepadMode && inputControllerMode != InputControllerModes.DialogMode)
        {  
            GetMouseInput();

            if (Input.touchCount == 2)
            {
                GetTouchInput();
            }
        }  
    }

    private bool CheckingTheUITouch()
    {
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                return true;
            }
        }
        if (EventSystem.current.IsPointerOverGameObject(-1)) return true;

        return false;
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

    public void SetInputControllerMode(InputControllerModes inputControllerMode)
    {
        this.inputControllerMode = inputControllerMode;
        OnInputControllerModeChanges?.Invoke(this.inputControllerMode);
    }
}
