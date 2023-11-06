using System;
using System.Linq;
using Cinemachine;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

namespace Source.Scripts.Systems.Menu
{
    public class PlayerProgressionSystem : GameSystem
    {
        [SerializeField] [Tag] private string sceneMoneyTag;
        [SerializeField] private MoneyProgression[] moneyProgressions;

        private Transform _sceneMoney;
        
        public override void OnInit()
        {
            _sceneMoney = GameObject.FindWithTag(sceneMoneyTag).transform;
            LoadCurrentProgression();
        }

        private void LoadCurrentProgression()
        {
            var progression = moneyProgressions.Where(x => player.RecordMoney >= x.neededMoney).OrderBy(x => x.neededMoney).Last();
            var cam = CameraController.Instance.GetCamera(2);
            CinemachineComponentBase componentBase = cam.GetCinemachineComponent(CinemachineCore.Stage.Body);
            
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = progression.camDistance;
            }
            
            Instantiate(progression.prefab, _sceneMoney.position, Quaternion.identity, _sceneMoney);
        }

        [Serializable]
        public class MoneyProgression
        {
            public GameObject prefab;
            public int neededMoney;
            public int camDistance;
        }
    }
}