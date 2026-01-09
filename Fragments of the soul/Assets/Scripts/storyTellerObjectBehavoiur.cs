using UnityEngine;
using TMPro;
using System.Collections;

public class StoryTellerObjectBehaviour : MonoBehaviour
{
    public GameObject interactPopup;
    public GameObject messagePanel;
    public TMP_Text messageText;
    

    [SerializeField] private string message = "The fox is you";
    [SerializeField] private float interactionDistance = 3f;
    
    private Player player;
    private PlaytimeKeyBind keyBind;
    private bool isMessageShowing = false;
    private bool hasBeenPickedUp = false;
    
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        keyBind = FindAnyObjectByType<PlaytimeKeyBind>();
        
        if (interactPopup != null) interactPopup.SetActive(false);
        if (messagePanel != null) messagePanel.SetActive(false);
    }
    
    void Update()
    {
        if (player == null || keyBind == null || hasBeenPickedUp)
            return;
        

        CheckDistance();

        if (!isMessageShowing && Input.GetKeyDown(KeyCode.E))
        {

            if (Vector3.Distance(transform.position, player.transform.position) <= interactionDistance)
            {

                PickUpObject();
            }
        }
        
    }
    
    void CheckDistance()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool isInRange = distance <= interactionDistance;
        
        if (interactPopup != null)
        {

            interactPopup.SetActive(isInRange && !hasBeenPickedUp && !isMessageShowing);
        }
    }
    
    void PickUpObject()
    {
        hasBeenPickedUp = true;
        

        if (interactPopup != null)
            interactPopup.SetActive(false);
        
        ShowMessage();
    }
    
    void ShowMessage()
    {
        isMessageShowing = true;
        
        Debug.Log("Showing message: " + message); 
        

            keyBind.TogglePauseWithNoPauseMenu();
            messagePanel.SetActive(true);
            messageText.text = message;
  
    }
    
    public void CloseMessage()
    {
        Debug.Log("Closing message"); 
        
            isMessageShowing = false;
        

            messagePanel.SetActive(false);
            keyBind.TogglePauseWithNoPauseMenu();


        StartCoroutine(DestroyAfterFrame());
    }

    System.Collections.IEnumerator DestroyAfterFrame()
    {
        yield return null;
        Destroy(gameObject);
    }

   
}