using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private int m_GameRound;
    [SerializeField] private CameraController cameraController;

    public static event Action OnGameRoundPushAction;
    public static event Action OnAllEnemyDies;
    public static event Action OnCardDisable;

    public bool EnemyRound;
    public bool PlayerRound = true;

    public bool CardActionStage;

    public List<IEnemy> allEnemys = new List<IEnemy>();

    public int GameRound
    {
        get { return m_GameRound; }
        set
        {
            m_GameRound = value;
            OnGameRoundPushAction?.Invoke();
        }
    }
    void Start()
    {
        Time.timeScale = 1;
        FadeInOut.instance.GetFadeIn();
        HandleCardInit();
        GameModel.OnMoveStage = true;
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);

        SoundManager.instance.BGM_1.Stop();
        SoundManager.instance.BGM_2.Play();
    }

    private void Update()
    {
        if (GameModel.OnMoveStage)
        {
            if (PlayerRound)
            {
                HandleNextRound();
            }
            else if (EnemyRound)
            {
                HandleEnemyRound();
                EnemyRound = false;
                PlayerRound = true;
            }
        }
    }

    void HandleNextRound()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerModel.isDead || GameModel.isPaused) return;
            GameRound++;
            PlayerRound = false;
            StartCoroutine(RoundDelay());
        }
    }

    void HandleEnemyRound()
    {
        foreach (var e in allEnemys)
        {
            e.OnEnemyRound();
        }
    }

    IEnumerator RoundDelay()
    {
        yield return new WaitForSeconds(0.08f);
        EnemyRound = true;
    }

    void HandleCardInit()
    {
        CardModel.CardBag = new List<int> { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 4, 4, 5, 6, 7, 8, 9, };
    }

    public void CellAllEnemyDie()
    {
        GameModel.OnMoveStage = true;
        PlayerModel.Mana = 0;
        OnCardDisable?.Invoke();
        OnAllEnemyDies?.Invoke();
    }
}
