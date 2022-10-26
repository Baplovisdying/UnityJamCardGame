using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerModel
{
    public static void ReLoad()
    {
        m_isDead = false;
        m_Pos = 26;
        m_Hp = 2;
        m_MaxHp = 2;
        m_mana = 0;
        m_MaxMana = 9;
        m_MaxCanMovePos = 2;
        m_posPlayerMoved = 0;
        m_selectedPos = 0;
    }

    private static int m_Pos = 26;

    public static event Action<int> onPosChangeAction;

    public static int Pos
    {
        get { return m_Pos; }
        set
        {
            if (value != m_Pos)
            {
                m_Pos = value;
                onPosChangeAction?.Invoke(value);
            }
        }
    }

    private static int m_Hp = 2;
    private static int m_MaxHp = 2;
    public static bool m_isDead;

    public static event Action<int> onHPLowerAction;
    public static event Action<int> onHPHigherAction;

    public static event Action<int> onMaxHPHigherAction;
    public static event Action<int> onMaxHPLowerAction;

    public static event Action onPlayerIsDead;

    public static int Hp
    {
        get { return m_Hp; }
        set
        {
            if (value != m_Hp)
            {
                if (m_Hp > value)
                {
                    onHPLowerAction?.Invoke(value);
                    SoundManager.instance.Impact.Play();
                }
                else
                {
                    onHPHigherAction?.Invoke(value);
                }
                m_Hp = value;
                if (m_Hp == 0)
                {
                    m_isDead = true;
                    onPlayerIsDead?.Invoke();
                }
            }
        }
    }

    public static bool isDead
    {
        get { return m_isDead; }
        set
        {
            if (value == true)
            {
                m_isDead = value;
                onPlayerIsDead?.Invoke();
            }
        }
    }

    public static int MaxHp
    {
        get { return m_MaxHp; }
        set
        {
            if (m_MaxHp > value) onMaxHPLowerAction?.Invoke(value);
            else onMaxHPHigherAction?.Invoke(value);
            m_MaxHp = value;
        }
    }

    private static int m_mana = 0;
    private static int m_MaxMana = 9;

    public static event Action<int> onManaLowerAction;
    public static event Action<int> onManaHigherAction;

    public static event Action<int> onMaxManaChangeAction;

    public static int Mana
    {
        get { return m_mana; }
        set
        {
            if (value != m_mana)
            {
                if (m_mana > value) onManaLowerAction?.Invoke(value);
                else onManaHigherAction?.Invoke(value);
                m_mana = value;
            }
        }
    }

    public static int MaxMana
    {
        get { return m_MaxMana; }
        set
        {
            m_MaxMana = value;
            onMaxManaChangeAction?.Invoke(value);
        }
    }

    private static int m_invincibleFrame;

    public static event Action onInvisibleFrameStartAction;
    public static event Action onInvisibleFrameEndAction;

    public static int InvincibleFrame
    {
        get { return m_invincibleFrame; }
        set
        {
            m_invincibleFrame = value;
            if (value > m_invincibleFrame)
            {
                onInvisibleFrameStartAction?.Invoke();
            }
            if (value == 0)
            {
                onInvisibleFrameEndAction?.Invoke();
            }
        }
    }

    private static int m_selectedPos;

    public static event Action<int> onPosSelected;

    public static int SelectedPos
    {
        get { return m_selectedPos; }
        set
        {
            m_selectedPos = value;
            onPosSelected?.Invoke(value);
        }
    }

    private static int m_posPlayerMoved = 0;
    public static int m_MaxCanMovePos = 2;
    public static event Action<int> onMaxCanMoveChange;
    public static event Action<int> onMovedPosChange;

    public static int PosMoved
    {
        get { return m_posPlayerMoved; }
        set
        {
            m_posPlayerMoved = value;
            onMovedPosChange?.Invoke(value);
        }
    }

    public static int MaxCanMovePos
    {
        get { return m_MaxCanMovePos; }
        set
        {
            m_MaxCanMovePos = value;
            onMaxCanMoveChange?.Invoke(value);
        }
    }
}
