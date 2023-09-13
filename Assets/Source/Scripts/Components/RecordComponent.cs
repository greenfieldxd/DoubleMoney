using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class RecordComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Text")] TMP_Text number;
    [SerializeField, BoxGroup("Text")] TMP_Text playerName;
    [SerializeField, BoxGroup("Text")] TMP_Text value;

    public TMP_Text Number => number;
    public TMP_Text PlayerName => playerName;
    public TMP_Text Value => value;
}