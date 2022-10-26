using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
public class SO_LevelData : ScriptableObject
{
    public List<int> Enemy_1_Pos;
    public List<int> Enemy_2_Pos;
}
