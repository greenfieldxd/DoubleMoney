using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

namespace Source.Scripts.Systems.Loading
{
    public class ChangePlaneSystem : GameSystem
    {
        [SerializeField] [Tag] private string planeTag;
        [SerializeField] private Material[] materials;

        public override void OnInit()
        {
            SelectPlaneMaterial();
        }

        private void SelectPlaneMaterial()
        {
            if (player.planeMatIndex >= materials.Length)
            {
                player.planeMatIndex = 0;
            }

            GameObject.FindWithTag(planeTag).GetComponent<MeshRenderer>().material = materials[player.planeMatIndex];
        }
    }
}