using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CardData",menuName = "Data/CardData")]
public class SO_CardData : ScriptableObject
{
    public List<Card> Cards;
}
