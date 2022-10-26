using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    [SerializeField] private SO_CardData CardDatas;
    [SerializeField] private GameObject cardUIPrefab;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        if (instance != null) Destroy(gameObject);

        instance = this;
    }


    public Card GetCardDetail(int _ID)
    {
        return CardDatas.Cards.Find(c => c.CardID == _ID);
    }

    public void AddCardsToBag()
    {
        CardModel.CardsInBag.Clear();

        foreach (var c in CardModel.CardBag)
        {
            CardModel.CardsInBag.Add(c);
        }
    }

    public void AddCardsToCardBag(int _cardID)
    {
        CardModel.CardBag.Add(_cardID);
    }

    public void AddCardToDesk()
    {
        if (CardModel.CardsInBag.Count < CardModel.CardPerRound)
        {
            AddCardsToBag();
        }

        for (int i = 0; i < CardModel.CardPerRound; ++i)
        {
            int t = Random.Range(0, CardModel.CardsInBag.Count);

            CardModel.CardsOnDesk.Add(CardModel.CardsInBag[t]);
            CardModel.CardsInBag.RemoveAt(t);
        }
    }
}
