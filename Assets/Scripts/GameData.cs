using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [Header("Player")]
    public string previousSceneName;

    private void Awake()
    {
        //Singleton stuff
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
