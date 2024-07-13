public class Singleton<T> : UnityEngine.MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
            Instance = (T)this;
        else Destroy(this);
    }
}
