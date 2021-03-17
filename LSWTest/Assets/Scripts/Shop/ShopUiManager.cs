using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _visualObject;

    [SerializeField] private List<UiClothingPiece> _uiClothings;

    public event Action<ClothingPiece> OnOutfitSelected;

    private void OnEnable()
    {
        foreach (var ui in _uiClothings)
        {
            ui.OnSelected += ListenOnClothingSelected;
        }
    }

    private void OnDisable()
    {
        foreach (var ui in _uiClothings)
        {
            ui.OnSelected -= ListenOnClothingSelected;
        }
    }

    public void Open()
    {
        _visualObject.SetActive(true);
    }

    public void Close()
    {
        _visualObject.SetActive(false);
    }

    private void ListenOnClothingSelected(ClothingPiece outfit)
    {
        OnOutfitSelected?.Invoke(outfit);
    }

    public void ChangeButtonInteractionState(int index, bool shouldInteract)
    {
        _uiClothings[index].SetButtonInteractible(shouldInteract);
    }

    public void UpdateUi(List<ClothingPiece> inventoryItems)
    {
        for (int i = 0; i < _uiClothings.Count; i++)
        {
            if (i < inventoryItems.Count)
            {
                _uiClothings[i].gameObject.SetActive(true);
                _uiClothings[i].ChangeClothingPiece(inventoryItems[i]);
                _uiClothings[i].SetButtonInteractible(true);
            }
            else
            {
                _uiClothings[i].gameObject.SetActive(false);
            }
        }
    }
}
