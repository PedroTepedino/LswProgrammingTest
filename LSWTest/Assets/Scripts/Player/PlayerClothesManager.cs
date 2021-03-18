using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class PlayerClothesManager : MonoBehaviour
{
    [SerializeField] private GameObject _interactionPopup = null;
    [SerializeField] private List<ClothingPiece> _clothingPieces;

    private ClothingPiece _currentOutfit;

    public event Action<ClothingPiece> OnCurrentOutfitChanged;
    public static event Action<List<ClothingPiece>> OnInventoryChanged;

    public List<ClothingPiece> ClothingPieces => _clothingPieces;
    public ClothingPiece CurrentOutfit => _currentOutfit;

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
        InputManager.OnInteract += ListenOnInteraction;
        InputManager.OnEscape += ListenOnEscape;
    }

    private void OnDisable()
    {
        UiInventory.OnOutfitSelected -= ChangeClothes;
        InputManager.OnInteract -= ListenOnInteraction;
        InputManager.OnEscape -= ListenOnEscape;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _shopInRange = collision.gameObject.GetComponent<IShop>();
        
        if (_shopInRange != null)
        {
            TurnPopuopOn(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IShop>() == _shopInRange)
        {
            _shopInRange = null;

            TurnPopuopOn(false);
        }
    }

    public void ChangeClothes(ClothingPiece clothing)
    {
        _currentOutfit = clothing;
        OnCurrentOutfitChanged?.Invoke(_currentOutfit);
    }

    public void ChangeToValidOutfitIfNecessary()
    {
        if (!_clothingPieces.Contains(_currentOutfit))
        {
            ChangeClothes(_clothingPieces[0]);
        }
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

    private void ListenOnInteraction()
    {
        if (_shopInRange != null)
        {
            AudioManager.Instance.Play("Button");

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

    private void ListenOnEscape()
    {
        if (_shopInRange != null && _shopInRange.IsOpen)
        {
            _shopInRange.Close();
        }
    }

    private void TurnPopuopOn(bool turnOn)
    {
        if (_interactionPopup != null)
        {
            _interactionPopup.SetActive(turnOn);
        }
    }
}
