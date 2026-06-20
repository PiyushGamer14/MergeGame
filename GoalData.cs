using UnityEngine;

[System.Serializable]
public class GoalData 
{
    public BubbleData TargetBubble;

    public int RequiredCount;

    [HideInInspector]
    public int CurrentCount;
}
