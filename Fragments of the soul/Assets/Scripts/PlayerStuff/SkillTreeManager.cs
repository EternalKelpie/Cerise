using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    [SerializeField] bool canChooseRoute = false;
    public bool chosenCorruptionPath = false, chosenCleanPath = false;

    public Player player;

    public Button DefButton, HpButton, AttackButton;
    public Image DefImage, HpImage, AttackImage;

    public bool gotHpUpgrade = false, gotAttackUpgrade = false, gotDefUpgrade = false;

    public Button defHpButton, atkHealButton, circleAthButton, shieldButton;
    public Image defHpImage, atkHealImage, circleAthImage, shieldImage;

    public bool gotDefHpUpgrade = false, gotCircleAttackUpgrade = false, gotAthHealUpgrade = false, gotShieldUpgrade = false;

    private void Start()
    {
        if (canChooseRoute == false)
        {
            CleanPath();
            CorruptionPath();
        }
    }

    public void DefUpgrade()
    {   if (player.GetSkillUpgradePoint() > 0 && !gotDefUpgrade)
        {
            gotDefUpgrade = true;
            player.ScaleDef(1.5f);
            DefButton.enabled = false;
            DefImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
        }
    }

    public void HpUpgrade()
    {
        if (player.GetSkillUpgradePoint() > 0 && !gotHpUpgrade)
        {
            gotHpUpgrade = true;
            player.ScaleHp(1.5f);
            HpButton.enabled = false;
            HpImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
        }
    }

    public void AttackUpgrade()
    {
        if (player.GetSkillUpgradePoint() > 0 && !gotAttackUpgrade)
        {
            gotAttackUpgrade = true;
            player.ScaleAttack(1.3f);
            AttackButton.enabled = false;
            AttackImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
        }
    }
    public int ApplyLoadedSkills()
    {
        int howManySkills = 0;
        if (gotAttackUpgrade)
        {
            gotAttackUpgrade = true;
            gotAttackUpgrade = true;
            player.ScaleAttack(1.3f);
            AttackButton.enabled = false;
            AttackImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            howManySkills++;
        }
        if (gotHpUpgrade)
        {
            gotHpUpgrade = true;
            player.ScaleHp(1.5f);
            HpButton.enabled = false;
            HpImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            howManySkills++;
        }
        if (gotDefUpgrade)
        {
            gotDefUpgrade = true;
            player.ScaleDef(1.5f);
            DefButton.enabled = false;
            DefImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            howManySkills++;
        }
        if (gotDefHpUpgrade)
        {
            gotDefHpUpgrade = true;
            player.ScaleHp(1.5f);
            player.ScaleDef(1.5f);
            defHpButton.enabled = false;
            defHpImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            howManySkills++;
            CleanPath();
        }
        if (gotCircleAttackUpgrade)
        {
            gotCircleAttackUpgrade = true;
            player.circleAttackAcquired = true;
            circleAthButton.enabled = false;
            circleAthImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            howManySkills++;
            CorruptionPath();

        }
        if (gotAthHealUpgrade)
        {
            gotAthHealUpgrade = true;
            player.ScaleAttack(1.3f);
            player.healAfterKillAcquired = true;
            atkHealButton.enabled = false;
            atkHealImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            howManySkills++;
            CorruptionPath();
        }
        if (gotShieldUpgrade)
        {
            gotShieldUpgrade = true;
            player.shieldAcquired = true;
            shieldButton.enabled = false;
            shieldImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            howManySkills++;
            CleanPath();
        }

        return howManySkills;
    }

    public void GetShield()
    {
        if (player.GetSkillUpgradePoint() > 0 && !gotShieldUpgrade)
        {
            gotShieldUpgrade = true;
            player.shieldAcquired = true;
            shieldButton.enabled = false;
            shieldImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            CleanPath();
            chosenCleanPath = true;
        }
    }

    public void GetCircleAttack()
    {
        if (player.GetSkillUpgradePoint() > 0 && !gotCircleAttackUpgrade)
        {
            gotCircleAttackUpgrade = true;
            player.circleAttackAcquired = true;
            circleAthButton.enabled = false;
            circleAthImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            chosenCorruptionPath = true;
            CorruptionPath();

        }
    }

    public void HealandAttackBuff()
    {
        if (player.GetSkillUpgradePoint() > 0 && !gotAthHealUpgrade)
        {
            gotAthHealUpgrade = true;
            player.ScaleAttack(1.3f);
            player.healAfterKillAcquired = true;
            atkHealButton.enabled = false;
            atkHealImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            CorruptionPath();
            chosenCorruptionPath = true;
        }

    }

    public void GetDefAndHpBuff()
    {
        if (player.GetSkillUpgradePoint() > 0 && !gotDefHpUpgrade)
        {
            gotDefHpUpgrade = true;
            player.ScaleHp(1.5f);
            player.ScaleDef(1.5f);
            defHpButton.enabled = false;
            defHpImage.color = Color.black;
            player.SpendSkillUpgradePoint();
            player.skillUpgradePointSpend++;
            CleanPath();
            chosenCleanPath = true;

        }
    }

    public void CorruptionPath()
    {
        defHpButton.enabled = false;
        defHpImage.color = Color.black;

        shieldButton.enabled = false;
        shieldImage.color = Color.black;
        //chosenCorruptionPath = true;

    }

    public void CleanPath()
    {

        circleAthButton.enabled = false;
        circleAthImage.color = Color.black;

        atkHealButton.enabled = false;
        atkHealImage.color = Color.black;
        //chosenCleanPath = true;

    }
   


}
