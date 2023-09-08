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

        game.board = FindObjectOfType<BoardComponent>();
        BoardVariantInit();
    }

    void BoardVariantInit()
    {
        foreach (var prefab in game.board.VariantPrefabList)
        {
            prefab.SetActive(false);
        }

        game.board.VariantPrefabList[variantIndex].SetActive(true);
        BoardPointComponent[] boardPointList = game.board.VariantPrefabList[variantIndex].GetComponentsInChildren<BoardPointComponent>();
        foreach (var point in boardPointList)
        {
            game.board.BoardPointList.Add(point);
        }
    }
    void UpdateDependence()
    {
        foreach (var point in game.board.BoardPointList)
        {
            if (!point.CardSlot) continue;

            point.CardSlot.SetAvailable(point.IsCardSlotAvailable());
        }
    }
}