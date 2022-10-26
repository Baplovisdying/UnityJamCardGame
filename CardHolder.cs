using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public Vector3 pos;

    [SerializeField] private Animator effect;
    [SerializeField] private CardUI cardUI;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        HandlePos();
    }

    void HandlePos()
    {
        if (Vector3.Distance(transform.position, pos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 5000 * Time.deltaTime);
        }
    }

    public void DestoryCards()
    {
        Destroy(gameObject, 0.9f);
        effect.Play("CardEffect");
        cardUI.gameObject.SetActive(false);
        BattleManager.instance.UsingCard = false;
        ShowCard.instance.isShowing = false;
    }

    public void CardUsed()
    {
        Destroy(gameObject, 1f);
    }
}
