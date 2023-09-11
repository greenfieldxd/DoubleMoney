using System;
using System.Linq;
using Kuhpik;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class BoardSystem : GameSystem
    {
        public override void OnInit()
        {
            Supyrb.Signals.Get<CardTakeSignal>().AddListener(UpdateDependence);

            game.board = FindObjectOfType<BoardComponent>();
            BoardVariantInit();
        }

        void BoardVariantInit()
        {
            var boardType = game.currentDuelConfig.BoardType;
            var variantPrefab = game.board.VariantPrefabList.First(x => x.Type == boardType).Variant;
            var boardVariant = Instantiate(variantPrefab, Vector3.zero, quaternion.identity, game.table.BoardPosition);
            boardVariant.transform.localPosition = Vector3.zero;
            
            BoardPointComponent[] boardPointList = boardVariant.GetComponentsInChildren<BoardPointComponent>();
            
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
}