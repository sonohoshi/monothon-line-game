using System;
using DG.Tweening;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform target;
    private bool _endOpening;

    private void Start()
    {
        transform.DOMoveY(4, 3f).SetEase(Ease.InCirc).OnComplete(() => _endOpening = true);
    }

    private void LateUpdate()
    {

        if (_endOpening)
        {
            var pos = transform.position;
            if (target.position.y >= 4)
            {
                pos.y = target.position.y;
                transform.position = pos;
            }
        }
    }
}