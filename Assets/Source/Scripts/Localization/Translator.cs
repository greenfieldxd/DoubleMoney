using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    static int languageID;
    static List<TranslateText> listID = new List<TranslateText>();

    #region ВЕСЬ ТЕКСТ [двумерный массив]

    private static string[,] lineText =
    {
        #region АНГЛИЙСКИЙ
        {
            "Loading...",
            "PLAY",
            "Richest People",
            "Hidden user",
            "Your budget:",
            "Cards",
            "Rock",
            "Scissors",
            "Paper",
            "Random",
            "Menu",
        },
        #endregion

        #region РУССКИЙ
        {
            "Загрузка...",
            "ИГРАТЬ",
            "Богатейшие Люди",
            "Пользователь скрыт",
            "Твой бюджет:",
            "Карты",
            "Камень",
            "Ножницы",
            "Бумага",
            "Случайно",
            "Меню",
        },
        #endregion
    };
    #endregion

    static public void SelectLanguage(int id)
    {
        languageID = id;
        UpdateText();
    }

    static public string GetText (int textKey)
    {
        return lineText[languageID, textKey];
    }

    static public void Add(TranslateText idText)
    {
        listID.Add(idText);
    }
    static public void Delete(TranslateText idText)
    {
        listID.Remove(idText);
    }

    static public void UpdateText()
    {
        for (int i = 0; i < listID.Count; i++)
        {
            listID[i].TextUI.text = GetText(listID[i].TextID);
        }
    }
}