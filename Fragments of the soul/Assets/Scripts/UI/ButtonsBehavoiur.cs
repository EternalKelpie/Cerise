using UnityEngine;

public class ButtonsBehavoiur : MonoBehaviour
{

    public void DoExitGame()
    {
        Application.Quit();
    }

    public void OnLoadButton(int savePathNumber)
    {
        SaveManager.Instance.LoadGame( savePathNumber);
    }
    public void OnSaveButton(int savePathNumber)
    {
        SaveManager.Instance.SaveGame( savePathNumber);
    }
    public void OnLoadLastSaveButton()
    {
        SaveManager.Instance.LoadLastSave();
    }

}
