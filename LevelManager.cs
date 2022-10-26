using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<SO_LevelData> levels;

    public SO_LevelData bossLevel;

    public int levelIndex;

    [SerializeField] private GameObject enemy_1_Prefab;
    [SerializeField] private GameObject enemy_2_Prefab;

    [SerializeField] private GameObject bossPrefab;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }


    private void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.T))
        {
            LoadLevel();
        } */
    }

    public void LoadLevel()
    {
        int num = levels[levelIndex].Enemy_1_Pos.Count;
        for (int i = 0; i < num; i++)
        {
            Instantiate(enemy_1_Prefab, Vector3.zero, Quaternion.identity).GetComponent<Enemy>().m_Pos =
                levels[levelIndex].Enemy_1_Pos[i];
        }

        num = levels[levelIndex].Enemy_2_Pos.Count;
        for (int i = 0; i < num; i++)
        {
            Instantiate(enemy_2_Prefab, Vector3.zero, Quaternion.identity).GetComponent<Enemy>().m_Pos =
                levels[levelIndex].Enemy_2_Pos[i];
        }

        levelIndex++;
    }

    public void LoadBoss()
    {
        int num = bossLevel.Enemy_1_Pos.Count;
        for (int i = 0; i < num; i++)
        {
            Instantiate(enemy_1_Prefab, Vector3.zero, Quaternion.identity).GetComponent<Enemy>().m_Pos =
                bossLevel.Enemy_1_Pos[i];
        }

        num = bossLevel.Enemy_2_Pos.Count;
        for (int i = 0; i < num; i++)
        {
            Instantiate(enemy_2_Prefab, Vector3.zero, Quaternion.identity).GetComponent<Enemy>().m_Pos =
                bossLevel.Enemy_2_Pos[i];
        }

        Instantiate(bossPrefab, transform.position, Quaternion.identity).GetComponent<BOSS>().m_Pos = 38;
    }
}
