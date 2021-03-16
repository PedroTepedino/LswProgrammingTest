using UnityEngine;
using System;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject _interactionButtonUi;
    private bool _playerInRange = false;

    [SerializeField] private ShopUiManager _ui;

    private Player _player = null;

    private void OnEnable()
    {
        InputManager.OnInteract += ListenOnInteraction;
    }

    private void OnDisable()
    {
        InputManager.OnInteract -= ListenOnInteraction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.gameObject.GetComponent<Player>();

        if (_player)
        {
            _interactionButtonUi.SetActive(true);
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _player.gameObject)
        {
            _interactionButtonUi.SetActive(false);
            _playerInRange = false;
        }
    }

    public void ListenOnInteraction()
    {
        if (_playerInRange)
        {
            _ui.Open();
        }
    }

    private void OnValidate()
    {
        if (!this.GetComponent<Collider2D>())
        {
            var collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
        }
    }
}
