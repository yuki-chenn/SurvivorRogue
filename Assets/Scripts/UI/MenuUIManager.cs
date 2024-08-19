using UnityEngine;


public class MenuUIManager : MonoBehaviour
{

    private GameObject panelMainMenu;

    private GameObject panelSelect;

    public static MenuUIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        panelMainMenu = transform.Find("MainMenuPanel").gameObject;
        panelSelect = transform.Find("SelectPanel").gameObject;
        //panelSelect.SetActive(false);
        //panelMainMenu.SetActive(false);
        //Invoke("ShowMenu", 0.5f);
    }


    private void ShowMenu()
    {
        EventCenter.Broadcast(EventDefine.ShowMainMenuPanel);
    }

    
}
