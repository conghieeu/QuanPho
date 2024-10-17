using System;
using System.Collections.Generic;
using System.Linq;
using QFSW.QC;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace QuanPho
{
    /// <summary> Quảng lý khi nào tải, khi nào load , load cái nào và không load cái nào </summary>
    public class SaveLoadManager : MonoBehaviour
    {
        public bool IsDataLoaded;
        public SaveGame SaveGame;

        public UnityAction OnSaveData;
        public UnityAction<GameData> OnSetData;
        public UnityAction OnDataLoad;

        public GameData GameData => SaveGame.GameData;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            if (SaveGame.IsSaveFileExists())
            {
                SetData();
                LoadData();
            }
            else
            {
                LoadNewGameData();
            }

            SceneManager.sceneLoaded += OnLoadScene;
        }

        void Init()
        {
            SaveGame = GetComponent<SaveGame>();
        }

        /// <summary> Tạo ra lại item đã lưu và gáng giá trị Data </summary>
        private void SetData()
        {
            ItemPooler itemPooler = FindFirstObjectByType<ItemPooler>();
            CustomerPooler customerPooler = FindFirstObjectByType<CustomerPooler>();

            SaveGame.SetGameDataByLocalData();
            GamePlayData gamePlayData = GameData.GamePlayData;
            List<ISaveData> allSaveData = AllISaveData();

            if (gamePlayData.IsInitialized == false) return;

            foreach (ISaveData iSaveData in allSaveData)
            {
                // Set item Data
                foreach (ItemData itemData in gamePlayData.ItemsData)
                {
                    itemPooler.ReloadItemByItemData(itemData);
                }

                // set customer data
                foreach (var customerData in gamePlayData.CustomersData)
                {
                    customerPooler.ReloadItemByItemData(customerData);
                }

                // set character data 
                iSaveData.SetData(gamePlayData.CharacterData);
            }
        }

        private void OnApplicationQuit()
        {
            SaveGameData();
        }

        private void StartNewGamePlay()
        {
            SaveGame.GameData.GamePlayData = new();
        }

        private void LoadData()
        {
            foreach (var entity in AllISaveData())
            {
                entity.LoadData();
            }

            IsDataLoaded = true;
        }

        private void LoadNewGameData()
        {
            SaveGame.SetNewGameData();
            LoadData();
        }

        [Command]
        private void SaveGameData()
        {
            SaveGame.GameData.GamePlayData = GetGamePlayData();
            SaveGame.SaveGameDataToLocal();
        }

        private GamePlayData GetGamePlayData()
        {
            GamePlayData gamePlayData = new GamePlayData();
            List<ISaveData> allSaveData = AllISaveData();

            // Get Item Data
            List<ItemData> itemsData = new();
            List<CustomerData> customersData = new();
            foreach (ISaveData data in allSaveData)
            {
                if (data.GetData<ItemData>() is ItemData itemData && itemData.EntityData.ID != "")
                {
                    itemsData.Add(itemData);
                }

                if (data.GetData<CustomerData>() is CustomerData customerData && customerData.EntityData.ID != "")
                {
                    customersData.Add(customerData);
                }

                if (data.GetData<CharacterData>() is CharacterData characterData && characterData.EntityData.ID != "")
                {
                    gamePlayData.CharacterData = characterData;
                }
            }

            // Get Game Play Data
            gamePlayData.ItemsData = itemsData;
            gamePlayData.CustomersData = customersData;
            gamePlayData.IsInitialized = true;

            return gamePlayData;
        }

        private List<ISaveData> AllISaveData()
        {
            List<ISaveData> allSaveData = new List<ISaveData>();
            allSaveData.AddRange(FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveData>());
            return allSaveData;
        }

        private void OnLoadScene(Scene scene = default, LoadSceneMode mode = default)
        {
            Init();
            if (IsDataLoaded)
            {
                // SaveGameData();
            }
        }
    }
}
