using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    //[SerializeField] int savePathNumber;

    public static SaveManager Instance;

    string savePath1, savePath2, savePath3;

    string pathForMainStats;


    [SerializeField] private string scene = "SampleScene";
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        pathForMainStats = Application.persistentDataPath + "/mainStats.json";
        savePath1 = Application.persistentDataPath + "/save1.json";
        savePath2 = Application.persistentDataPath + "/save2.json";
        savePath3 = Application.persistentDataPath + "/save3.json";
    }

    // ================== PUBLIC ==================

    public void SaveGame(int savePathNumber)
    {
        dataToSave data = new dataToSave();

        SavePlayer(data);
        SaveEnemies(data);
        SaveShrines(data);
        SaveSkillTree(data);
        if (savePathNumber == 1) { File.WriteAllText(savePath1, JsonUtility.ToJson(data, true)); }
        else if (savePathNumber == 2) { File.WriteAllText(savePath2, JsonUtility.ToJson(data, true)); }
        else { File.WriteAllText(savePath3, JsonUtility.ToJson(data, true)); }
        
        Debug.Log("GAME SAVED");
       

    }
    public void SaveMainStatsGame()
    {
        dataToSave data = new dataToSave();

        SavePlayer(data);
        SaveEnemies(data);
        SaveShrines(data);
        SaveSkillTree(data);
        File.WriteAllText(pathForMainStats, JsonUtility.ToJson(data, true)); 


        Debug.Log("GAME SAVED");


    }

    public void LoadGame(int savePathNumber)
    {
        string savePath;
        if (savePathNumber == 1)
        { savePath = savePath1; }
        else if (savePathNumber == 2)
        { savePath = savePath2; }
        else 
        { savePath = savePath3; }

        if (!File.Exists(savePath))
            {
                Debug.LogWarning("NO SAVE FILE");
                return;
            }
        
        StartCoroutine(LoadGameCoroutine(savePath));

    }

    public void LoadLastSave()
    {


        string[] files = { savePath1, savePath2, savePath3};

        string latest = files.OrderByDescending(f => File.GetLastWriteTime(f)).First();

        if (!File.Exists(latest))
        {
            Debug.LogWarning("NO SAVE FILE");
            return;
        }

        StartCoroutine(LoadGameCoroutine(latest));


    }
    public void LoadMainStats()
    {
        if (!File.Exists(pathForMainStats))
        {
            Debug.LogWarning("NO SAVE FILE");
            return;
        }

        StartCoroutine(LoadMainStatsCoroutine(pathForMainStats));

    }

    IEnumerator LoadGameCoroutine(string path)
    {
        dataToSave data =
            JsonUtility.FromJson<dataToSave>(File.ReadAllText(path));

        AsyncOperation op = SceneManager.LoadSceneAsync(scene);

        while (!op.isDone)
            yield return null;

        yield return null;

        LoadPlayer(data);
        LoadEnemies(data);
        LoadShrines(data);
        LoadSkillTree(data);

        Debug.Log("GAME LOADED");
    }

    IEnumerator LoadMainStatsCoroutine(string path)
    {
        dataToSave data =
            JsonUtility.FromJson<dataToSave>(File.ReadAllText(path));

        AsyncOperation op = SceneManager.LoadSceneAsync(scene);

        while (!op.isDone)
            yield return null;

        yield return null;

        LoadPlayerCorruprtionAndSkillPoint(data);
        
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
            corruptionPoints = player.getCorruprionLevel(),
            skillUpgradePoint = (player.GetSkillUpgradePoint()+player.skillUpgradePointSpend)
        };
    }

    void LoadPlayer(dataToSave data)
    {
        Player player = FindObjectOfType<Player>();

        player.transform.position = data.player.position;
        player.currentHealth = data.player.hp;
        player.setCorruprionLevel(data.player.corruptionPoints);
       
    }
    void LoadPlayerCorruprtionAndSkillPoint(dataToSave data)
    {
        Player player = FindObjectOfType<Player>();
        player.setCorruprionLevel(data.player.corruptionPoints);
        player.SetSkillUpgradePoint(data.player.skillUpgradePoint);
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
            gotDefUpgrade = tree.gotDefUpgrade,
            gotDefHpUpgrade = tree.gotDefHpUpgrade,
            gotCircleAttackUpgrade = tree.gotCircleAttackUpgrade,
            gotAthHealUpgrade = tree.gotAthHealUpgrade,
            gotShieldUpgrade = tree.gotShieldUpgrade
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
        tree.gotDefHpUpgrade = data.skillTree.gotDefHpUpgrade;
        tree.gotCircleAttackUpgrade = data.skillTree.gotCircleAttackUpgrade;
        tree.gotAthHealUpgrade = data.skillTree.gotAthHealUpgrade;
        tree.gotShieldUpgrade = data.skillTree.gotShieldUpgrade;

        tree.ApplyLoadedSkills();
    }
}
