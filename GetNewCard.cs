using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetNewCard : MonoBehaviour
{
    [SerializeField] private List<GetNewCardUI> choise;
    [SerializeField] private List<AddableCard> cards;

    [SerializeField] private Transform enterTrans;
    [SerializeField] private Transform hideTrans;

    [SerializeField] private bool isShowing;

    public static event Action onCardSeletOver;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < choise.Count; i++)
        {
            choise[i].onThisCardSelected += CardSelected;
        }
        Game.OnAllEnemyDies += OnShow;
        LevelMap.onLevelSelectOver += Close;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        Game.OnAllEnemyDies -= OnShow;
        LevelMap.onLevelSelectOver -= Close;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (isShowing)
        {
            transform.position = Vector3.MoveTowards(transform.position, enterTrans.position, 2000 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, hideTrans.position, 2000 * Time.deltaTime);
        }
    }

    void CardSelected(int _id)
    {
        choise.ForEach(c => c.button.interactable = false);
        AddableCard selectedCard = cards.Find(c => c.m_ID == _id);
        selectedCard.m_Amount--;
        if (selectedCard.m_Amount == 0)
            cards.Remove(selectedCard);

        CardModel.CardBag.Add(_id);
        onCardSeletOver?.Invoke();
    }

    void OnShow()
    {
        isShowing = true;
        int t_r1 = UnityEngine.Random.Range(0, cards.Count - 1);
        int t_r2 = UnityEngine.Random.Range(0, cards.Count - 1);
        while (t_r2 == t_r1)
            t_r2 = UnityEngine.Random.Range(0, cards.Count - 1);
        int t_r3 = UnityEngine.Random.Range(0, cards.Count - 1);
        while (t_r3 == t_r2 || t_r3 == t_r1)
            t_r3 = UnityEngine.Random.Range(0, cards.Count - 1);

        if (LevelManager.instance.levelIndex == 2)
            t_r2 = 0;

        choise[0].Init(cards[t_r1].m_ID);
        choise[1].Init(cards[t_r2].m_ID);
        choise[2].Init(cards[t_r3].m_ID);
        choise.ForEach(c => c.button.interactable = true);
    }

    void Close()
    {
        isShowing = false;
    }
}

[System.Serializable]
struct AddableCard
{
    public int m_ID;
    public int m_Amount;
}