using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GetNewCardUI : MonoBehaviour
{
    public int ID;
    [SerializeField] private Text cardName;
    [SerializeField] private Text cardCast;
    [SerializeField] private Image cardIcon;
    [SerializeField] private Image cardCover;
    [SerializeField] private Image cardBack;
    [SerializeField] private Text cardInfo;
    [SerializeField] public Button button;

    public event Action<int> onThisCardSelected;

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            onThisCardSelected?.Invoke(ID);
        });
    }


    public void Init(int _ID)
    {
        Card cardDetail = CardManager.instance.GetCardDetail(_ID);
        ID = _ID;
        cardName.text = cardDetail.CardName;
        cardCast.text = cardDetail.CardCast.ToString();
        cardIcon.sprite = cardDetail.Icon;
        cardIcon.SetNativeSize();
        cardCover.sprite = cardDetail.cover;
        cardCover.SetNativeSize();
        cardBack.sprite = cardDetail.back;
        cardBack.SetNativeSize();
        cardInfo.text = cardDetail.CardInfo;
    }
}
