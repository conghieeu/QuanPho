using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private bool _dontDestroyOnLoad;

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                if (instance == null)
                {
                    Debug.LogWarning($"Lỗi singleton: đối tượng {typeof(T).Name} không tồn tại");
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDontDestroyOnLoad(bool value)
    {
        _dontDestroyOnLoad = value;
        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
