using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ClickSound);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ClickSound);
    }

    private void ClickSound()
    {
        AudioManager.Instance.Play("Click");
    }

    private void OnValidate()
    {
        if (_button == null)
        {
            _button = this.GetComponent<Button>();
        }
    }
}
