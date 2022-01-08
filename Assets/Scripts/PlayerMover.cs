using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private DragController controller;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (controller.GetInput(out var distance))
        {
            Debug.Log($"distance : {distance}");
            _rigidbody.AddForce(Vector2.up * distance, ForceMode2D.Impulse);
        }
    }
}