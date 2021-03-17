using UnityEngine;
public interface IShop
{
    bool IsOpen { get;}
    void Open(PlayerClothesManager player);
    void Close();
    void Buy(ClothingPiece Item);
    void Sell(ClothingPiece Item);
}