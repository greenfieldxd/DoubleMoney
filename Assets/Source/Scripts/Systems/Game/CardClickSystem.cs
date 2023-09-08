using Kuhpik;
using Source.Scripts.Components;
using System.Linq;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class CardClickSystem : GameSystem
    {
        private Camera _cam;
        
        public override void OnInit()
        {
            _cam = Camera.main;    
        }

        public override void OnUpdate()
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit))
                {
                    var card = hit.transform.GetComponent<CardComponent>();

                    if (card != null && card.IsAvailable)
                    {
                        game.cardsOnBoard.Remove(card);
                        BoardPointComponent point = game.board.BoardPointList.First(x => x.CardSlot == card);
                        point.SetCardSlot(null);
                        card.SetAvailable(false);
                        
                        Supyrb.Signals.Get<CardTakeSignal>().Dispatch();
                        Supyrb.Signals.Get<CalculateSignal>().Dispatch(game.currentTurnType, card);
                    }
                }
            }
        }
    }
}