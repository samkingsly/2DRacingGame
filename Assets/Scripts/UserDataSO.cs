using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "user data", menuName = "Scriptable Object/UserDataScriptObj")]
public class UserDataSO : ScriptableObject
{
    public int coins = 0;
    public List<bool> bikesUnlockStatus = new List<bool>() { true, false };
    public List<int> bikesCost = new List<int>() { 0, 20 };
    public List<GameObject> bikes = new List<GameObject>();
    public int currentBike = 1;
    public int currentLevel = 0;
    public int unlockedLevel = 1;
    public int totalLevels = 2;

}
