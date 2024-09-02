using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public event UnityAction Click;
    [SerializeField] private Camera m_Camera;

    private Vector3 worldPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
}
