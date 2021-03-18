using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject _popUp;

    private IInteractable _interactionTarget;

    private void OnEnable()
    {
        InputManager.OnInteract += ListenOnInteract;
    }

    private void OnDisable()
    {
        InputManager.OnInteract -= ListenOnInteract;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _interactionTarget = collision.GetComponent<IInteractable>();
        if (_interactionTarget != null)
        {
            _popUp.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactionTarget != null)
        {
            if (collision.gameObject.GetComponent<IInteractable>() == _interactionTarget)
            {
                _interactionTarget = null;
                _popUp.SetActive(false);
            }
        }
    }

    private void ListenOnInteract()
    {
        if (_interactionTarget != null)
        {
            _interactionTarget.Interact();
        }
    }
}
