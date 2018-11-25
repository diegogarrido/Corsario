using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public static class DataManager
{

    public static string saveName;

    public static string SaveName
    {
        get
        {
            return saveName;
        }

        set
        {
            saveName = value;
        }
    }

    public static void SaveInventory(PlayerInventory inv)
    {
        FileInfo f = new FileInfo(Application.persistentDataPath + "/" + saveName + "/PlayerInventory" + ".dat");
        f.Directory.Create();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + saveName + "/PlayerInventory" + ".dat", FileMode.OpenOrCreate);
        bf.Serialize(file, inv);
        file.Close();
    }

    public static PlayerInventory LoadInventory()
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveName + "/PlayerInventory" + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveName + "/PlayerInventory" + ".dat", FileMode.Open);
            PlayerInventory inv = (PlayerInventory)bf.Deserialize(file);
            file.Close();
            return inv;
        }
        else
        {
            return null;
        }
    }

    public static void SaveData(GameData data)
    {
        FileInfo f = new FileInfo(Application.persistentDataPath + "/" + saveName + "/PlayerData" + ".dat");
        f.Directory.Create();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + saveName + "/PlayerData" + ".dat", FileMode.OpenOrCreate);
        bf.Serialize(file, data);
        file.Close();
    }

    public static GameData LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveName + "/PlayerData" + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveName + "/PlayerData" + ".dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static void SaveSquare(MapSquare square)
    {
        FileInfo f = new FileInfo(Application.persistentDataPath + "/" + saveName + "/Map/" + square.CoordX + "-" + square.CoordZ + ".dat");
        f.Directory.Create();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + saveName + "/Map/" + square.CoordX + "-" + square.CoordZ + ".dat", FileMode.OpenOrCreate);
        bf.Serialize(file, square);
        file.Close();
    }

    public static MapSquare LoadSquare(int x, int z)
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveName + "/Map/" + x + "-" + z + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveName + "/Map/" + x + "-" + z + ".dat", FileMode.Open);
            MapSquare square = (MapSquare)bf.Deserialize(file);
            file.Close();
            return square;
        }
        else
        {
            return null;
        }
    }

    public static void SaveSquareData(SquareData data, int x, int z)
    {
        FileInfo f = new FileInfo(Application.persistentDataPath + "/" + saveName + "/Map/" + x + "-" + z + " Data.dat");
        f.Directory.Create();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + saveName + "/Map/" + x + "-" + z + " Data.dat", FileMode.OpenOrCreate);
        bf.Serialize(file, data);
        file.Close();
    }

    public static SquareData LoadSquareData(int x, int z)
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveName + "/Map/" + x + "-" + z + " Data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveName + "/Map/" + x + "-" + z + " Data.dat", FileMode.Open);
            SquareData data = (SquareData)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static bool SquareCreated(int x, int z)
    {
        return File.Exists(Application.persistentDataPath + "/" + saveName + "/Map/" + x + "-" + z + ".dat");
    }

    public static bool MapCreated()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + saveName + "/Map/");
    }

    public static bool SaveCreated(string name)
    {
        return Directory.Exists(Application.persistentDataPath + "/" + name);
    }


}