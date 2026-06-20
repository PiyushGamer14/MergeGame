using UnityEngine;
using System.Collections.Generic;
public class GoalUIManager : MonoBehaviour
{
    public GameObject GoalPrefab;

    public Transform Parent;

    List<GoalUIItem> items =
        new List<GoalUIItem>();

    public static GoalUIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void BuildGoals(
        GoalData[] goals)
    {
        foreach (var goal in goals)
        {
            GameObject obj =
                Instantiate(
                    GoalPrefab,
                    Parent);

            GoalUIItem item =
                obj.GetComponent<GoalUIItem>();

            item.Setup(goal);

            items.Add(item);
        }
    }

    public void Refresh()
    {
        foreach (var item in items)
        {
            item.Refresh();
        }
    }
}
