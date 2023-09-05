using Kuhpik;
using Source.Scripts.Components;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class CreateDeckSystem : GameSystem
    {
        [SerializeField] private CardComponent cardPrefab;
        
        public override void OnInit()
        {
            
        }
    }
}