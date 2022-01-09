using System;
using UnityEngine;
using UnityEngine.UI;

public class ClickClosingObject : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => gameObject.SetActive(false));
    }
}