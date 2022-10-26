using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRangeSign : MonoBehaviour
{
    public static PlayerAttackRangeSign instance;

    public List<GameObject> Signs;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    public void OpenSigns(int _range)
    {
        for (int i = 27; i <= 26 + _range; i++)
        {
            Signs[i].SetActive(true);
            Signs[i - (1 + _range)].SetActive(true);
        }
    }

    public void CloseSigns(int _range)
    {
        Signs.ForEach(s => s.SetActive(false));
    }
}
