using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public int currentCardID;
    public Transform currentCardTrans;
    [SerializeField] private CameraController cameraController;

    private bool m_usingCard;

    public bool UsingCard
    {
        get { return m_usingCard; }
        set
        {
            m_usingCard = value;
            onUsingCardAction?.Invoke(m_usingCard);
        }
    }

    public static event Action<bool> onUsingCardAction;

    private void Start()
    {
        if (instance != null) Destroy(gameObject);

        instance = this;
    }

    void DoCardEffect(IDoEffect _enemy)
    {
        _enemy.DoEffect(currentCardID);
    }

    public void DoCameraShake(float _amount)
    {
        cameraController.StartCameraShake(_amount);
    }
}
