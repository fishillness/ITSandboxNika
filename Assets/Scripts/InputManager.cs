using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public event UnityAction Click;
    [SerializeField] private Camera m_Camera;

    private Vector3 worldPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    return;
                }
            }
            if (EventSystem.current.IsPointerOverGameObject(-1)) return;
            Click?.Invoke();
        }   
    }

    public Vector3 GetMousePosition(Transform targetPlane)
    {
        Plane plane = new Plane(targetPlane.up, targetPlane.position);
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float position))
        {
            worldPosition = ray.GetPoint(position);
        }
        return worldPosition;
    }

    public Vector3 GetCameraPosition(Transform targetPlane)
    {
        Plane plane = new Plane(targetPlane.up, targetPlane.position);
        Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        if (plane.Raycast(ray, out float position))
        {
            worldPosition = ray.GetPoint(position);
        }
        return worldPosition;
    }
}
