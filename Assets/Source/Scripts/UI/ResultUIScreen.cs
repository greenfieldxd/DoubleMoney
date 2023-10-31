using Kuhpik;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class ResultUIScreen : UIScreen
    {
        [SerializeField, BoxGroup("GameObject")] GameObject resultPanel;
        [SerializeField, BoxGroup("GameObject")] GameObject winPanel;
        [SerializeField, BoxGroup("GameObject")] GameObject losePanel;

        [SerializeField, BoxGroup("Button")] Button homeButton;

        [SerializeField, BoxGroup("Text")] TMP_Text reward;

        public GameObject ResultPanel => resultPanel;
        public GameObject WinPanel => winPanel;
        public GameObject LosePanel => losePanel;
        public Button HomeButton => homeButton;
        public TMP_Text Reward => reward;
    }
}