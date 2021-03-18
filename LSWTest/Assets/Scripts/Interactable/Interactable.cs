using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog _dialog;

    public void Interact()
    {
        AudioManager.Instance.Play("Thunder");
        UiDialog.Instance.ShowDialog(_dialog);
    }

    private void OnValidate()
    {
        if (!this.gameObject.CompareTag("Interactable"))
        {
            this.gameObject.tag = "Interactable";
        }
    }
}