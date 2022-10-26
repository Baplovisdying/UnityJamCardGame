using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int CardID;
    [SerializeField] public Card cardDetail;
    [SerializeField] private Text cardName;
    [SerializeField] private Text cardCast;
    [SerializeField] private Image cardIcon;
    [SerializeField] private Image cardCover;
    [SerializeField] private Image cardBack;
    [SerializeField] private Text cardInfo;
    [SerializeField] private GameObject card;
    [SerializeField] private Animator cardEffect;
    private Vector3 startPos;
    public Vector3 pos;

    public event Action<CardUI> onThisCardUsed;

    private void Start()
    {
        cardDetail = CardManager.instance.GetCardDetail(CardID);
        Init(cardDetail);
        Game.OnCardDisable += CloseInterActable;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        Game.OnCardDisable -= CloseInterActable;
    }

    private void Update()
    {

    }

    public void Init(Card _card)
    {
        cardDetail = _card;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!BattleManager.instance.UsingCard)
        {
            ShowCard.instance.HandleShowCard(transform.position, CardID);
            ShowCard.instance.isShowing = true;
            ShowCard.onBackToPos += OnCardBack;
            ShowCard.instance.idleCard = gameObject;
            card.SetActive(false);

            PlayerAttackRangeSign.instance.OpenSigns(cardDetail.CardRange);
        }
    }

    void OnCardBack(bool _isUsed)
    {
        if (!_isUsed)
        {

            ShowCard.onBackToPos -= OnCardBack;
        }
        else
        {
            ShowCard.onBackToPos -= OnCardBack;
            onThisCardUsed?.Invoke(this);
            GetComponentInParent<CardHolder>().CardUsed();
            BattleManager.instance.UsingCard = false;
            ShowCard.instance.isShowing = false;
        }
        UnityEngine.Debug.Log(cardDetail.CardName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!BattleManager.instance.UsingCard)
        {
            PlayerAttackRangeSign.instance.CloseSigns(cardDetail.CardRange);
        }
    }

    IEnumerator EnableCardSprite()
    {
        yield return new WaitForSeconds(0.09f);
        card.SetActive(true);
    }

    public void DestoryCard()
    {
        /* cardEffect.Play("CardEffect");
        card.SetActive(false); */
    }

    void CloseInterActable()
    {
        cardBack.raycastTarget = false;
    }
}
