using UnityEngine;

public class Speaker : MonoBehaviour
{
    [SerializeField] private Dialog[] _dialog;

    private int index = 0;

    private void Awake()
    {
        index = 0;
    }

    public void Talk()
    {
        if (_dialog.Length == 0) return;

        UiDialog.Instance.ShowDialog(_dialog[index]);
        index++;

        if (index >= _dialog.Length)
            index = 0;
    }
}
