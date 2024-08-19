using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private LayerMask m_LayerMask;

    private Vector3 lastPosition;

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = m_Camera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, m_LayerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
