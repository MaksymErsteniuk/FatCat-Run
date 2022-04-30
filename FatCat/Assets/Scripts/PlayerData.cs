using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable] - allow that class to be Serializable(convert to binary)
// this class used for saving different value about user

[System.Serializable]
public class PlayerData
{
    public int healthyfood;
    public int junkfood;
    public int coins;
    public int levels;
    public int currentMaterialFloorIndex;
    public int currentCatIndex;
    public float volumeOfMusic;
    public float volumeOfEffects;
    public List<int> avaibleMaterialFloor = new List<int> { 0 };
    public List<int> avaibleCat = new List<int> { 0 };
    public PlayerData(int junkFood=0, int healthyFood=0, int Coins=0, int numlevels=1, int CurrentMaterialFloorIndex=0, int CurrentCatIndex=0, int NewFloorMaterialIndex=0, int NewCatIndex=0, float VolumeOfMusic=0.5f, float VolumeOfEffects=0.5f)
    {
        junkfood = junkFood;
        healthyfood = healthyFood;
        coins = Coins;
        levels = numlevels;
        currentMaterialFloorIndex = CurrentMaterialFloorIndex;
        currentCatIndex = CurrentCatIndex;
        volumeOfMusic = VolumeOfMusic;
        volumeOfEffects = VolumeOfEffects;
        if (NewFloorMaterialIndex != 0)
        {
            avaibleMaterialFloor.Add(NewFloorMaterialIndex);
        }
        if (NewCatIndex != 0)
        {
            avaibleCat.Add(NewCatIndex);
        }
    }
}
