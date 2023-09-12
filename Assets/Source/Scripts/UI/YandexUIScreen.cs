using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class YandexUIScreen : UIScreen
{
    [SerializeField, BoxGroup("Component")] GameObject loadingPanel;

    public GameObject LoadingPanel => loadingPanel;
}