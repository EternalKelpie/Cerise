using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    public Player player;

    public Button DefButton, HpButton, AttackButton;
    public Image DefImage, HpImage, AttackImage;

    public void DefUpgrade()
    {   if (player.GetSkillUpgradePoint() > 0)
        {
            player.ScaleDef(1.5f);
            DefButton.enabled = false;
            DefImage.color = Color.black;
            player.SpendSkillUpgradePoint();
        }
    }

    public void HpUpgrade()
    {
        if (player.GetSkillUpgradePoint() > 0)
        {
            player.ScaleHp(1.5f);
            HpButton.enabled = false;
            HpImage.color = Color.black;
            player.SpendSkillUpgradePoint();
        }
    }

    public void AttackUpgrade()
    {
        if (player.GetSkillUpgradePoint() > 0)
        {
            player.ScaleAttack(1.3f);
            AttackButton.enabled = false;
            AttackImage.color = Color.black;
            player.SpendSkillUpgradePoint();
        }
    }
}
