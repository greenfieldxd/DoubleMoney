using System;
using System.Collections;
using Cinemachine;
using Kuhpik;
using UnityEngine;

namespace Source.Scripts
{
    public class CameraController : Singleton<CameraController>
    {
        [SerializeField] private CinemachineVirtualCamera[] cameras;
        
        
        public void SwitchCameraWithDelay(int index, float delay = 0, float actionDelay = 0, Action action = null)
        {
            StartCoroutine(DelayRoutine(delay, index, actionDelay, action));
        }

        private void Switch(int index)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                var cam = cameras[i];
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

        public CinemachineVirtualCamera GetCamera(int index)
        {
            return cameras[index];
        }
    }
}