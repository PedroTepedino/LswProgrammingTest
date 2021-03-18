using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerInteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject _popUp;
    [SerializeField] private Collider2D _collider2D;

    private IInteractable _interactionTarget;

    private void OnEnable()
    {
        InputManager.OnInteract += ListenOnInteract;
    }

    private void OnDisable()
    {
        InputManager.OnInteract -= ListenOnInteract;
    }

    private void Update()
    {
        Collider2D[] colliders = new Collider2D[50];
        var colliderCount = _collider2D.OverlapCollider((new ContactFilter2D()).NoFilter(), colliders);

        for (int i = 0; i < colliderCount; i++)
        {
            if (colliders[i].gameObject.CompareTag("Interactable"))
            {
                _popUp.SetActive(true);
                _interactionTarget = colliders[i].gameObject.GetComponent<IInteractable>();
                return;
            }
        }

        _popUp.SetActive(false);
        _interactionTarget = null;
    }

    private void ListenOnInteract()
    {
        if (_interactionTarget != null)
        {
            AudioManager.Instance.Play("Button");
            _interactionTarget.Interact();
        }
    }

    private void OnValidate()
    {
        if (_collider2D == null || !_collider2D.isTrigger)
        {
            List<Collider2D> colliders = new List<Collider2D> (this.GetComponents<Collider2D>());
            _collider2D = colliders.Find(col => col.isTrigger);
        }
    }
}
