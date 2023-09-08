using NaughtyAttributes;
using Source.Scripts.Components;
using System.Collections.Generic;
using UnityEngine;

public class BoardPointComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] List<BoardPointComponent> dependenceList = new List<BoardPointComponent>();

    [SerializeField, BoxGroup("Debug"), ReadOnly] CardComponent cardSlot;

    public int DependenceCount => dependenceList.Count;
    public CardComponent CardSlot => cardSlot;

    public void SetCardSlot(CardComponent card)
    {
        cardSlot = card;
    }
    public bool IsCardSlotAvailable()
    {
        foreach (var dependence in dependenceList)
        {
            if (dependence.cardSlot)
            {
                return false;
            }
        }

        return true;
    }
}