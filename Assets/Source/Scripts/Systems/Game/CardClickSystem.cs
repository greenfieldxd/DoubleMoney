using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Enums;
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
            if (game.blockClicks) return;
            if (game.currentTurnType != TurnType.My) return;
            
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit))
                {
                    var card = hit.transform.GetComponent<CardComponent>();

                    if (card != null)
                    {
                        game.cardsOnBoard.Remove(card);
                        game.actions.Calculate(game.currentTurnType, card);
                    }
                }
            }
        }
    }
}