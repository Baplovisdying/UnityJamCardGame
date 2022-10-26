using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameModel
{
    public static void ReLoad()
    {
        m_isPause = false;
        m_onMoveStage = true;
        Final = false;
    }

    public static bool Final;
    public static bool OnBoss;

    private static bool m_isPause;

    public static event Action<bool> onGamePause;

    public static bool isPaused
    {
        get { return m_isPause; }
        set
        {
            onGamePause?.Invoke(value);
            m_isPause = value;
        }
    }

    private static bool m_onMoveStage = true;
    public static event Action<bool> onMoveStageChange;
    public static bool OnMoveStage
    {
        get { return m_onMoveStage; }
        set
        {
            m_onMoveStage = value;
            onMoveStageChange?.Invoke(value);
        }
    }
}
