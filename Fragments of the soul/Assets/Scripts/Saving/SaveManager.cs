using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    string savePath;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath + "/save.json";
    }

    // ================== PUBLIC ==================

    public void SaveGame()
    {
        dataToSave data = new dataToSave();

        SavePlayer(data);
        SaveEnemies(data);
        SaveShrines(data);
        SaveSkillTree(data);

        File.WriteAllText(savePath, JsonUtility.ToJson(data, true));
        Debug.Log("GAME SAVED");
       

    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("NO SAVE FILE");
            return;
        }

        dataToSave data =
            JsonUtility.FromJson<dataToSave>(File.ReadAllText(savePath));

        LoadPlayer(data);
        LoadEnemies(data);
        LoadShrines(data);
        LoadSkillTree(data);

        Debug.Log("GAME LOADED");
    }

    // ================== PLAYER ==================

    void SavePlayer(dataToSave data)
    {
        Player player = FindObjectOfType<Player>();

        data.player = new PlayerData
        {
            position = player.transform.position,
            hp = player.currentHealth,
            corruptionPoints = player.getCorruprionLevel()
        };
    }

    void LoadPlayer(dataToSave data)
    {
        Player player = FindObjectOfType<Player>();

        player.transform.position = data.player.position;
        player.currentHealth = data.player.hp;
        player.setCorruprionLevel(data.player.corruptionPoints);
    }

    // ================== ENEMIES ==================

    void SaveEnemies(dataToSave data)
    {
        BaseOponnentScript[] enemies = FindObjectsOfType<BaseOponnentScript>();

        foreach (BaseOponnentScript e in enemies)
        {
            data.enemies.Add(new EnemyData
            {
                id = e.enemyID,
                position = e.transform.position,
                hp = e.hp

            });
        }

    }

    void LoadEnemies(dataToSave data)
    {
        BaseOponnentScript[] enemies = FindObjectsOfType<BaseOponnentScript>();

        foreach (BaseOponnentScript e in enemies)
        {
            EnemyData saved =
                data.enemies.Find(x => x.id == e.enemyID);

            if (saved == null)
            {
                Destroy(e.gameObject);
                Debug.Log("ni ma");
                continue;
                
            }
            Debug.Log("saved");
            e.transform.position = saved.position;
            if (e.hp <= 0)
            {
                e.isLoading = true;
            }
            else {
                e.isLoading = false;
            }
                e.hp = saved.hp;
            e.isLoading = false;
            
        }
    }

    // ================== SHRINES ==================

    void SaveShrines(dataToSave data)
    {
        ShrineBehavoiur[] shrines = FindObjectsOfType<ShrineBehavoiur>();

        foreach (ShrineBehavoiur s in shrines)
        {
            data.shrines.Add(new ShrineData
            {
                id = s.id,
                healUsed = s.healUsed,
                cleanseUsed = s.cleanseUsed
            });
        }
    }

    void LoadShrines(dataToSave data)
    {
        ShrineBehavoiur[] shrines = FindObjectsOfType<ShrineBehavoiur>();

        foreach (ShrineBehavoiur s in shrines)
        {
            ShrineData saved =
                data.shrines.Find(x => x.id == s.id);

            if (saved == null)
                continue;

            s.healUsed = saved.healUsed;
            s.cleanseUsed = saved.cleanseUsed;
        }
    }

    // ================== SKILL TREE ==================

    void SaveSkillTree(dataToSave data)
    {
        SkillTreeManager tree = FindObjectOfType<SkillTreeManager>();

        data.skillTree = new SkillTreeData
        {
            gotHpUpgrade = tree.gotHpUpgrade,
            gotAttackUpgrade = tree.gotAttackUpgrade,
            gotDefUpgrade = tree.gotDefUpgrade
        };
    }

    void LoadSkillTree(dataToSave data)
    {
        if (data.skillTree == null)
            return;

        SkillTreeManager tree = FindObjectOfType<SkillTreeManager>();

        tree.gotHpUpgrade = data.skillTree.gotHpUpgrade;
        tree.gotAttackUpgrade = data.skillTree.gotAttackUpgrade;
        tree.gotDefUpgrade = data.skillTree.gotDefUpgrade;

        tree.ApplyLoadedSkills();
    }
}
