using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Merge/Level Data")]
public class LevelData : ScriptableObject
{
    public BubbleData[] StartingBubbles;

    public int Moves;

    public GoalData[] Goals;

    //public BubbleData[] PossibleBubbles;
   // public int BubbleCount = 8;



}
