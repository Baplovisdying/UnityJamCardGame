using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IDoEffect
{
    [SerializeField] private Maps map;
    [SerializeField] private Game game;
    private SpriteRenderer sr;

    [SerializeField] private int nextRoundPos;

    [SerializeField] private GameObject nextPosSign;
    [SerializeField] private Animator Abottom;
    [SerializeField] private Animator Dbottom;

    [SerializeField] private Animator playerAnima;
    [SerializeField] private GameObject targetSign;

    [SerializeField] private CameraController cameraController;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        GameModel.onMoveStageChange += OnGameStageChange;
        //PlayerModel.onHPLowerAction += OnHurt;
        Game.OnGameRoundPushAction += DoAction;
        BattleManager.onUsingCardAction += OnUsingCard;
        PlayerModel.onHPLowerAction += OnTakingDamage;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        GameModel.onMoveStageChange -= OnGameStageChange;
        //PlayerModel.onHPLowerAction -= OnHurt;
        Game.OnGameRoundPushAction -= DoAction;
        BattleManager.onUsingCardAction -= OnUsingCard;
        PlayerModel.onHPLowerAction -= OnTakingDamage;
    }

    private void OnMouseEnter()
    {
        if (BattleManager.instance.UsingCard)
        {
            targetSign.SetActive(true);
        }
        else
        {
            targetSign.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        targetSign.SetActive(false);
    }

    private void Update()
    {
        //HandleMoving();
        HandleNextRoundMove();
    }

    /* void HandleMoving()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (PlayerModel.Pos > 0 && PlayerModel.InvincibleFrame >= 0)
            {
                PlayerModel.Pos--;
                map.m_mapIsUsed[PlayerModel.Pos] = true;
                map.m_mapIsUsed[PlayerModel.Pos + 1] = false;
                sr.flipX = false;
                transform.position = map.m_maps[PlayerModel.Pos].position;
                game.GameRound++;
                return;
            }

            if (map.m_mapIsUsed[PlayerModel.Pos - 1])
            {
                Debug.Log(PlayerModel.InvincibleFrame);
                if (PlayerModel.Pos > 1 && PlayerModel.Pos < map.m_maps.Length - 1)
                {
                    PlayerModel.Pos += 2;
                    map.m_mapIsUsed[PlayerModel.Pos] = true;
                    map.m_mapIsUsed[PlayerModel.Pos - 2] = false;
                }

                sr.flipX = true;
                transform.position = map.m_maps[PlayerModel.Pos].position;
                game.GameRound++;
                PlayerModel.Hp--;
            }
            else if (PlayerModel.Pos > 0 && !map.m_mapIsUsed[PlayerModel.Pos - 1])
            {
                PlayerModel.Pos--;
                map.m_mapIsUsed[PlayerModel.Pos] = true;
                map.m_mapIsUsed[PlayerModel.Pos + 1] = false;
                sr.flipX = false;
                transform.position = map.m_maps[PlayerModel.Pos].position;
                game.GameRound++;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (PlayerModel.Pos < map.m_maps.Length - 1 && PlayerModel.InvincibleFrame >= 0)
            {
                PlayerModel.Pos++;
                map.m_mapIsUsed[PlayerModel.Pos] = true;
                map.m_mapIsUsed[PlayerModel.Pos - 1] = false;
                sr.flipX = true;
                transform.position = map.m_maps[PlayerModel.Pos].position;
                game.GameRound++;
                return;
            }

            if (map.m_mapIsUsed[PlayerModel.Pos + 1])
            {
                if (PlayerModel.Pos > 1)
                {
                    PlayerModel.Pos -= 2;
                    map.m_mapIsUsed[PlayerModel.Pos] = true;
                    map.m_mapIsUsed[PlayerModel.Pos + 2] = false;
                }

                sr.flipX = false;
                transform.position = map.m_maps[PlayerModel.Pos].position;
                game.GameRound++;
                PlayerModel.Hp--;
            }
            else if (PlayerModel.Pos < map.m_maps.Length - 1 && !map.m_mapIsUsed[PlayerModel.Pos + 1])
            {
                PlayerModel.Pos++;
                map.m_mapIsUsed[PlayerModel.Pos] = true;
                map.m_mapIsUsed[PlayerModel.Pos - 1] = false;
                sr.flipX = true;
                transform.position = map.m_maps[PlayerModel.Pos].position;
                game.GameRound++;
            }
        }
    } */

    void HandleNextRoundMove()
    {
        if (PlayerModel.isDead) return;
        if (GameModel.isPaused) return;
        if (game.EnemyRound) return;
        if (!GameModel.OnMoveStage) return;

        if (nextRoundPos != 0)
        {
            nextPosSign.SetActive(true);
        }
        else
        {
            nextPosSign.SetActive(false);
        }
        map.m_mapIsUsed[PlayerModel.Pos] = false;
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.instance.Move.Play();
            Abottom.Play("Abottom");
            if ((PlayerModel.Pos + nextRoundPos) > 0 && !map.m_mapIsUsed[PlayerModel.Pos + nextRoundPos - 1])
            {
                nextRoundPos--;
                PlayerModel.SelectedPos--;
                if (PlayerModel.MaxCanMovePos < Mathf.Abs(nextRoundPos))
                {
                    PlayerModel.SelectedPos++;
                    nextRoundPos++;
                }
                else
                {
                    PlayerModel.PosMoved = Mathf.Abs(nextRoundPos);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SoundManager.instance.Move.Play();
            Dbottom.Play("Dbottom");
            if ((PlayerModel.Pos + nextRoundPos) < map.m_maps.Length - 1 && !map.m_mapIsUsed[PlayerModel.Pos + nextRoundPos + 1])
            {
                nextRoundPos++;
                PlayerModel.SelectedPos++;
                if (PlayerModel.MaxCanMovePos < Mathf.Abs(nextRoundPos))
                {
                    PlayerModel.SelectedPos--;
                    nextRoundPos--;
                }
                else
                {
                    PlayerModel.PosMoved = Mathf.Abs(nextRoundPos);
                }
            }
        }
        nextPosSign.transform.position = map.m_maps[PlayerModel.Pos + nextRoundPos].position;
        //Debug.Log(PlayerModel.Pos + nextRoundPos);
        //Debug.Log(map.m_mapIsUsed[PlayerModel.Pos + nextRoundPos - 1]);
    }

    /* private void OnHurt(int _hp)
    {
        PlayerModel.InvincibleFrame = 3;
        Debug.Log("hit");
    } */

    void DoAction()
    {
        if (PlayerModel.isDead) return;

        if (PlayerModel.MaxMana < (PlayerModel.Mana + Mathf.Abs(nextRoundPos)))
        {
            PlayerModel.Mana = PlayerModel.MaxMana;
            PlayerModel.Hp--;
        }
        else
        {
            PlayerModel.Mana += Mathf.Abs(nextRoundPos);
        }

        if (nextRoundPos > 0)
            sr.flipX = false;
        else
            sr.flipX = true;
        PlayerModel.Pos += nextRoundPos;
        map.m_mapIsUsed[PlayerModel.Pos] = true;
        transform.position = map.m_maps[PlayerModel.Pos].position;
        nextRoundPos = 0;
        PlayerModel.PosMoved = 0;
    }

    void OnGameStageChange(bool _onMoveStage)
    {
        if (Abottom != null && Dbottom != null)
        {
            Abottom.gameObject.SetActive(_onMoveStage);
            Dbottom.gameObject.SetActive(_onMoveStage);
        }
    }

    void OnUsingCard(bool _isUsing)
    {
        if (_isUsing)
            playerAnima.Play("PlayerCharge");
        else
            playerAnima.Play("PlayerIdle");
    }

    void OnTakingDamage(int _i)
    {
        playerAnima.Play("Playerhurt");
    }

    public void DoEffect(int _cardID)
    {
        switch (_cardID)
        {
            case 4:
                DoAddHp();
                break;
            case 7:
                DoAddMaxHp();
                break;
            case 8:
                AddMaxCanMove();
                break;
            case 9:
                AddMaxMana();
                break;
        }
    }

    public bool CheckType(CardType _type)
    {
        if (_type == CardType.ONPLAYER)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DoAddHp()
    {
        if (PlayerModel.Hp < PlayerModel.MaxHp)
            PlayerModel.Hp++;
    }

    void DoAddMaxHp()
    {
        PlayerModel.MaxHp++;
        PlayerModel.Hp++;
    }

    void AddMaxCanMove()
    {
        PlayerModel.MaxCanMovePos++;
    }

    void AddMaxMana()
    {
        PlayerModel.MaxMana += 2;
    }

}
