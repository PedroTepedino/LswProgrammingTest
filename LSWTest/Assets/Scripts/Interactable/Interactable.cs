using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog _dialog;

    public void Interact()
    {
        UiDialog.Instance.ShowDialog(_dialog);
    }
}