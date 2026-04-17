using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI strokeUI;
    [Space(10)]
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private TextMeshProUGUI levelCompletedStrokeUI;
    [Space(10)]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject tutorialPanel;

    [Header("Attributes")]
    [SerializeField] private int maxStrokes = 3;

    private int strokes;
    [HideInInspector] public bool outOfStrokes;
    [HideInInspector] public bool levelCompleted;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        strokes = 0;
        UpdateStrokeUI();
        levelCompleteUI.SetActive(false);
        gameOverUI.SetActive(false);
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
    }

    public void IncreaseStroke()
    {
        if (outOfStrokes || levelCompleted) return;
        strokes++;
        UpdateStrokeUI();
        if (strokes >= maxStrokes)
        {
            outOfStrokes = true;
        }
    }

    public void LevelComplete()
    {
        if (levelCompleted) return;
        levelCompleted = true;
        levelCompletedStrokeUI.text = strokes > 1
            ? "You holed it in " + strokes + " strokes"
            : "You got a hole in one!";
        levelCompleteUI.SetActive(true);
    }

    public void GameOver()
    {
        if (gameOverUI.activeSelf) return;
        gameOverUI.SetActive(true);
    }

    private void UpdateStrokeUI()
    {
        strokeUI.text = strokes + "/" + maxStrokes;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameOver();
        }
        #endif
    }
}