using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuPanel = null, DefeatMenuPanel = null;

    private void Awake()
    {
        Player.defeated += Defeat;    
    }

    private void OnDisable()
    {
        Player.defeated -= Defeat;
    }
    //not clear name of function
    //I assume that it is used for two buttons (as there are 2 if inside
    //separate the logic in separate functions for separate buttons
    public void PauseBtn()
    {
        if (!PauseMenuPanel.activeSelf)
        {
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (PauseMenuPanel.activeSelf)//it is redundant
        {
            PauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    /*private void Update()
    {
        if (GameManager.gameManagerInstance._player.activeSelf == false)
        {
            GameManager.gameManagerInstance.SaveRecord();
            Time.timeScale = 0.5f;
            Invoke("OpenDefeatMenu", 1.5f);
        }
    }*/

    private void Defeat()
    {
        //may be subscribe gamemanager separatelly to event in Player?
        GameManager.gameManagerInstance.SaveRecord();
        Time.timeScale = 0.5f;
        Invoke("OpenDefeatMenu", 1.5f);
    }

    private void OpenDefeatMenu()
    {
        //Open defeat menu
        GameManager.gameManagerInstance.SaveRecord();
        DefeatMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        GameManager.gameManagerInstance.SaveRecord();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        GameManager.gameManagerInstance.SaveRecord();
        SceneManager.LoadScene("Menu");
    }
}
