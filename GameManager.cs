using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject MergeParticlePrefab;
    public GameObject GoalItemPrefab;
    public int Moves = 20;

    public TMP_Text MovesText;

    public GoalData[] Goals;

    public TMP_Text GoalText;

    public GameObject WinPanel;
    public GameObject LosePanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        UpdateUI();

    }

    public void UseMove()
    {
        Moves--;

        Debug.Log("MOVE USED. Remaining = " + Moves);

        UpdateUI();

        if (Moves <= 0)
        {
            Debug.Log("OUT OF MOVES");

            LoseGame();
        }
    }

    void UpdateUI()
    {
        MovesText.text =
            "Moves: " + Moves;
    }

    void LoseGame()
    {
        Debug.Log("LOSE GAME CALLED");
        LosePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void SetupGoals(GoalData[] goals)
    {
        Goals = goals;

        foreach (var goal in Goals)
        {
            goal.CurrentCount = 0;
        }
        GoalUIManager.Instance.BuildGoals(goals);
        UpdateGoalUI();
    }
    void UpdateGoalUI()
    {
        /* string text = "";

         foreach (var goal in Goals)
         {
             text += goal.TargetBubble.BubbleName + " " + goal.CurrentCount + "/" + goal.RequiredCount + "\n";
         }

         GoalText.text = text;*/

        if (GoalUIManager.Instance != null)
        {
            GoalUIManager.Instance.Refresh();
        }
    }

    public void AddGoalProgress(BubbleData bubble)
    {
        Debug.Log("AddGoalProgress Called For = " + bubble.name);

        foreach (var goal in Goals)
        {
            Debug.Log("Goal Target = " + goal.TargetBubble.name);

            if (goal.TargetBubble == bubble)
            {
                Debug.Log("GOAL MATCH FOUND");

                goal.CurrentCount++;
                //GoalUIManager.Instance.Refresh();
                UpdateGoalUI();
                Debug.Log("Current Count = " + goal.CurrentCount);
                Debug.Log("Required Count = " + goal.RequiredCount);

                UpdateGoalUI();

                CheckWin();

                return;
            }
        }

        Debug.Log("NO GOAL MATCH FOUND");
    }

    void CheckWin()
    {
        /*foreach (var goal in Goals)
        {
            Debug.Log(goal.TargetBubble.name + " : " + goal.CurrentCount + "/" +
            goal.RequiredCount);
            if (goal.CurrentCount < goal.RequiredCount)
            {
                return;
            }
        }

        WinGame();*/

        Debug.Log("CHECKING WIN");

        foreach (var goal in Goals)
        {
            Debug.Log(goal.TargetBubble.name + " " + goal.CurrentCount + "/" + goal.RequiredCount);

            if (goal.CurrentCount < goal.RequiredCount)
            {
                Debug.Log("NOT COMPLETE");

                return;
            }
        }

        Debug.Log("ALL GOALS COMPLETE");

        WinGame();
    }

    void WinGame()
    {
        Debug.Log("WIN GAME CALLED");
        WinPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void CheckFrozenLoseCondition()
    {
        if (WinPanel.activeSelf)
            return;

        Bubble[] bubbles = FindObjectsByType<Bubble>(FindObjectsSortMode.None);

        if (bubbles.Length == 0)
            return;

        int blockedCount = 0;

        foreach (Bubble bubble in bubbles)
        {
            if (bubble.IsFrozen ||
                bubble.IsCaptured)
            {
                blockedCount++;
            }
        }

        Debug.Log("Blocked Count = " + blockedCount + " Total = " + bubbles.Length);

        if (blockedCount == bubbles.Length)
        {
            Debug.Log("ALL BUBBLES BLOCKED");

            LoseGame();
        }
    }

    public void CheckNoPossibleMoves()
    {
        Debug.Log("Checking Possible Moves");

        if (WinPanel.activeSelf)
            return;

        Bubble[] bubbles = FindObjectsByType<Bubble>(FindObjectsSortMode.None);

        for (int i = 0; i < bubbles.Length; i++)
        {
            if (bubbles[i].IsFrozen ||
                bubbles[i].IsCaptured)
                continue;

            for (int j = i + 1; j < bubbles.Length; j++)
            {
                if (bubbles[j].IsFrozen ||
                    bubbles[j].IsCaptured)
                    continue;

                if (bubbles[i].Data ==
                    bubbles[j].Data)
                {
                    Debug.Log(
                        "Possible Move Found: "
                        + bubbles[i].Data.name);

                    return;
                }
            }
        }

        Debug.Log("NO POSSIBLE MOVES");

        LoseGame();
    }

    public void DelayedMoveCheck()
    {
        StartCoroutine(DelayedCheckRoutine());
    }

    IEnumerator DelayedCheckRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        CheckNoPossibleMoves();
    }
}
