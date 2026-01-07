using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class dataToSave
{
    // public int levelToLoad;   // if 1 then first and so on
    public PlayerData player;
    public List<EnemyData> enemies = new();
    public List<ShrineData> shrines = new();
    public SkillTreeData skillTree;
}

[System.Serializable]
public class PlayerData
{
    public Vector2 position;
    public float hp;
    public float corruptionPoints;
    public float skillUpgradePoint;
}

[System.Serializable]
public class EnemyData
{
    public string id;
    public Vector2 position;
    public float hp;
}

[System.Serializable]
public class ShrineData
{
    public string id;
    public bool healUsed;
    public bool cleanseUsed;
}

[System.Serializable]
public class SkillTreeData
{
    public bool gotHpUpgrade;
    public bool gotAttackUpgrade;
    public bool gotDefUpgrade;
    public bool gotDefHpUpgrade;
    public bool gotCircleAttackUpgrade;
    public bool gotAthHealUpgrade ;
    public bool gotShieldUpgrade;
}
