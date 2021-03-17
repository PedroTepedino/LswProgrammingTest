using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Dialog", fileName = "NewDialog", order = 0)]
[Serializable]
public class Dialog : ScriptableObject
{
    [SerializeField] private DialogBit[] _dialogBits;
    public DialogBit[] DialogBits => _dialogBits;
}
