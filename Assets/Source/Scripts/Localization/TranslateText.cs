using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class TranslateText : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] int textID;

    TMP_Text textUI;

    public int TextID => textID;
    public TMP_Text TextUI => textUI;
    void Awake()
    {
        textUI = GetComponent<TMP_Text>();

        Translator.Add(this);
    }
    void OnEnable()
    {
        Translator.UpdateText();
    }
    void OnDestroy()
    {
        Translator.Delete(this);
    }
    public void SetIndex(int index)
    {
        textID = index;
        Translator.UpdateText();
    }
}