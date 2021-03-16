using System.Collections.Generic;
using UnityEngine;
using System;

public class UiInventory : MonoBehaviour
{
    [SerializeField] private List<UiClothingPiece> _uiClothingPieces;

    public static event Action<ClothingPiece> OnOutfitSelected;

    private void OnEnable()
    {
        foreach(var ui in _uiClothingPieces)
        {
            ui.OnSelected += ListenOnClothingSelected;
        }
    }

    private void OnDisable()
    {
        foreach (var ui in _uiClothingPieces)
        {
            ui.OnSelected -= ListenOnClothingSelected;
        }
    }

    public void OpenInventory()
    {
       
    }

    private void ListenOnClothingSelected(ClothingPiece clothingPiece)
    {
        OnOutfitSelected?.Invoke(clothingPiece);
    }

    private void OnValidate()
    {
        _uiClothingPieces = new List<UiClothingPiece>(this.GetComponentsInChildren<UiClothingPiece>());
    }
}
