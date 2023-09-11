using System;
using System.Linq;
using NaughtyAttributes;
using Source.Scripts.Enums;
using Source.Scripts.Extensions;
using UnityEngine;
using Random = System.Random;

namespace Source.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DuelConfig", menuName = "Config/DuelConfig")]
    public class DuelConfig : ScriptableObject
    {
        [SerializeField] [BoxGroup("Board Type")] private BoardType boardType;
        [SerializeField] [BoxGroup("Other Settings")] private int roundCount;
        [SerializeField] [BoxGroup("Other Settings")] private int neededWins;
        [SerializeField] [BoxGroup("Other Settings")] private int startMoneyCount = 10;

        public BoardType BoardType => boardType;
        public int RoundCount => roundCount;
        public int NeededWins => neededWins;

        public int StartMoneyCount => startMoneyCount;
    }
}