using UnityEngine;

[CreateAssetMenu(menuName = "Clothes", fileName = "NewClothingPiece", order = 0)]
public class ClothingPiece : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private RuntimeAnimatorController _clothes;

    public Sprite Icon => _icon;

    public RuntimeAnimatorController Clothes => _clothes;
}
