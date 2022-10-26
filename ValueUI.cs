using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueUI : MonoBehaviour
{
    [SerializeField] private GameObject Hpprefab;
    [SerializeField] private GameObject Moveprefab;

    [SerializeField] private List<Value> hpValues = new List<Value>();
    [SerializeField] private List<Value> moveValues = new List<Value>();

    [SerializeField] private float offset;

    [SerializeField] private Animator hpDownEffect;

    void Start()
    {
        InitHp();
        InitMove();
        PlayerModel.onMaxHPHigherAction += AddMaxHp;
        PlayerModel.onMaxHPLowerAction += SubMaxHp;
        PlayerModel.onMaxCanMoveChange += AddMaxMove;

        PlayerModel.onHPHigherAction += HpUp;
        PlayerModel.onHPLowerAction += HpDown;
        PlayerModel.onMovedPosChange += OnPosChange;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        PlayerModel.onMaxHPHigherAction -= AddMaxHp;
        PlayerModel.onMaxHPLowerAction -= SubMaxHp;
        PlayerModel.onMaxCanMoveChange -= AddMaxMove;

        PlayerModel.onHPHigherAction -= HpUp;
        PlayerModel.onHPLowerAction -= HpDown;
        PlayerModel.onMovedPosChange -= OnPosChange;
    }

    void InitHp()
    {
        Transform newTrans = Instantiate(Hpprefab, transform.position, Quaternion.identity).transform;
        newTrans.SetParent(transform);
        Image newImage = newTrans.GetChild(0).GetComponent<Image>();
        Value newValue = new Value();
        newValue.m_Container = newTrans;
        newValue.m_Value = newImage;
        hpValues.Add(newValue);
        AddMaxHp(1);
    }

    void InitMove()
    {
        Transform newTrans = Instantiate(Moveprefab, transform.position + Vector3.down * 50, Quaternion.identity).transform;
        newTrans.SetParent(transform);
        Image newImage = newTrans.GetChild(0).GetComponent<Image>();
        Value newValue = new Value();
        newValue.m_Container = newTrans;
        newValue.m_Value = newImage;
        moveValues.Add(newValue);
        AddMaxMove(1);
    }

    void AddMaxHp(int _hp)
    {
        Vector3 lastPos = hpValues[hpValues.Count - 1].m_Container.position;
        Vector3 newPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z);
        Transform newTrans = Instantiate(Hpprefab, newPos, Quaternion.identity).transform;
        newTrans.SetParent(transform);
        Image newImage = newTrans.GetChild(0).GetComponent<Image>();
        Value newValue = new Value();
        newValue.m_Container = newTrans;
        newValue.m_Value = newImage;
        hpValues.Add(newValue);
        if (PlayerModel.Hp != PlayerModel.MaxHp)
        {
            newImage.enabled = false;
            hpDownEffect.transform.position = hpValues[PlayerModel.Hp].m_Container.position;
            hpDownEffect.Play("HpDownEffect");
            hpValues[PlayerModel.Hp].m_Value.enabled = true;
        }
    }

    void AddMaxMove(int _move)
    {
        Vector3 lastPos = moveValues[moveValues.Count - 1].m_Container.position;
        Vector3 newPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z);
        Transform newTrans = Instantiate(Moveprefab, newPos, Quaternion.identity).transform;
        newTrans.SetParent(transform);
        Image newImage = newTrans.GetChild(0).GetComponent<Image>();
        Value newValue = new Value();
        newValue.m_Container = newTrans;
        newValue.m_Value = newImage;
        moveValues.Add(newValue);
    }

    void SubMaxHp(int _mp)
    {
        Destroy(hpValues[hpValues.Count - 1].m_Container.gameObject);
    }

    void HpDown(int _hp)
    {
        hpDownEffect.transform.position = hpValues[_hp].m_Container.position;
        hpDownEffect.Play("HpDownEffect");
        hpValues[_hp].m_Value.enabled = false;
    }

    void HpUp(int _hp)
    {
        hpDownEffect.Play("HpDownEffect");
        hpDownEffect.transform.position = hpValues[_hp - 1].m_Container.position;
        hpValues[_hp - 1].m_Value.enabled = true;
    }

    void OnPosChange(int _moved)
    {
        for (int i = moveValues.Count - 1; i >= moveValues.Count - _moved; i--)
        {
            moveValues[i].m_Value.enabled = false;
        }
        for (int i = 0; i < moveValues.Count - _moved; i++)
        {
            moveValues[i].m_Value.enabled = true;
        }
    }
}

struct Value
{
    public Transform m_Container;
    public Image m_Value;
}