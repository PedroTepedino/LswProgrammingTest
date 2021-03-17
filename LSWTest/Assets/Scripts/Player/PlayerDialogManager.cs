using UnityEngine;

public class PlayerDialogManager : MonoBehaviour
{
    [SerializeField] private GameObject _commandPopup;

    private Speaker _speakerTarget;
    
    private void OnEnable()
    {
        InputManager.OnTalk += ListenOnTalk;    
    }

    private void OnDisable()
    {
        InputManager.OnTalk -= ListenOnTalk;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _speakerTarget = collision.GetComponent<Speaker>();
        if (_speakerTarget != null)
        {
            _commandPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _speakerTarget.gameObject)
        {
            _speakerTarget = null;
            _commandPopup.SetActive(false);
        }
    }

    private void ListenOnTalk()
    {
        if (_speakerTarget != null)
        {
            _speakerTarget.Talk();
        }
    }
}
