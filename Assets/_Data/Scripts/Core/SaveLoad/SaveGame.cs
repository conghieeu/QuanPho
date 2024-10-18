using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaveGame : Singleton<SaveGame>
{
    [SerializeField] string saveName = "/gameData.save";
    [SerializeField] string filePath;
    [SerializeField] bool serialize;
    [SerializeField] bool usingXML;
    [SerializeField] bool encrypt;
    [SerializeField] GameData gameData = new GameData();

    public GameData GameData
    {
        get
        {
            return gameData;
        }
        set
        {
            gameData = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        SetDontDestroyOnLoad(true);
        filePath = Application.persistentDataPath + saveName;
    } 

    public void SetGameDataByLocalData()
    {
        GameData = GetLocalData();
    }

    public GameData GetLocalData()
    {
        if (IsSaveFileExists())
        {
            string stringData = File.ReadAllText(filePath);
            Debug.Log("Game data loaded from: " + filePath);
            return Deserialized(stringData);
        }
        else
        {
            Debug.Log("Save file not found in: " + filePath);
            return null;
        }
    }

    public void SaveGameDataToLocal()
    {
        File.WriteAllText(filePath, SerializeAndEncrypt(GameData));
        Debug.Log("Game data saved to: " + filePath);
    }

    public bool IsSaveFileExists()
    {
        if(File.Exists(filePath))
        {
            GameData = new GameData();
            return true;
        }
        return false;
    }

    /// <summary> Let's first serialize and encrypt.... </summary>
    private string SerializeAndEncrypt(GameData gameData)
    {
        string stringData = "";

        if (serialize)
        {
            if (usingXML)
                stringData = Utils.SerializeXML<GameData>(gameData);
            else
                stringData = JsonUtility.ToJson(gameData);
        }

        if (encrypt)
        {
            stringData = Utils.EncryptAES(stringData);
        }

        return stringData;
    }

    /// <summary> Now let's de-serialize and de-encrypt.... </summary>
    private GameData Deserialized(string stringData)
    {
        // giải mã hoá
        if (encrypt)
        {
            stringData = Utils.DecryptAES(stringData);
        }

        GameData gameData = new GameData();

        // đọc tuần tự hoá json hoặc xml
        if (serialize)
        {
            if (usingXML)
                gameData = Utils.DeserializeXML<GameData>(stringData);
            else
                gameData = JsonUtility.FromJson<GameData>(stringData);
        }
        return gameData;
    }
}

public static class Utils
{
    public static string SerializeXML<T>(System.Object inputData)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (var sww = new StringWriter())
        {
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                serializer.Serialize(writer, inputData);
                return sww.ToString();
            }
        }
    }

    public static T DeserializeXML<T>(string data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (var sww = new StringReader(data))
        {
            using (XmlReader reader = XmlReader.Create(sww))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }

    static byte[] ivBytes = new byte[16]; // Generate the iv randomly and send it along with the data, to later parse out
    static byte[] keyBytes = new byte[16]; // Generate the key using a deterministic algorithm rather than storing here as a variable

    static void GenerateIVBytes()
    {
        System.Random rnd = new System.Random();
        rnd.NextBytes(ivBytes);
    }

    const string nameOfGame = "HieuDev";
    static void GenerateKeyBytes()
    {
        int sum = 0;
        foreach (char curChar in nameOfGame)
            sum += curChar;

        System.Random rnd = new System.Random(sum);
        rnd.NextBytes(keyBytes);
    }

    public static string EncryptAES(string data)
    {
        GenerateIVBytes();
        GenerateKeyBytes();

        SymmetricAlgorithm algorithm = Aes.Create();
        ICryptoTransform transform = algorithm.CreateEncryptor(keyBytes, ivBytes);
        byte[] inputBuffer = Encoding.Unicode.GetBytes(data);
        byte[] outputBuffer = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

        string ivString = Encoding.Unicode.GetString(ivBytes);
        string encryptedString = Convert.ToBase64String(outputBuffer);

        return ivString + encryptedString;
    }

    public static string DecryptAES(this string text)
    {
        GenerateIVBytes();
        GenerateKeyBytes();

        int endOfIVBytes = ivBytes.Length / 2;  // Half length because unicode characters are 64-bit width

        string ivString = text.Substring(0, endOfIVBytes);
        byte[] extractedivBytes = Encoding.Unicode.GetBytes(ivString);

        string encryptedString = text.Substring(endOfIVBytes);

        SymmetricAlgorithm algorithm = Aes.Create();
        ICryptoTransform transform = algorithm.CreateDecryptor(keyBytes, extractedivBytes);
        byte[] inputBuffer = Convert.FromBase64String(encryptedString);
        byte[] outputBuffer = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

        string decryptedString = Encoding.Unicode.GetString(outputBuffer);

        return decryptedString;
    }
}
