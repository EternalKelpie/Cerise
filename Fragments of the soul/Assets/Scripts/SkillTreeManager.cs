using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    public Player player;

    public Button DefButton, HpButton, AttackButton;
    public Image DefImage, HpImage, AttackImage;

    public bool gotHpUpgrade = false, gotAttackUpgrade = false, gotDefUpgrade = false;
    public void DefUpgrade()
    {   if (player.GetSkillUpgradePoint() > 0 && !gotDefUpgrade)
        {
            gotDefUpgrade = true;
            player.ScaleDef(1.5f);
            DefButton.enabled = false;
            DefImage.color = Color.black;
            player.SpendSkillUpgradePoint();
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
        }
    }

    public void AttackUpgrade()
    {
        if (player.GetSkillUpgradePoint() > 0 && !gotAttackUpgrade && !gotAttackUpgrade)
        {
            gotAttackUpgrade = true;
            gotAttackUpgrade = true;
            player.ScaleAttack(1.3f);
            AttackButton.enabled = false;
            AttackImage.color = Color.black;
            player.SpendSkillUpgradePoint();
        }
    }
    public void ApplyLoadedSkills()
    {
        if (gotAttackUpgrade)
        {
            gotAttackUpgrade = true;
            gotAttackUpgrade = true;
            player.ScaleAttack(1.3f);
            AttackButton.enabled = false;
            AttackImage.color = Color.black;
            player.SpendSkillUpgradePoint();
        }
        if (gotHpUpgrade)
        {
            gotHpUpgrade = true;
            player.ScaleHp(1.5f);
            HpButton.enabled = false;
            HpImage.color = Color.black;
            player.SpendSkillUpgradePoint();
        }
        if (gotDefUpgrade)
        {
            gotDefUpgrade = true;
            player.ScaleDef(1.5f);
            DefButton.enabled = false;
            DefImage.color = Color.black;
            player.SpendSkillUpgradePoint();
        }


    }

}
