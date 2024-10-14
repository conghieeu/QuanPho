using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityData
{
    public string ID;
    public string Name;
    public string ParentID;
    public EntityType TypeID;
    public Vector3 Position;
    public Quaternion Rotation;

    public EntityData()
    {
        ID = "";
        Name = "";
        ParentID = "";
        Position = Vector3.zero;
        TypeID = EntityType.None;
        Rotation = Quaternion.identity;
    }
}

[Serializable]
public class ItemData : EntityData
{
    public float Price;

    public ItemData()
    {
        Price = 0;
    }
}

[Serializable]
public class CustomerData : EntityData
{
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
public class PlayerData
{
    public float CurrentMoney;
    public int RatingPoints;
    public Vector3 Position;
    public Quaternion Rotation;

    public PlayerData()
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
    public PlayerData PlayerData;
    public List<CustomerData> CustomersData;
    public List<ItemData> ItemsData;
}

[Serializable]
public class PlayerProfile
{
    public SettingData SettingData;
    public string UserName;
    public float TimePlay;
}

[Serializable]
public class GameData
{
    public PlayerProfile PlayerProfile;
    public GamePlayData GamePlayData;
}