using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class BoardSystem : GameSystem
{
    [SerializeField, BoxGroup("Test"), Range(0, 4)] int variantIndex = 0;

    public override void OnInit()
    {
        Signals.Get<CardTakeSignal>().AddListener(UpdateDependence);

        game.Board = FindObjectOfType<BoardComponent>();
        BoardVariantInit();
    }

    void BoardVariantInit()
    {
        foreach (var prefab in game.Board.VariantPrefabList)
        {
            prefab.SetActive(false);
        }

        game.Board.VariantPrefabList[variantIndex].SetActive(true);
        BoardPointComponent[] boardPointList = game.Board.VariantPrefabList[variantIndex].GetComponentsInChildren<BoardPointComponent>();
        foreach (var point in boardPointList)
        {
            game.Board.BoardPointList.Add(point);
        }
    }
    void UpdateDependence()
    {
        foreach (var point in game.Board.BoardPointList)
        {
            if (!point.CardSlot) continue;

            point.CardSlot.SetAvailable(point.IsCardSlotAvailable());
        }
    }
}