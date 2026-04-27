using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject ControlUI;
    private void Start()
    {
        AudioManager.PlayMenuMusic();
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleControls()
    {
        ControlUI.SetActive(!ControlUI.activeInHierarchy);
    }
}