using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public bool IsGamePaused { get; private set; }

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] GameObject virtualCam;
    [SerializeField] GameObject initialMessage;

    private Player player;
    private bool start;
    private bool startFlag;
    private bool endFlag;

    // Use this for initialization
    void Awake () 
	{
        player = FindObjectOfType<Player>();
		virtualCam.SetActive(true);
	}

    private void Update()
    {
        if (!start || (player.Died && !endFlag))
        {
            if (!startFlag)
            {
                initialMessage.SetActive(true);
                startFlag = true;
                Time.timeScale = 0;
                IsGamePaused = true;
            }
            WaitForInput();
        }
        else
        {
            if (startFlag)
            {
                Time.timeScale = 1;
                IsGamePaused = false;
                startFlag = false;
            }
            if (endFlag) SceneManager.LoadScene("MainScene");
            PauseOnInput();
            GoBackToMenu();
        }
    }

    private void WaitForInput()
    {
        if(Input.anyKey)
        {
            start = true;
            if(player.Died) endFlag = true;
            initialMessage.SetActive(false);
        }
    }

    private void GoBackToMenu()
    {
        // If the game is paused and player presses Enter
        if (IsGamePaused && Input.GetButtonDown("Confirm"))
        {

            SceneManager.LoadScene("MainMenu");
        }
    }

    /// <summary>
    /// Checks if the player presses Escape and pauses or unpauses the game
    /// </summary>
    private void PauseOnInput()
    {
        // If player presses Escape
        if (Input.GetButtonDown("Start"))
        {
            // If the game is paused
            if (IsGamePaused)
            {
                // Resume the game
                Resume();
            }
            // If the game is unpaused
            else
            {
                // Pause the game
                Pause();
            }
        }
    }

    /// <summary>
    /// Method that resumes the game
    /// </summary>
    private void Resume()
    {
        // Hide the pause menu
        ShowPauseMenu(false);
        // Set the timeScale property of Time to 1
        Time.timeScale = 1;
        IsGamePaused = false;
    }

    private void Pause()
    {
        // Show the pause menu
        ShowPauseMenu(true);
        // Set the timeScale property of Time to 0
        Time.timeScale = 0;
        IsGamePaused = true;
    }

    public void ShowPauseMenu(bool show)
    {
        // If we want to show the pause menu, enable it
        if (show) pauseMenu.SetActive(true);
        // Else, disable it
        else pauseMenu.SetActive(false);
    }
}
