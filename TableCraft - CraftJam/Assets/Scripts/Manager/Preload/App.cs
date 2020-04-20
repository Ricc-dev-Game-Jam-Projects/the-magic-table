using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    public static App instance;
    public string FirstScene = "Game";

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "_preload")
        {
            GameInitializer initializer = FindObjectOfType<GameInitializer>();

            if (initializer != null)
            {
                if (initializer.Done)
                {
                    SceneManager.LoadScene(FirstScene);
                }
            } else
            {
                SceneManager.LoadScene(FirstScene);
            }
        }
    }
}
