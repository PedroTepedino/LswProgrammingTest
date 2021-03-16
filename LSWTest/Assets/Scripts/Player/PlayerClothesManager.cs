using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerClothesManager : MonoBehaviour
{
    [SerializeField] private List<ClothingPiece> _clothingPieces;

    private ClothingPiece _currentOutfit;

    public event Action<ClothingPiece> OnCurrentOutfitChanged;
    public static event Action<List<ClothingPiece>> OnInventoryChanged;

    private void Awake()
    {
        if (_clothingPieces == null)
            _clothingPieces = new List<ClothingPiece>();

        OnInventoryChanged?.Invoke(_clothingPieces);
    }

    private void OnEnable()
    {
        if (_clothingPieces.Count > 0)
        {
            ChangeClothes(_clothingPieces[0]);
        }

        UiInventory.OnOutfitSelected += ChangeClothes;
    }

    private void OnDisable()
    {
        UiInventory.OnOutfitSelected += ChangeClothes;
    }

    public void ChangeClothes(ClothingPiece clothing)
    {
        _currentOutfit = clothing;
        OnCurrentOutfitChanged?.Invoke(_currentOutfit);
    }

    public void AddItem(ClothingPiece item)
    {
        _clothingPieces.Add(item);
        OnInventoryChanged?.Invoke(_clothingPieces);
    }

    public void RemoveItem(ClothingPiece item)
    {
        _clothingPieces.Remove(item);
        OnInventoryChanged?.Invoke(_clothingPieces);
    }
}
