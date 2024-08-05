//makes the object not destroyed between scene changes
public class DDOL : UnityEngine.MonoBehaviour { private void Awake() => DontDestroyOnLoad(gameObject); }