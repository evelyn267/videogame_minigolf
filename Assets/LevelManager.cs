using UnityEngine;
using TMPro;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameOver();
        }
    }
}