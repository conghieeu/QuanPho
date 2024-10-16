using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityData
{
    public string ID;
    public string Name;
    public string ParentID;
    public TypeID TypeID;
    public Vector3 Position;
    public Quaternion Rotation;

    public EntityData()
    {
        ID = "";
        Name = "";
        ParentID = "";
        Position = Vector3.zero;
        TypeID = TypeID.None;
        Rotation = Quaternion.identity;
    }
}

[Serializable]
public class ItemData
{
    public EntityData EntityData;
    public float Price;

    public ItemData()
    {
        Price = 0;
    }
}

[Serializable]
public class CustomerData
{
    public EntityData EntityData;
    public float TotalPay;
    public bool IsFinishMission; // co hoan thanh nhiem vu chua

    public CustomerData()
    {
        TotalPay = 0;
        IsFinishMission = false;
    }
}

[Serializable]
public class SettingData
{
    public bool IsFullScreen;
    public int QualityIndex;
    public float MasterVolume;
    public int CurrentResolutionIndex;

    public SettingData()
    {
        IsFullScreen = false;
        QualityIndex = 0;
        MasterVolume = 0;
        CurrentResolutionIndex = 0;
    }
}

[Serializable]
public class CharacterData
{
    public EntityData EntityData;
    public float CurrentMoney;
    public int RatingPoints;
    public Vector3 Position;
    public Quaternion Rotation;

    public CharacterData()
    {
        CurrentMoney = 0;
        RatingPoints = 0;
        Position = Vector3.zero;
        Rotation = Quaternion.identity;
    }
}

[Serializable]
public class GamePlayData
{
    public bool IsInitialized;
    public CharacterData characterData;
    public List<CustomerData> CustomersData;
    public List<ItemData> ItemsData;

    public GamePlayData()
    {
        IsInitialized = false;
        characterData = new CharacterData();
        CustomersData = new List<CustomerData>();
        ItemsData = new List<ItemData>();
    }
}

[Serializable]
public class PlayerData
{
    public SettingData SettingData;
    public string UserName;
    public float TimePlay;

    public PlayerData()
    {
        SettingData = new SettingData();
        UserName = "NoName";
        TimePlay = 0;
    }
}

[Serializable]
public class GameData
{
    public PlayerData PlayerData;
    public GamePlayData GamePlayData;

    public GameData()
    {
        PlayerData = new PlayerData();
        GamePlayData = new GamePlayData();
    }
}