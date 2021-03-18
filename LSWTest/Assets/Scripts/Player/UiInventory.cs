using System.Collections.Generic;
using UnityEngine;
using System;

public class UiInventory : MonoBehaviour
{
    [SerializeField] private List<UiClothingPiece> _uiClothingPieces;

    [SerializeField] private GameObject _uiVisualObject;
    [SerializeField] private GameObject _menuObject;

    public static event Action<ClothingPiece> OnOutfitSelected;

    private void Awake()
    {
        PlayerClothesManager.OnInventoryChanged += UpdateUi;
    }

    private void OnDestroy()
    {
        PlayerClothesManager.OnInventoryChanged -= UpdateUi;
    }

    private void OnEnable()
    {
        foreach (var ui in _uiClothingPieces)
        {
            ui.OnSelected += ListenOnClothingSelected;
        }

        InputManager.OnInventoryAction += OpenInventory;
        InputManager.OnEscape += ListenOnEscape;
    }

    private void OnDisable()
    {
        foreach (var ui in _uiClothingPieces)
        {
            ui.OnSelected -= ListenOnClothingSelected;
        }

        InputManager.OnInventoryAction -= OpenInventory;
        InputManager.OnEscape -= ListenOnEscape;
    }

    private void ListenOnClothingSelected(ClothingPiece clothingPiece)
    {
        OnOutfitSelected?.Invoke(clothingPiece);
    }

    public void OpenInventory()
    {
        if (_uiVisualObject.activeInHierarchy)
        {
            AudioManager.Instance.Play("Button");
            _uiVisualObject.SetActive(false);
            _menuObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else if (Time.timeScale > 0.1f)
        {
            AudioManager.Instance.Play("Button");
            _uiVisualObject.SetActive(true);
            _menuObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void ListenOnEscape()
    {
        if (_uiVisualObject.activeInHierarchy)
        {
            AudioManager.Instance.Play("Button");
            _uiVisualObject.SetActive(false);
            _menuObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else if (Time.timeScale > 0.1f)
        {
            AudioManager.Instance.Play("Button");
            _uiVisualObject.SetActive(true);
            _menuObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void UpdateUi(List<ClothingPiece> inventoryItems)
    {
        for(int i = 0; i < _uiClothingPieces.Count ; i++)
        {
            if (i < inventoryItems.Count)
            {
                _uiClothingPieces[i].gameObject.SetActive(true);
                _uiClothingPieces[i].ChangeClothingPiece(inventoryItems[i]);
            }
            else
            {
                _uiClothingPieces[i].gameObject.SetActive(false);
            }
        }
    }
}
