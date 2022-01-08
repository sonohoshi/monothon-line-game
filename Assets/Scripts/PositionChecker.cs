using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    string CurrentTile = "";
    const float DO_NOTHING_LENGTH = 5f;
    const float MINIMUM_LENGTH = 15f;
    const float MAXIMUM_LENGTH = 30f;
    const float LENGTH = 15f;
    static readonly int IgnoreRaycastLayerMask = ~(1 << 2);

    Vector2 MousePosition;
    private Camera _camera;
    float Angle;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        BoxCollider2D t = gameObject.GetComponent<BoxCollider2D>();
        Debug.DrawRay(transform.position, (-transform.up + transform.right) * 0.708f, Color.green);
        Debug.DrawRay(transform.position, (-transform.up - transform.right) * 0.708f, Color.green);
        Debug.DrawRay(transform.position, (-transform.up) * 0.5f, Color.green);
        SetAngle(17);
        Debug.Log(Angle);
    }

    private void SetAngle(float length)
    {
        int masking = 0b000001;

        RaycastHit2D left = Physics2D.Raycast(transform.position, (-transform.up - transform.right), 0.8f, IgnoreRaycastLayerMask);
        RaycastHit2D middle = Physics2D.Raycast(transform.position, -transform.up, 0.5f, IgnoreRaycastLayerMask);
        RaycastHit2D right = Physics2D.Raycast(transform.position, (-transform.up + transform.right), 0.8f, IgnoreRaycastLayerMask);
        if (left.collider != null)
            masking = masking << 1;
        if (middle.collider != null)
            masking = masking << 1;
        if (right.collider != null)
            masking = masking << 1;

        Debug.Log(1);
        Debug.Log(masking);

        if (masking < 0b000100)
            return;
        else
            Angle = CalculateAngle(left, middle, right, length);
    }

    private float CalculateAngle(RaycastHit2D left, RaycastHit2D middle, RaycastHit2D right, float length)
    {
        if (Direction() == 0)
            return (GetBaseAngle(left, middle, right) - (length - MINIMUM_LENGTH) / LENGTH * 60);
        else if (Direction() == 1)
            return (GetBaseAngle(left, middle, right) + (length - MINIMUM_LENGTH) / LENGTH * 60);
        else
            return -1;
    }

    private static float GetBaseAngle(RaycastHit2D left, RaycastHit2D middle, RaycastHit2D right)
    {
        Debug.Log($"left.point : {left.point}, left : {left.collider}");
        Debug.Log($"right.point : {right.point}, left : {right.collider}");
        if (left.point.y > right.point.y)
        {
            return 135.0f;
        }
        else if (left.point.y < right.point.y)
        {
            return 45.0f;
        }
        else
        {
            return 0;
        }
    }

    private int Direction()
    {
        Vector3 tmp = transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = _camera.ScreenToWorldPoint(MousePosition);
        }
        if (MousePosition != null)
        {
            if (transform.position.x - MousePosition.x > 0)
                //left = 0
                return 0;
            else if (transform.position.x - MousePosition.x < 0)
                //right = 1
                return 1;
            else
                //error
                return -1;
        }
        //error
        return -1;
    }
}
