using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] CameraMovement camMove;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject WinUI;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] List<GameObject> BikeShowCases;
    [SerializeField] UserDataSO initUserData;
    public List<GameObject> Levels;
    public List<Image> LevelImages;
    Color opaqueColor = Color.white;
    Color TransparentColor = new Color(1, 1, 1, 0.5f);
    public int coins;
    public List<GameObject> bikes;
    public UserDataSO userData;
    public GameObject currentBike;
    public GameObject currentLevel;
    public bool LevelComplete = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        UserDataSaver.Load(userData);
        setCoinsText();
        EnableLevels();
        setShopBikeStatus();
        bikes = userData.bikes;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            goToMainMenu();
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            Application.Quit();
        }
    }

    public void resetEntireGame()
    {
        userData.coins = initUserData.coins;
        userData.bikesUnlockStatus = new List<bool>(initUserData.bikesUnlockStatus);
        userData.bikesCost = new List<int>(initUserData.bikesCost);
        userData.bikes = new List<GameObject>(initUserData.bikes);
        userData.currentBike = initUserData.currentBike;
        userData.currentLevel = initUserData.currentLevel;
        userData.unlockedLevel = initUserData.unlockedLevel;
        userData.totalLevels = initUserData.totalLevels;

        UserDataSaver.Save(userData);

        setCoinsText();
        resetLevelImages();
        EnableLevels();
        setShopBikeStatus();
        bikes = userData.bikes;
    }

    public void LoadLevel(int i)
    {
        Debug.Log("Load Level" + (userData.unlockedLevel - 1));
        if(i < userData.unlockedLevel)
        {
            resetLevels();
            currentBike = Instantiate(bikes[userData.currentBike]);
            currentBike.SetActive(true);
            currentLevel = Levels[i];   
            userData.currentLevel = i;
            currentLevel.SetActive(true);
            currentBike.transform.position = currentLevel.transform.GetChild(0).transform.position;
            currentBike.transform.rotation = currentLevel.transform.GetChild(0).transform.rotation;
            camMove.target = currentBike.transform;
        }
        UserDataSaver.Save(userData);
    }

    public void LoadCurrentLevel()
    {
        if(userData.currentLevel < userData.totalLevels)
        {
            Debug.Log("Current Level" + " " + (userData.currentLevel < userData.totalLevels) + currentLevel);
            resetLevels();
            currentBike = Instantiate(bikes[userData.currentBike]);
            currentBike.SetActive(true);
            currentLevel = Levels[userData.currentLevel];
            currentLevel.SetActive(true);
            currentBike.transform.position = currentLevel.transform.GetChild(0).transform.position;
            currentBike.transform.rotation = currentLevel.transform.GetChild(0).transform.rotation;
            camMove.target = currentBike.transform;
        }
        else
        {
            currentLevel.SetActive(false);
            Canvas.SetActive(true);
            userData.currentLevel -= 1;
            //userData.unlockedLevel -= 1;
        }
        UserDataSaver.Save(userData);

    }

    public void RestartLevel()
    {
        currentBike.transform.position = currentLevel.transform.GetChild(0).transform.position;
        currentBike.transform.rotation = currentLevel.transform.GetChild(0).transform.rotation;
        currentBike.GetComponent<BikeMovementScript>().resetBike();
    }

    void resetLevels()
    {
        MainMenu.GetComponent<MainMenuHandler>().setToMainMenu();
        Canvas.SetActive(false);
        WinUI.SetActive(false);
        LevelComplete = false;
        Destroy(currentBike);
        foreach (var level in Levels)
        {
            level.SetActive(false);
        }
        /*foreach(var bike in bikes)
        {
            bike.SetActive(false);
        }*/
    }

    public void goToMainMenu()
    {
        MainMenu.GetComponent<MainMenuHandler>().setToMainMenu();
        Destroy(currentBike);
        Canvas.SetActive(true);
        WinUI.SetActive(false);
        LevelComplete = false;
        foreach (var level in Levels)
        {
            level.SetActive(false);
        }
        /*foreach (var bike in bikes)
        {
            bike.SetActive(false);
        }*/
    }

    public void FinishedActions()
    {
        
        Debug.Log("Finished Protocols");
        WinUI.SetActive(true);
        userData.coins += 10;
        setCoinsText();
        if(userData.currentLevel < userData.totalLevels)
        {
            userData.currentLevel += 1;
            userData.unlockedLevel += 1;     
        }
        if(userData.unlockedLevel > userData.totalLevels)
        {
            userData.unlockedLevel -= 1;
        }
        EnableLevels();
        LevelComplete = true;

    }

    void setCoinsText()
    {
        coinsText.text = userData.coins.ToString();
    }

    void EnableLevels()
    {
        int unlockedLevels = userData.unlockedLevel;
        for(int i = 0; i < unlockedLevels; i++)
        {
            LevelImages[i].color = Color.white;
        }
    }

    void resetLevelImages()
    {
        foreach(var li in LevelImages)
        {
            li.color = TransparentColor;
        }
    }

    public void chooseBike(int i)
    {
        Debug.Log("Choosing Bike" + i);
        if (userData.bikesUnlockStatus[i])
        {
            userData.currentBike = i;
        }
        else
        {
            buyBike(i);
        }
    }

    public void buyBike(int i)
    {
        int bikeCost = userData.bikesCost[i];
        coins = userData.coins;
        if (coins >= bikeCost)
        {
            coins -= bikeCost;
            userData.coins = coins;
            userData.bikesUnlockStatus[i] = true;
            userData.currentBike = i;
            setCoinsText();
            setShopBikeStatus();
            UserDataSaver.Save(userData);
        }
        else
        {
            return;
        }
    }

    void setShopBikeStatus()
    {
        List<bool> x = userData.bikesUnlockStatus;
        if(x.Count == BikeShowCases.Count)
        {
            for (int i = 0; i < x.Count; i++)
            {
                GameObject go = BikeShowCases[i].transform.GetChild(0).gameObject;
                go.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = userData.bikesCost[i].ToString();
                go.SetActive(!x[i]);
            }
        }
        
    }

    private void OnApplicationQuit()
    {
        UserDataSaver.Save(userData);
    }
}
