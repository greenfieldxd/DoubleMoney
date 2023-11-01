using System;
using Kuhpik;
using Source.Scripts.Tutorial;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class TutorialSystem : GameSystem
    {
        private TutorialStep[] _steps;
        private int _stepIndex;
        
        public override void OnInit()
        {
            if (player.winsCount == 0) StartTutorial();
        }

        private void StartTutorial()
        {
            _steps = new TutorialStep[]
            {
                new MenuHandStep(),
                new RockPaperScissorsStep(),
                new TakeCardStep()
            };
            
            _steps[_stepIndex].Begin();
            _steps[_stepIndex].CompleteEvent += NextStep;
        }

        private void NextStep()
        {
            _steps[_stepIndex].CompleteEvent -= NextStep;
            _stepIndex++;

            if (_stepIndex < _steps.Length)
            {
                _steps[_stepIndex].Begin();
                _steps[_stepIndex].CompleteEvent += NextStep;
            }
        }
    }

    public abstract class TutorialStep
    {
        public event Action CompleteEvent;

        protected abstract void OnBegin();
        protected abstract void OnComplete();

        public virtual void Begin()
        {
            Log(true);
            OnBegin();
        }

        protected virtual void Complete()
        {
            Log(false);
            OnComplete();
            CompleteEvent?.Invoke();
        }

        void Log(bool begins)
        {
            Debug.Log($"Tutorial step {this.GetType().Name} {(begins ? "begins" : "completed")}");
        }
    }
}