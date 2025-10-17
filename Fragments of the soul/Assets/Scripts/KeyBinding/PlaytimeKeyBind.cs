using UnityEngine;
using UnityEngine.UI;

public class PlaytimeKeyBind : MonoBehaviour
{
    public Canvas PauseMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))  
        {
            PauseMenu.enabled = true;
        }
       
    }
}
