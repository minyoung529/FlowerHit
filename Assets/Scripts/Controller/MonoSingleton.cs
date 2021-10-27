using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool shuttingDown = false;
    private static object locker = new object();
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning("[Instance] Instace " + typeof(T) + " is already destroyed. Returning null.");
                return null;
            }
            lock (locker)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        DontDestroyOnLoad(Instance);
                    }
                }
                return instance;
            }
        }


    }
}