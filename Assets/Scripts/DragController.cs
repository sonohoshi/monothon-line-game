using System;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private Vector2 _startPosition;
    private Camera _mainCam;
    
    [SerializeField] private LineRenderer lineRenderer;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    public bool GetInput(out float distance)
    {
        distance = 0;
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new[] {(Vector3) _startPosition, (Vector3) _startPosition});
            transform.rotation = Quaternion.Euler(Vector3.zero);
            return false;
        }
        if (Input.GetMouseButton(0))
        {
            lineRenderer.SetPosition(1, (Vector2) _mainCam.ScreenToWorldPoint(Input.mousePosition));
            return false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            var temp = _startPosition - (Vector2) _mainCam.ScreenToWorldPoint(Input.mousePosition);
            distance = temp.x > 0 ? -temp.magnitude : temp.magnitude;
            _startPosition = Vector3.zero;
            lineRenderer.positionCount = 0;
            return true;
        }

        return false;
    } 
}