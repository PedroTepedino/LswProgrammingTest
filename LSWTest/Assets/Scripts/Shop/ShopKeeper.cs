using UnityEngine;
using System.Collections.Generic;
using System;

public class ShopKeeper : MonoBehaviour, IShop
{
    [SerializeField] private GameObject _uiVisualObject;
    [SerializeField] private ShopUiManager _buyUi;
    [SerializeField] private ShopUiManager _sellUi;
    [SerializeField] private List<ClothingPiece> _availableClothes;

    private PlayerClothesManager _player;

    public List<ClothingPiece> AvailableClothes => _availableClothes;

    public bool IsOpen => _uiVisualObject.activeInHierarchy;

    private void OnEnable()
    {
        _buyUi.OnOutfitSelected += Buy;
        _sellUi.OnOutfitSelected += Sell;
    }

    private void OnDisable()
    {
        _buyUi.OnOutfitSelected -= Buy;
        _sellUi.OnOutfitSelected -= Sell;

    }

    public void Open(PlayerClothesManager player)
    {
        _player = player;
        _sellUi.UpdateUi(_player.ClothingPieces);
        _buyUi.UpdateUi(_availableClothes);
        _sellUi.Open();
        _buyUi.Open();
        _uiVisualObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Close()
    {
        _sellUi.Close();
        _buyUi.Close();
        _uiVisualObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Buy(ClothingPiece Item)
    {
        _availableClothes.Remove(Item);
        _player.AddItem(Item);
        _buyUi.UpdateUi(_availableClothes);
        _sellUi.UpdateUi(_player.ClothingPieces);
    }

    public void Sell(ClothingPiece Item)
    {
        _availableClothes.Add(Item);
        _player.RemoveItem(Item);
        _buyUi.UpdateUi(_availableClothes);
        _sellUi.UpdateUi(_player.ClothingPieces);
    }
}
