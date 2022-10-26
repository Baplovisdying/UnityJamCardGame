using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetManaEffectPool : MonoBehaviour
{
    [SerializeField] private Maps maps;
    [SerializeField] private Stack<GameObject> effects = new Stack<GameObject>(22);

    [SerializeField] private Transform toPos;

    [SerializeField] private int lastPos = 26;

    void Start()
    {
        PlayerModel.onPosChangeAction += OnPosChange;

        ManaEffect[] e = GetComponentsInChildren<ManaEffect>();
        for (int i = 0; i < e.Length; i++)
        {
            effects.Push(e[i].gameObject);
            e[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        PlayerModel.onPosChangeAction -= OnPosChange;
    }


    void OnPosChange(int _newPos)
    {
        if (_newPos > lastPos)
        {
            for (int i = lastPos + 1; i <= _newPos; i++)
            {
                effects.Peek().SetActive(true);
                ManaEffect effect = effects.Pop().GetComponent<ManaEffect>();
                effect.toPos = toPos.position;
                effect.transform.position = maps.m_maps[i].position;
                effect.onReach += Reached;
            }
        }
        else
        {
            for (int i = lastPos - 1; i >= _newPos; i--)
            {
                effects.Peek().SetActive(true);
                ManaEffect effect = effects.Pop().GetComponent<ManaEffect>();
                effect.toPos = toPos.position;
                effect.transform.position = maps.m_maps[i].position;
                effect.onReach += Reached;
            }
        }
        lastPos = _newPos;
    }

    void Reached(GameObject effect)
    {
        effects.Push(effect);
        effect.GetComponent<ManaEffect>().onReach -= Reached;
        effect.SetActive(false);
    }
}
