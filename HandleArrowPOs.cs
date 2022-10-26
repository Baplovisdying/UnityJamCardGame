using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleArrowPOs : MonoBehaviour
{
    [SerializeField] private Transform Arrowtrans;

    void Update()
    {
        if (BattleManager.instance.UsingCard)
        {
            Arrowtrans.position = BattleManager.instance.currentCardTrans.position;
        }
    }
}
