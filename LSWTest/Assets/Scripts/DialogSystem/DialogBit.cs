using UnityEngine;
using System;

[Serializable]
public struct DialogBit
{
    public bool PlayerBit;
    public Sprite Icon;
    [TextArea(3, 10)]
    public string Dialog;
}
