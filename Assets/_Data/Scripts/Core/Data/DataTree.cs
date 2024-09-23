using System;
using System.Collections.Generic;
using UnityEngine; 

[Serializable]
public class ItemData
{
    [SerializeField] string id;
    [SerializeField] string parentId; 
    [SerializeField] float price;
    [SerializeField] EntityType typeID;
    [SerializeField] Vector3 position;
    [SerializeField] Quaternion rotation;
} 

[Serializable]
public class CustomerData
{
    [SerializeField] string id;
    [SerializeField] string name;
    [SerializeField] float totalPay;
    [SerializeField] bool isNotNeedBuy; 
    [SerializeField] EntityType typeID;
    [SerializeField] Vector3 position;
    [SerializeField] Quaternion rotation;
}

[Serializable]
public class SettingData
{
    [SerializeField] bool isFullScreen;
    [SerializeField] int qualityIndex;
    [SerializeField] float masterVolume;
    [SerializeField] int currentResolutionIndex;
    [SerializeField] Quaternion camRotation;
}

[Serializable]
public class PlayerData
{
    [SerializeField] float _currentMoney;
    [SerializeField] int _reputation;
    [SerializeField] Vector3 _position;
    [SerializeField] Quaternion _rotation;
}

[Serializable]
public class PlayData
{
    [SerializeField] bool isInitialized;
    [SerializeField] PlayerData playerData;
    [SerializeField] List<CustomerData> customersData; 
    [SerializeField] List<ItemData> itemsData;
}

[Serializable]
public class UserData
{
    [SerializeField] string userName;
    [SerializeField] float timePlay;
}

[Serializable]
public class GameData
{
    public UserData userData;
    public SettingData settingData;
    public PlayData playData;
}