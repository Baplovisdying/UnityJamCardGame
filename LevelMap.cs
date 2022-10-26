using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelMap : MonoBehaviour
{
    [SerializeField] private Transform startTrans;
    [SerializeField] private Transform enterTrans;
    [SerializeField] private Animator anima;
    [SerializeField] private bool isShowing;
    [SerializeField] private List<Button> nextLevel;
    [SerializeField] private int LevelIndex = 1;

    [SerializeField] private GameObject buttonsHolder;

    [SerializeField] private List<Image> locks;

    [SerializeField] private Button BossButton;

    public static event Action onLevelSelectOver;


    private void Start()
    {
        GetNewCard.onCardSeletOver += OnLevelChoise;
        nextLevel[LevelIndex].onClick.AddListener(ChangeButton);

        BossButton.onClick.AddListener(() =>
        {
            GameModel.Final = true;
            OnLevelClose();
            LevelManager.instance.LoadBoss();
            buttonsHolder.SetActive(false);
            BossButton.gameObject.SetActive(false);
            SoundManager.instance.BGM_2.Stop();
            SoundManager.instance.BGM_3.Play();
        });

        BossButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isShowing)
            transform.position = Vector3.MoveTowards(transform.position, enterTrans.position, 2500 * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, startTrans.position, 2500 * Time.deltaTime);

        /* if (Input.GetKeyDown(KeyCode.T))
        {
            OnLevelChoise();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OnLevelClose();
        } */
    }

    void OnLevelChoise()
    {
        isShowing = true;
        anima.Play("MapOpen");
    }

    void OnLevelClose()
    {
        anima.Play("MapClose");
        //nextLevel[LevelIndex].onClick.AddListener(ChangeButton)
        onLevelSelectOver?.Invoke();
    }

    public void SetUnShowing()
    {
        isShowing = false;
    }

    void ChangeButton()
    {
        LevelManager.instance.LoadLevel();
        OnLevelClose();
        nextLevel[LevelIndex].interactable = false;
        if (LevelIndex == nextLevel.Count - 1)
        {
            BossButton.gameObject.SetActive(true);
        }
        if (LevelIndex < nextLevel.Count - 1)
            LevelIndex++;
        nextLevel[LevelIndex].onClick.AddListener(ChangeButton);
        buttonsHolder.SetActive(false);
    }

    public void ShowButtons()
    {
        locks[LevelIndex].enabled = false;
        buttonsHolder.SetActive(true);
    }
}
