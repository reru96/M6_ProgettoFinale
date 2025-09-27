using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int sceneIndex;
    public float playerX, playerY, playerZ;
    public int score;
    public int collectedGems;
    public float musicVolume;
    public float sfxVolume;
    public float hp;
}

