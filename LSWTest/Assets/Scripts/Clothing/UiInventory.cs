using System.Collections.Generic;
using UnityEngine;
using System;

public class UiInventory : MonoBehaviour
{
    [SerializeField] private List<UiClothingPiece> _uiClothingPieces;

    [SerializeField] private GameObject _uiVisualObject;

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
    }

    private void OnDisable()
    {
        foreach (var ui in _uiClothingPieces)
        {
            ui.OnSelected -= ListenOnClothingSelected;
        }

        InputManager.OnInventoryAction -= OpenInventory;
    }

    public void OpenInventory()
    {
        _uiVisualObject.SetActive(!_uiVisualObject.activeInHierarchy);
    }

    private void ListenOnClothingSelected(ClothingPiece clothingPiece)
    {
        OnOutfitSelected?.Invoke(clothingPiece);
    }

    private void UpdateUi(List<ClothingPiece> inventoryItems)
    {
        Debug.Log($"UpdateInventory inventory items {inventoryItems.Count} / Ui count {_uiClothingPieces.Count}");
        for(int i = 0; i < _uiClothingPieces.Count ; i++)
        {
            if (i < inventoryItems.Count)
            {
                _uiClothingPieces[i].gameObject.SetActive(true);
                _uiClothingPieces[i].ClothingPiece = inventoryItems[i];
            }
            else
            {
                _uiClothingPieces[i].gameObject.SetActive(false);
            }
        }
    }
}
