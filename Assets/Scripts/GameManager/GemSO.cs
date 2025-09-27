using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Gems", fileName ="Gem")]
public class GemSO : ScriptableObject
{
    public string nameGem;
    public GameObject gemPrefab;
    public GameObject gemPrefabUI;
    public int id;
}
