using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlane : MonoBehaviour
{
    [SerializeField] Transform showTrans;
    [SerializeField] private Transform startTrans;
    private Animator anima;

    private bool isShowing;

    void Start()
    {
        GameModel.onMoveStageChange += HandleShow;
        anima = GetComponent<Animator>();
    }

    void Update()
    {
        if (isShowing)
        {
            Show();
        }
        else
        {
            UnShow();
        }
    }

    void Show()
    {
        transform.position = Vector3.MoveTowards(transform.position, showTrans.position, 2500 * Time.deltaTime);
    }

    void UnShow()
    {
        transform.position = Vector3.MoveTowards(transform.position, startTrans.position, 2000 * Time.deltaTime);
    }

    void HandleShow(bool _isOnMove)
    {
        if (_isOnMove)
        {
            if (anima != null)
                anima.Play("BookClose");
            //isShowing = false;
        }
        else
        {
            if (anima != null)
                anima.Play("BookOpen");
            isShowing = true;
        }
    }

    public void Close()
    {
        isShowing = false;
    }
}
