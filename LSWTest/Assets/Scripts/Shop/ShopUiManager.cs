using System.Collections.Generic;
using UnityEngine;

public class ShopUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _visualObject;

    [SerializeField] private List<UiClothingPiece> _uiClothings;

    public void Open()
    {
        _visualObject.SetActive(true);
    }

    public void Close()
    {
        _visualObject.SetActive(false);
    }

    public void UpdateUi(List<ClothingPiece> inventoryItems)
    {
        for (int i = 0; i < _uiClothings.Count; i++)
        {
            if (i < inventoryItems.Count)
            {
                _uiClothings[i].gameObject.SetActive(true);
                _uiClothings[i].ChangeClothingPiece(inventoryItems[i]);
            }
            else
            {
                _uiClothings[i].gameObject.SetActive(false);
            }
        }
    }
}
