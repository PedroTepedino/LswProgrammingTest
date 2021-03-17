using UnityEngine;

public class Speaker : MonoBehaviour
{
    [SerializeField] private Dialog _dialog;

    public void Talk()
    {
        if (_dialog == null) return;

        UiDialog.Instance.ShowDialog(_dialog);
    }
}
