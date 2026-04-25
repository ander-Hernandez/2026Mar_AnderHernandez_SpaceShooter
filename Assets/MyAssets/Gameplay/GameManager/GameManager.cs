using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject restartMenu;

    private static GameManager managerInstance;

    private void Awake()
    {
        if (managerInstance == null)
        {
            managerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        restartMenu.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public static void EnableRestartMenu()
    {
        if (managerInstance == null)
            return;

        managerInstance.restartMenu.SetActive(true);
    }


    public static void LoadMenu() {
        if (managerInstance == null)
            return;
        SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
    }
}