using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToShowCardUi : MonoBehaviour, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler
{
    public int CardID;
    [SerializeField] private Card cardDetail;
    [SerializeField] private Text cardName;
    [SerializeField] private Text cardCast;
    [SerializeField] private Image cardIcon;
    [SerializeField] private Image cardCover;
    [SerializeField] private Image cardBack;
    [SerializeField] private Text cardInfo;
    [SerializeField] private GameObject card;
    [SerializeField] private Animator cardDestoryEffect;
    public Vector3 pos;

    private void Start()
    {

    }

    private void Update()
    {
        //HandlePos();
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
        CardID = _card.CardID;
    }

    void HandlePos()
    {
        if (Vector3.Distance(transform.position, pos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 5);
        }
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        if (!BattleManager.instance.UsingCard)
        {
            ShowCard.instance.isShowing = false;
            ShowCard.instance.CellBackToPos(false);
            PlayerAttackRangeSign.instance.CloseSigns(cardDetail.CardRange);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(myRay.origin.x, myRay.origin.y), Vector2.zero);

        if (hit.collider)
        {
            if (cardDetail.CardCast > PlayerModel.Mana)
            {
                BattleManager.instance.UsingCard = false;
                ShowCard.instance.isShowing = false;
                ShowCard.instance.CellBackToPos(false);
                return;
            }

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                int distance = Mathf.Abs(hit.collider.GetComponent<Enemy>().m_Pos - 26);
                if (distance > cardDetail.CardRange)
                {
                    BattleManager.instance.UsingCard = false;
                    ShowCard.instance.isShowing = false;
                    ShowCard.instance.CellBackToPos(false);
                    return;
                }
            }


            IDoEffect doEffect = hit.collider.GetComponent<IDoEffect>();
            if (doEffect != null && doEffect.CheckType(cardDetail.type))
            {
                doEffect.DoEffect(CardID);
                Debug.Log("doEffect");
                cardDestoryEffect.transform.position = card.transform.position;
                cardDestoryEffect.Play("CardDestory");
                PlayerModel.Mana -= cardDetail.CardCast;
                ShowCard.instance.CellBackToPos(true);
            }
            else
            {
                BattleManager.instance.UsingCard = false;
                ShowCard.instance.isShowing = false;
                ShowCard.instance.CellBackToPos(false);
            }
        }
        else
        {
            BattleManager.instance.UsingCard = false;
            ShowCard.instance.isShowing = false;
            ShowCard.instance.CellBackToPos(false);
        }

        Debug.Log("drag end");
        /* if (enemyDetect.enemy != null)
        {
            IDoEffect doEffect = enemyDetect.enemy;
            if (doEffect != null)
            {
                Debug.Log("YES!!!");
            }
            else
            {
                BattleManager.instance.usingCard = false;
                ShowCard.instance.isShowing = false;
                ShowCard.instance.CellBackToPos();
            }
        }
        else
        {
            BattleManager.instance.usingCard = false;
            ShowCard.instance.isShowing = false;
            ShowCard.instance.CellBackToPos();
        } */
        PlayerAttackRangeSign.instance.CloseSigns(cardDetail.CardRange);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        BattleManager.instance.UsingCard = true;
        BattleManager.instance.currentCardID = CardID;
        BattleManager.instance.currentCardTrans = transform;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ShowCard.instance.isShowing = true;
    }
}
