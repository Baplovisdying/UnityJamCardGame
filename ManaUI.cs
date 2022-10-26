using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private Transform starTrans;
    [SerializeField] private Transform actionTrans;

    void Update()
    {
        if (GameModel.OnMoveStage)
        {
            transform.position = Vector3.MoveTowards(transform.position, starTrans.position, 3500f * Time.deltaTime);

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, actionTrans.position, 3500f * Time.deltaTime);

        }
    }
}
