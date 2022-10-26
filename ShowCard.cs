using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class ShowCard : MonoBehaviour
{
    public static ShowCard instance;
    [SerializeField] public GameObject showedCard;
    [SerializeField] private Transform cardTrans;
    public GameObject idleCard;

    private Vector3 ordPos;
    [SerializeField] private float upAmount;
    public bool isShowing;
    private int cardID;
    public static event Action<bool> onBackToPos;

    [SerializeField] public bool roundEnd;

    private void Start()
    {
        if (instance != null) Destroy(gameObject);

        instance = this;
    }

    public void HandleShowCard(Vector3 _pos, int _id)
    {
        ordPos = _pos;
        cardTrans.position = ordPos;
        cardID = _id;
    }

    private void Update()
    {
        if (!roundEnd)
        {
            if (isShowing)
            {
                Show();
                Vector3 toPos = new Vector3(ordPos.x, ordPos.y + upAmount, 0);

                cardTrans.position = Vector3.MoveTowards(cardTrans.position, toPos, 1400f * Time.deltaTime);
            }
            else
            {
                //onBackToPos?.Invoke(false);
                showedCard.SetActive(false);
            }
        }
    }

    void Show()
    {
        showedCard.SetActive(true);
        Card toShowCard = CardManager.instance.GetCardDetail(cardID);
        showedCard.GetComponentInParent<ToShowCardUi>().Init(toShowCard);
    }

    public void CellBackToPos(bool _isUsed)
    {
        onBackToPos?.Invoke(_isUsed);
        idleCard.SetActive(!_isUsed);
    }

}
