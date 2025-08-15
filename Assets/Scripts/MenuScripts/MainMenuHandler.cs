using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject levelMenu;
    GameObject currentScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onShopButtonClicked()
    {
        currentScreen = shopMenu;
        currentScreen.SetActive(true);
    }

    public void onLevelButtonClicked()
    {
        currentScreen = levelMenu;
        currentScreen.SetActive(true);
    }

    public void onBackButtonClicked()
    {
        currentScreen.SetActive(false);
    }

    public void setToMainMenu()
    {
        gameObject.SetActive(true);
        shopMenu.SetActive(false);
        levelMenu.SetActive(false);
    }
}
