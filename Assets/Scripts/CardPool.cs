using System.Collections.Generic;
using UnityEngine;

public static class CardPool
{
    private static List<CardBehaviour> CardsInHand { get; } = new List<CardBehaviour>();
    private static int _index = 0;

    public static void ChangeCardValue(int value)
    {
        if (_index >= CardsInHand.Count)
            _index = 0;
        var card = CardsInHand[_index++].GetComponent<CardBehaviour>();
        var rand = Random.Range(0, 3);
        switch (rand)
        {
            case 1:
                card.Health += value;
                break;
            case 2:
                card.Damage += value;
                break;
            default:
                card.Mana += value;
                break;
        }
        card.RefreshUi();
        card.CheckHealth();
    }

    public static void CheckCardFromHand(CardBehaviour card, int index)
    {
        if (CardsInHand.Exists(x => x == card))
            CardsInHand.Remove(card);
        else
            CardsInHand.Insert(index, card);
    }

    public static void AddCard(CardBehaviour card) => CardsInHand.Add(card);
}
