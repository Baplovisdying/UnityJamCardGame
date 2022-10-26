using System.Data;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text manaText;
    [SerializeField] private Text maxManaText;
    [SerializeField] private Text hpText;
    [SerializeField] private List<CardUI> cards;
    [SerializeField] private List<CardHolder> cardsTrans;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Button gameStageChangeToAction;

    [SerializeField] private Button gameStageChangeToMove;

    [SerializeField] private GameObject blockPlane;

    private void Start()
    {
        PlayerModel.onManaHigherAction += OnManaChange;
        PlayerModel.onManaLowerAction += OnManaChange;
        PlayerModel.onMaxManaChangeAction += OnMaxManaChange;
        PlayerModel.onHPHigherAction += OnHPChange;
        PlayerModel.onHPLowerAction += OnHPChange;
        PlayerModel.onPosChangeAction += OnPlayerPos;

        Game.OnAllEnemyDies += ClearAllCardsOnDesk;
        gameStageChangeToAction.onClick.AddListener(() =>
        {
            GameModel.OnMoveStage = !GameModel.OnMoveStage;
            gameStageChangeToAction.gameObject.SetActive(false);
            //AddCard();
            StartCoroutine(AddCards());
        });
        gameStageChangeToMove.onClick.AddListener(() =>
        {
            GameModel.OnMoveStage = true;
            PlayerModel.Mana = 0;
            ClearAllCardsOnDesk();
        });
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        PlayerModel.onManaHigherAction -= OnManaChange;
        PlayerModel.onManaLowerAction -= OnManaChange;
        PlayerModel.onMaxManaChangeAction -= OnMaxManaChange;
        PlayerModel.onHPHigherAction -= OnHPChange;
        PlayerModel.onHPLowerAction -= OnHPChange;
        PlayerModel.onPosChangeAction -= OnPlayerPos;

        Game.OnAllEnemyDies -= ClearAllCardsOnDesk;
    }

    void OnManaChange(int _mana)
    {
        manaText.text = _mana.ToString();
    }

    void OnHPChange(int _hp)
    {
        hpText.text = _hp.ToString();
    }

    void Test()
    {
        /* if (Input.GetKeyDown(KeyCode.P))
        {
            AddCard();
        } */
        /* if (Input.GetKeyDown(KeyCode.E))
        {
            GameModel.OnMoveStage = !GameModel.OnMoveStage;
        } */
    }

    void AddCard()
    {
        CardManager.instance.AddCardToDesk();
        for (int i = 0; i < CardModel.CardsOnDesk.Count; i++)
        {
            int id = CardModel.CardsOnDesk[i];
            CardHolder cardHolder = Instantiate(cardPrefab, spawnPos.position, Quaternion.identity).GetComponent<CardHolder>();
            CardUI newCard = cardHolder.GetComponentInChildren<CardUI>();
            cardHolder.transform.SetParent(transform);
            newCard.CardID = id;
            newCard.onThisCardUsed += OnCardUsed;
            cards.Add(newCard);
            cardsTrans.Add(cardHolder);
        }
        CardModel.CardsOnDesk.Clear();
        HandleCardPos();
    }

    void Update()
    {
        Test();
    }

    void HandleCardPos()
    {
        int num = cards.Count;
        //num--;
        float len = (rightPos.position.x - leftPos.position.x) / num;
        Vector3 pos = new Vector3(leftPos.position.x, leftPos.position.y, 0);
        foreach (var c in cardsTrans)
        {
            c.pos = pos;
            pos = new Vector3(pos.x + len, pos.y, 0);
        }
    }

    void OnCardUsed(CardUI _usedCard)
    {
        SoundManager.instance.CardSelect.Play();
        if (_usedCard.cardDetail.isOneTime)
        {
            CardModel.CardBag.Remove(_usedCard.cardDetail.CardID);
        }
        cards.Remove(_usedCard);
        cardsTrans.Remove(_usedCard.GetComponentInParent<CardHolder>());
        _usedCard.onThisCardUsed -= OnCardUsed;
        HandleCardPos();
    }

    void OnPlayerPos(int _pos)
    {
        if (_pos == 26)
        {
            gameStageChangeToAction.gameObject.SetActive(true);
        }
        else
        {
            gameStageChangeToAction.gameObject.SetActive(false);
        }
    }

    void ClearAllCardsOnDesk()
    {
        blockPlane.SetActive(true);
        cardsTrans.ForEach(c => c.DestoryCards());

        cardsTrans.Clear();
        cards.Clear();
    }

    IEnumerator AddCards()
    {
        SoundManager.instance.BookOpen.Play();
        yield return new WaitForSeconds(0.5f);
        AddCard();
        blockPlane.SetActive(false);
    }

    void OnMaxManaChange(int _newMaxMana)
    {
        maxManaText.text = _newMaxMana.ToString();
    }


}
