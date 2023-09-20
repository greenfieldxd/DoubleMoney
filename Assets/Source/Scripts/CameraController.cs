using System;
using System.Collections;
using Cinemachine;
using Kuhpik;
using UnityEngine;

namespace Source.Scripts
{
    public class CameraController : Singleton<CameraController>
    {
        [SerializeField] private CinemachineVirtualCamera[] otherCameras;
        
        
        public void SwitchCameraWithDelay(int index, float delay = 0, float actionDelay = 0, Action action = null)
        {
            StartCoroutine(DelayRoutine(delay, index, actionDelay, action));
        }

        private void Switch(int index)
        {
            for (int i = 0; i < otherCameras.Length; i++)
            {
                var cam = otherCameras[i];
                cam.Priority = i == index ? 10 : 0;
            }
        }
        
        
        private IEnumerator DelayRoutine(float delay, int index, float actionDelay = 0, Action action = null)
        {
            yield return new WaitForSeconds(delay);
            Switch(index);
            
            yield return new WaitForSeconds(actionDelay);
            action?.Invoke();
        }

        public CinemachineVirtualCamera GetOtherCamera(int index)
        {
            return otherCameras[index];
        }
    }
}