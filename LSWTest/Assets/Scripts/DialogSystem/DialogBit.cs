using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct DialogBit
{
    public Sprite Icon;
    [TextArea(3, 10)]
    public string Dialog;
}
