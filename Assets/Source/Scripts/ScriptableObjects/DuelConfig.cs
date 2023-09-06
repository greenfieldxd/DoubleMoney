using System;
using System.Linq;
using Source.Scripts.Extensions;
using UnityEngine;
using Random = System.Random;

namespace Source.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DuelConfig", menuName = "Config/DuelConfig")]
    public class DuelConfig : ScriptableObject
    {
        [SerializeField] private Variant[] variants;
        
        public int GetRoundsCount(int winsCount)
        {
            var canGetVariants = variants.Where(x => winsCount >= x.neededWins).ToArray();
            var rng = new Random();
            rng.Shuffle(canGetVariants);
            return canGetVariants.First().roundCount;
        }
        
        [Serializable]
        public class Variant
        {
            public int roundCount;
            public int neededWins;
        }
    }
}