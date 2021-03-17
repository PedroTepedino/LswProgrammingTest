using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UiDialog : MonoBehaviour
{
    private static UiDialog _instance;
    public static UiDialog Instance => _instance;

    [SerializeField] private Image _speakerIcon;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private GameObject _mainVisualObject;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        _mainVisualObject.SetActive(false);
    }

    public void ShowSingleLine(DialogBit bit)
    {
        _mainVisualObject.SetActive(true);
        ChangeDialog(bit);
    }

    public void HideDialog()
    {
        _mainVisualObject.SetActive(false);
    }

    public void ShowDialog(Dialog dialog)
    {
        StartCoroutine(DialogInteraction(dialog));
    }

    private IEnumerator DialogInteraction(Dialog dialog)
    {
        Time.timeScale = 0f;
        _mainVisualObject.SetActive(true);

        for (int i = 0; i < dialog.DialogBits.Length; i++)
        {
            yield return new WaitForEndOfFrame();

            ChangeDialog(dialog.DialogBits[i]);

            yield return new WaitUntil(() => InputManager.Anykey);
        }

        _mainVisualObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ChangeDialog(DialogBit bit)
    {
        _speakerIcon.sprite = bit.Icon;
        _dialogText.text = bit.Dialog;
    }
}