using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CardModel
{
    public static List<int> CardBag;
    private static List<int> m_cardsInBag = new List<int>();

    public static event Action onCardsInBagChangeAction;

    public static List<int> CardsInBag
    {
        get { return m_cardsInBag; }
    }

    private static List<int> m_cardsOnDesk = new List<int>();

    public static event Action onCardsOnDeskChangeAction;

    public static List<int> CardsOnDesk
    {
        get { return m_cardsOnDesk; }
    }

    private static int m_cardPerRound = 5;
    public static event Action onCardPerRoundChange;
    public static int CardPerRound
    {
        get { return m_cardPerRound; }
        set
        {
            m_cardPerRound = value;
            onCardPerRoundChange?.Invoke();
        }
    }
}
