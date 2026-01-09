using UnityEngine;
using TMPro;

public class UpdatePointDisplayer : MonoBehaviour
{
    public TMP_Text pointsText;
    private Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        
    }
    void Update()
    {
        pointsText.text = player.GetSkillUpgradePoint().ToString();
    }
}
