using UnityEngine;
using UnityEngine.UI;


public class canvasOpenerScript : MonoBehaviour
{
    public Canvas canvas;

    public void Start()
    {
        canvas.enabled = false;    
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && canvas.enabled == true)
        { 
            canvas.enabled=false;        
        }
    }
    public void OnButtonClick()
    {
        canvas.enabled = !canvas.enabled;
    }

    public void CloseCanvas()
    { 
        canvas.enabled = false; 
    }
}
