using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GoalUIItem : MonoBehaviour
{
    /*public Image Icon;

    public TMP_Text Count;

    public void Setup(Sprite sprite, int current, int required)
    {
        Icon.sprite = sprite;

        Count.text = current + "/" + required;
    }*/


    public Image Icon;

    public TMP_Text CountText;

    GoalData goal;

    public void Setup(GoalData data)
    {
        goal = data;

        Icon.sprite =
            goal.TargetBubble.BubbleSprite;

        Refresh();
    }

    public void Refresh()
    {
        Debug.Log("Goal = " + goal);
        Debug.Log("CountText = " + CountText);

        CountText.text =
            goal.CurrentCount +
            "/" +
            goal.RequiredCount;
    }
}
