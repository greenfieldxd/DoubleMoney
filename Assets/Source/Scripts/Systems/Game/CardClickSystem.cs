using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Supyrb;
using System.Linq;
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
                        BoardPointComponent point = game.Board.BoardPointList.FirstOrDefault(x => x.CardSlot == card);
                        point.SetCardSlot(null);

                        Destroy(card.gameObject);

                        Signals.Get<CardTakeSignal>().Dispatch();
                    }
                }
            }
        }
    }
}