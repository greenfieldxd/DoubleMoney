using System;
using NaughtyAttributes;
using System.Collections.Generic;
using Source.Scripts.Enums;
using UnityEngine;

public class BoardComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Board Variants")] List<BoardVariant> variantPrefabList;

    [SerializeField, BoxGroup("Debug"), ReadOnly] List<BoardPointComponent> boardPointList = new List<BoardPointComponent>();

    public List<BoardVariant> VariantPrefabList => variantPrefabList;
    public List<BoardPointComponent> BoardPointList => boardPointList;
    
    [Serializable]
    public class BoardVariant
    {
        [field: SerializeField] public BoardType Type { get; private set; }
        [field: SerializeField] public GameObject Variant { get; private set; }
    }
}