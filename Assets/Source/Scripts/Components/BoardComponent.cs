using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class BoardComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Prefab")] List<GameObject> variantPrefabList;

    [SerializeField, BoxGroup("Debug"), ReadOnly] List<BoardPointComponent> boardPointList = new List<BoardPointComponent>();

    public List<GameObject> VariantPrefabList => variantPrefabList;
    public List<BoardPointComponent> BoardPointList => boardPointList;
}