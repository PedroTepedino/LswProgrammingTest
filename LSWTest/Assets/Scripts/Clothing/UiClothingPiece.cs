using UnityEngine;
using UnityEngine.UI;
using System;

public class UiClothingPiece : MonoBehaviour
{
    [SerializeField] private ClothingPiece _clothingPiece;

    [SerializeField] private Image _icon;

    public event Action<ClothingPiece> OnSelected;

    public ClothingPiece ClothingPiece => _clothingPiece;

    private void OnEnable()
    {
        _icon.sprite = _clothingPiece.Icon;
    }

    public void Selected()
    {
        OnSelected?.Invoke(_clothingPiece);
    }

    public void ChangeClothingPiece(ClothingPiece newClothingPiece)
    {
        _clothingPiece = newClothingPiece;
        _icon.sprite = _clothingPiece.Icon;
    }

    private void OnValidate()
    {
        if (_icon == null)
        {
            _icon = this.GetComponentInChildren<Image>();
        }
    }
}
