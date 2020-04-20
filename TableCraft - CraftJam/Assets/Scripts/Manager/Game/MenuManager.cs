using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button PlayBtn;
    public Button HowToBtn;
    public Button ExitBtn;

    public GameObject HowToObject;

    private LevelManager levelManager;

    void Start()
    {
        levelManager = App.instance.GetComponent<LevelManager>();

        PlayBtn.onClick.AddListener(() =>
        {
            levelManager.LoadLevel("Game");
        });

        HowToBtn.onClick.AddListener(() =>
        {
            HowToObject.SetActive(true);
        });

        ExitBtn.onClick.AddListener(() =>
        {
            levelManager.CloseApp();
        });   
    }

    void Update()
    {
        if (Input.anyKey && HowToObject.activeInHierarchy)
        {
            HowToObject.SetActive(false);
        }    
    }
}
