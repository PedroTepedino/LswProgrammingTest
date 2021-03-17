using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;


public class PlayerClothesManager : MonoBehaviour
{
    [SerializeField] private List<ClothingPiece> _clothingPieces;

    private ClothingPiece _currentOutfit;

    public event Action<ClothingPiece> OnCurrentOutfitChanged;
    public static event Action<List<ClothingPiece>> OnInventoryChanged;

    public List<ClothingPiece> ClothingPieces => _clothingPieces;

    private IShop _shopInRange = null;

    private void Awake()
    {
        if (_clothingPieces == null)
            _clothingPieces = new List<ClothingPiece>();
    }

    // I need this to execute once the object is created after every other object.
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        OnInventoryChanged?.Invoke(_clothingPieces);
    }

    private void OnEnable()
    {
        if (_clothingPieces.Count > 0)
        {
            ChangeClothes(_clothingPieces[0]);
        }

        UiInventory.OnOutfitSelected += ChangeClothes;
        InputManager.OnInteract += OnInteraction;
    }

    private void OnDisable()
    {
        UiInventory.OnOutfitSelected -= ChangeClothes;
        InputManager.OnInteract -= OnInteraction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _shopInRange = collision.gameObject.GetComponent<IShop>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IShop>() == _shopInRange)
        {
            _shopInRange = null;
        }
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

    private void OnInteraction()
    {
        if (_shopInRange != null)
        {
            if (Time.timeScale > 0.1f)
            {
                _shopInRange.Open(this);
                return;
            }
            
            if (_shopInRange.IsOpen)
            {
                _shopInRange.Close();
            }
        }
    }
}
