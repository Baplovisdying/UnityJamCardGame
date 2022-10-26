using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card
{
    public int CardID;
    public string CardName;
    public int CardCast;
    public Sprite Icon;
    public int CardRange;
    [TextArea(3, 5)]
    public string CardInfo;

    public Sprite cover;
    public Sprite back;

    public CardType type;

    public bool isOneTime;
}
