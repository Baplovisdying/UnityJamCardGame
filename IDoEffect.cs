using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoEffect
{
    void DoEffect(int _cardID);
    bool CheckType(CardType _type);
}
