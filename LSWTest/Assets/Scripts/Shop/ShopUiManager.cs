using System.Collections;
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

    private void UpdateUi()
    {

    }
}
