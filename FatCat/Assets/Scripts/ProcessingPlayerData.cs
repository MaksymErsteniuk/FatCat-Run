using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ProcessingPlayerData
{
    public delegate void ShowMessage(string message);
    private static ShowMessage _showMessage;
    private static string pathToSaveFile;
    public static void SetDelegateShowMessage(ShowMessage showMessage)
    {
        _showMessage = showMessage;
    }
    private static object[] GlobalVar()
    {
        pathToSaveFile = Application.persistentDataPath + "/player.data";
        // BinaryFormatter - for formatting binary files
        return new object[2] { new BinaryFormatter(), pathToSaveFile};
    }

    public static void CreateDataFile(PlayerData PlayerDataInst)
    {
        BinaryFormatter formatter = (BinaryFormatter)GlobalVar()[0];
        string path = (string)GlobalVar()[1];
        // using - like "with()" in python, FileStream(path, FileMode) - opening file,
        // FileMode - in what mode must be open file (write, read, append, etc.)
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            // Serializetion - its from object(in our case - PlayerData class) to binary
            formatter.Serialize(fs, PlayerDataInst);
        }
    }
    public static PlayerData GetDataFile()
    {
        BinaryFormatter formatter = (BinaryFormatter)GlobalVar()[0];
        string path = (string)GlobalVar()[1];
        
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            // (typeData) convert to typeData type, e.x: (int)5.43 => 5
            // Deserializetion - its from binary to object
            return (PlayerData)formatter.Deserialize(fs);
        }
    }
    public static void UpdateFile(int newValueJunk=0, int newValueHealthy=0, int coins=0)
    {
        // get old value
        PlayerData PlayerDataInst = GetDataFile();
        // old value + new value = all
        PlayerDataInst.levels++;
        PlayerDataInst.coins += coins;
        PlayerDataInst.junkfood += newValueJunk;
        PlayerDataInst.healthyfood += newValueHealthy;
        CreateDataFile(PlayerDataInst);
    }
    public static void ChangeMusicVolume(float num)
    {
        PlayerData pd = GetDataFile();
        pd.volumeOfMusic = num;
        CreateDataFile(pd);
    }
    public static void ChangeEffectsVolume(float num)
    {
        PlayerData pd = GetDataFile();
        pd.volumeOfEffects = num;
        CreateDataFile(pd);
    }

    // int? mean that this var can be int or null
    public static void Trade(int num, string propertyName, out int? price, int coinsInReturn)
    {
        PlayerData pd = GetDataFile();
        var prop = typeof(PlayerData).GetField(propertyName);
        int value = (int)prop.GetValue(pd);
        if (num > value)
        {
            price = null;
            _showMessage("You don't have enough money");
        }
        else
        {
            int newValue = value - num;
            prop.SetValue(pd, newValue);
            int coins = coinsInReturn;
            price = coins;
            pd.coins += coins;
            CreateDataFile(pd);
        }
    }
    public static void ChangeCurrentColorCatIndex(int index)
    {
        PlayerData pd = GetDataFile();
        pd.currentCatIndex = index;
        CreateDataFile(pd);
    }
    public static void ChangeCurrentColorFloorIndex(int index)
    {
        PlayerData pd = GetDataFile();
        pd.currentMaterialFloorIndex = index;
        CreateDataFile(pd);
    }
    public static void BuyCatColor(int costColor, int indexOfColor)
    {
        PlayerData pd = ProcessingPlayerData.GetDataFile();
        if (costColor > pd.coins)
        {
            _showMessage("You don't have enough money");
        }
        else
        {
            pd.coins -= costColor;
            pd.avaibleCat.Add(indexOfColor);
            //pd.avaibleCat.Clear();
            //pd.avaibleCat.Add(0);
            CreateDataFile(pd);
        }
    }
    public static void BuyFloorColor(int costColor, int indexOfColor)
    {
        PlayerData pd = ProcessingPlayerData.GetDataFile();
        if (costColor > pd.coins)
        {
            _showMessage("You don't have enough money");
        }
        else
        {
            pd.coins -= costColor;
            pd.avaibleMaterialFloor.Add(indexOfColor);
            //pd.avaibleMaterialFloor.Clear();
            //pd.avaibleMaterialFloor.Add(0);
            CreateDataFile(pd);
        }
    }
}