using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class UiDialog : MonoBehaviour
{
    private static UiDialog _instance;
    public static UiDialog Instance => _instance;

    [SerializeField] private Image _speakerIcon;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private GameObject _mainVisualObject;

    private PlayerClothesManager _playerInstance;

    private Coroutine _ongoingDialog;

    public static bool OnGoingDialog { get; private set; }

    private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

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

        _playerInstance = FindObjectOfType<PlayerClothesManager>();
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
        if (_ongoingDialog != null) return;

        _ongoingDialog = StartCoroutine(DialogInteraction(dialog));
    }

    private IEnumerator DialogInteraction(Dialog dialog)
    {
        OnGoingDialog = true;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(0.1f);

        _mainVisualObject.SetActive(true);

        for (int i = 0; i < dialog.DialogBits.Length; i++)
        {
            ChangeDialog(dialog.DialogBits[i]);

            yield return new WaitUntil(() => InputManager.Anykey);
            AudioManager.Instance.Play("Click");

            yield return new WaitForSecondsRealtime(0.1f);
        }

        _mainVisualObject.SetActive(false);
        Time.timeScale = 1f;
        _ongoingDialog = null;
        OnGoingDialog = false;
    }

    private void ChangeDialog(DialogBit bit)
    {
        if (bit.PlayerBit)
        {
            _speakerIcon.sprite = _playerInstance.CurrentOutfit.Icon;
        }
        else
        {
            _speakerIcon.sprite = bit.Icon;
        }

        _dialogText.text = bit.Dialog;
    }
}