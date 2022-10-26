using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour, IDoEffect, IEnemy
{
    [SerializeField] public int m_Pos = 19;
    [SerializeField] private Maps maps;

    [SerializeField] private bool movingRight;
    [SerializeField] private Transform player;

    [SerializeField] private Game game;
    [SerializeField] private bool pepreAttack;
    [SerializeField] private bool attackDone;
    [SerializeField] private AttackSign attackSign;
    [SerializeField] private GameObject targetSign;
    [SerializeField] private int m_HP;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Animator hitEffect;
    [SerializeField] private Animator bloodEffect;

    [SerializeField] private List<GameObject> HPBars;
    [SerializeField] private GameObject feezeSign;
    [SerializeField] private int AttackRange;
    [SerializeField] private int StopRange;

    private int lastMarked;

    private int roundNeededTOMove = 1;
    private int roundPassed = 0;
    private int feezedTime = 0;

    private SpriteRenderer sr;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, maps.m_maps[m_Pos].position, 20 * Time.deltaTime);
    }

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        maps = GameObject.FindObjectOfType<Maps>();
        attackSign = GameObject.FindObjectOfType<AttackSign>();
        game = GameObject.FindObjectOfType<Game>();
        //maps.m_mapIsUsed[m_Pos] = true;
        player = GameObject.FindObjectOfType<Player>().transform;
        Game.OnGameRoundPushAction += OnEnemyRound;
        transform.position = maps.m_maps[m_Pos].position;
        game.allEnemys.Add(this);
        cameraController = GameObject.FindObjectOfType<CameraController>();

    }

    private void OnDestroy()
    {
        maps.m_mapIsUsed[m_Pos] = false;
        game.allEnemys.Remove(this);
        Game.OnGameRoundPushAction -= OnEnemyRound;
        if (game.allEnemys.Count == 0)
        {
            FadeInOut.instance.GetFadeOut("WinScene");
        }
    }

    /* void OnRoundPush()
    {
        if (player.position.x > transform.position.x)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }

        if (pepreAttack)
        {
            if (Vector3.Distance(transform.position, player.position) < 0.75 && PlayerModel.InvincibleFrame <= 0)
            {
                PlayerModel.Hp--;
            }
            attackDone = true;
        }

        if (movingRight && !pepreAttack)
        {
            sr.flipX = true;
            if (Vector3.Distance(transform.position, player.position) < 1.5)
            {
                attackSign.signs[m_Pos + 1].SetActive(true);
                attackSign.signs[m_Pos - 1].SetActive(true);
                pepreAttack = true;

            }
            else if (m_Pos < maps.m_maps.Length && !maps.m_mapIsUsed[m_Pos + 1])
            {
                maps.m_mapIsUsed[m_Pos + 1] = true;
                m_Pos++;
                maps.m_mapIsUsed[m_Pos - 1] = false;
                transform.position = maps.m_maps[m_Pos].position;
            }
        }
        else if (!movingRight && !pepreAttack)
        {
            sr.flipX = false;
            if (Vector3.Distance(transform.position, player.position) < 1.5)
            {
                attackSign.signs[m_Pos + 1].SetActive(true);
                attackSign.signs[m_Pos - 1].SetActive(true);
                pepreAttack = true;
            }
            else if (m_Pos > 0 && !maps.m_mapIsUsed[m_Pos - 1])
            {
                maps.m_mapIsUsed[m_Pos - 1] = true;
                m_Pos--;
                maps.m_mapIsUsed[m_Pos + 1] = false;
                transform.position = maps.m_maps[m_Pos].position;
            }
        }

        if (attackDone)
        {
            pepreAttack = false;
            attackDone = false;
            attackSign.signs[m_Pos + 1].SetActive(false);
            attackSign.signs[m_Pos - 1].SetActive(false);
        }
    } */

    public void OnEnemyRound()
    {
        if (game.PlayerRound) return;


        if (player.position.x > transform.position.x)
        {
            sr.flipX = true;
            movingRight = true;
        }
        else
        {
            sr.flipX = false;
            movingRight = false;
        }

        if (pepreAttack)
        {
            if (PlayerModel.Pos == lastMarked)
            {
                PlayerModel.Hp--;
            }
            attackSign.signs[lastMarked].SetActive(false);
            attackDone = true;
        }

        if (attackDone)
        {
            attackDone = false;
            pepreAttack = false;
            return;
        }

        if (feezedTime-- > 0) return;
        feezeSign.SetActive(false);

        if (++roundPassed < roundNeededTOMove) return;

        if (StopRange >= Mathf.Abs(m_Pos - PlayerModel.Pos) && Mathf.Abs(m_Pos - PlayerModel.Pos) != 1)
        {
            Debug.Log("x");
            int t_r = Random.Range(1, 100);
            if (t_r > 15)
            {
                if (PlayerModel.Pos <= m_Pos + AttackRange)
                {
                    pepreAttack = true;
                    if (t_r >= 55)
                    {
                        if (t_r % 2 == 0)
                        {
                            attackSign.signs[PlayerModel.Pos + 1].SetActive(true);
                            lastMarked = PlayerModel.Pos + 1;
                        }
                        else
                        {
                            attackSign.signs[PlayerModel.Pos - 1].SetActive(true);
                            lastMarked = PlayerModel.Pos - 1;
                        }
                        return;
                    }
                    attackSign.signs[PlayerModel.Pos].SetActive(true);
                    lastMarked = PlayerModel.Pos;
                    return;
                }
                else if (PlayerModel.Pos <= m_Pos - AttackRange)
                {
                    pepreAttack = true;
                    if (t_r >= 65)
                    {
                        if (t_r % 2 == 0)
                        {
                            attackSign.signs[PlayerModel.Pos + 1].SetActive(true);
                            lastMarked = PlayerModel.Pos + 1;
                        }
                        else
                        {
                            attackSign.signs[PlayerModel.Pos - 1].SetActive(true);
                            lastMarked = PlayerModel.Pos - 1;
                        }
                        return;
                    }
                    attackSign.signs[PlayerModel.Pos].SetActive(true);
                    lastMarked = PlayerModel.Pos;
                    return;
                }
            }
        }

        if (movingRight)
        {
            if (PlayerModel.Pos <= m_Pos + AttackRange)
            {
                pepreAttack = true;
                attackSign.signs[PlayerModel.Pos].SetActive(true);
                lastMarked = PlayerModel.Pos;
                return;
            }
            if (m_Pos < maps.m_maps.Length - 1 && !maps.m_mapIsUsed[m_Pos + 1])
            {
                //maps.m_mapIsUsed[m_Pos + 1] = true;
                m_Pos++;
                //maps.m_mapIsUsed[m_Pos - 1] = false;
                //transform.position = maps.m_maps[m_Pos].position;
            }
        }
        else
        {
            if (PlayerModel.Pos >= m_Pos - AttackRange)
            {
                pepreAttack = true;
                attackSign.signs[PlayerModel.Pos].SetActive(true);
                lastMarked = PlayerModel.Pos;
                return;
            }
            if (m_Pos > 0 && !maps.m_mapIsUsed[m_Pos - 1])
            {
                //maps.m_mapIsUsed[m_Pos - 1] = true;
                m_Pos--;
                //maps.m_mapIsUsed[m_Pos + 1] = false;
                //transform.position = maps.m_maps[m_Pos].position;
            }
        }

        if (m_Pos == 26) PlayerModel.isDead = true;

        roundPassed = 0;

        if (pepreAttack)
        {
            enemyAnim.Play("EnemyReady_1");
        }
        else
        {
            enemyAnim.Play("GostIdle");
        }
    }

    public void DoEffect(int _cardID)
    {
        targetSign.SetActive(false);
        switch (_cardID)
        {
            case 1:
                HandleAttack_1();
                break;
            case 2:
                HandleAttack_2();
                break;
            case 3:
                HandleKonckBack();
                break;
            case 5:
                HandleDisable();
                break;
            case 6:
                HandleFeeze();
                break;
            case 10:
                HandleGetClose();
                break;
        }

        SoundManager.instance.Impact.Play();

        if (m_HP <= 0)
        {
            enemyAnim.Play("EnemyDead_1");
            if (pepreAttack)
            {
                attackSign.signs[lastMarked].SetActive(false);
            }
            Destroy(gameObject, 0.8f);
        }
    }

    public bool CheckType(CardType _type)
    {
        if (_type == CardType.ONENEMY)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    IEnumerator TimeController(float _slowedTime)
    {
        Time.timeScale = _slowedTime;
        while (Time.timeScale < 1)
        {
            Time.timeScale += 0.1f;
            if (Time.timeScale > 1)
            {
                Time.timeScale = 1;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    void HandleAttack_1()
    {
        m_HP--;
        cameraController.StartCameraShake(0.08f);
        hitEffect.Play("EnemyHitEffect_1");
        bloodEffect.Play("EnemyBloodEffect_1");
        StartCoroutine(TimeController(0.05f));
        if (m_HP >= 0)
        {
            HPBars[m_HP].SetActive(false);
        }
        else
        {
            HPBars.ForEach(h => h.SetActive(false));
        }
    }

    void HandleAttack_2()
    {
        m_HP -= 2;
        cameraController.StartCameraShake(0.09f);
        hitEffect.Play("EnemyHitEffect_2");
        bloodEffect.Play("EnemyBloodEffect_2");
        StartCoroutine(TimeController(0.04f));
        if (m_HP >= 0)
        {
            HPBars[m_HP + 1].SetActive(false);
            HPBars[m_HP].SetActive(false);
        }
        else
        {
            HPBars.ForEach(h => h.SetActive(false));
        }

    }

    void HandleKonckBack()
    {
        if (pepreAttack)
        {
            attackSign.signs[lastMarked].SetActive(false);
            attackSign.signs[lastMarked].SetActive(false);
            pepreAttack = false;
        }
        int count = 0;
        if (movingRight)
        {
            for (int i = m_Pos; i >= 0; i--)
            {
                count++;
                if (!maps.m_mapIsUsed[i] && count > 2)
                {
                    //maps.m_mapIsUsed[m_Pos] = false;
                    //maps.m_mapIsUsed[i] = true;
                    m_Pos = i;
                    break;
                }
            }
            //transform.position = maps.m_maps[m_Pos].transform.position;
        }
        else
        {
            for (int i = m_Pos; i < 53; i++)
            {
                count++;
                if (!maps.m_mapIsUsed[i] && count > 2)
                {
                    //maps.m_mapIsUsed[m_Pos] = false;
                    //maps.m_mapIsUsed[i] = true;
                    m_Pos = i;
                    break;
                }
            }
            //transform.position = maps.m_maps[m_Pos].transform.position;
        }
        cameraController.StartCameraShake(0.09f);
        hitEffect.Play("EnemyHitEffect_3");
        StartCoroutine(TimeController(0.08f));
    }

    void HandleDisable()
    {
        roundNeededTOMove = 2;
        cameraController.StartCameraShake(0.09f);
        hitEffect.Play("EnemyHitEffect_4");
        bloodEffect.Play("EnemyBloodEffect_3");
        StartCoroutine(TimeController(0.08f));
    }

    void HandleFeeze()
    {
        feezedTime = 2;
        feezeSign.SetActive(true);
        cameraController.StartCameraShake(0.08f);
        hitEffect.Play("EnemyHitEffect_5");
        StartCoroutine(TimeController(0.2f));
    }

    void HandleGetClose()
    {
        if (pepreAttack)
        {
            attackSign.signs[lastMarked].SetActive(false);
            attackSign.signs[lastMarked].SetActive(false);
            pepreAttack = false;
        }
        int count = 0;
        if (movingRight)
        {
            for (int i = m_Pos; i < 26; i++)
            {
                count++;
                if (!maps.m_mapIsUsed[i] && count > 2)
                {
                    //maps.m_mapIsUsed[m_Pos] = false;
                    //maps.m_mapIsUsed[i] = true;
                    m_Pos = i;
                    break;
                }
            }
            //transform.position = maps.m_maps[m_Pos].transform.position;
        }
        else
        {
            for (int i = m_Pos; i > 26; i--)
            {
                count++;
                if (!maps.m_mapIsUsed[i] && count > 2)
                {
                    //maps.m_mapIsUsed[m_Pos] = false;
                    //maps.m_mapIsUsed[i] = true;
                    m_Pos = i;
                    break;
                }
            }
            //transform.position = maps.m_maps[m_Pos].transform.position;
        }
        cameraController.StartCameraShake(0.09f);
        hitEffect.Play("EnemyHitEffect_3");
        StartCoroutine(TimeController(0.08f));
    }
}
