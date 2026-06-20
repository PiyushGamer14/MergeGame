using UnityEngine;



[CreateAssetMenu(fileName = "BubbleData", menuName = "Bubble Merge/Bubble Data")]
public class BubbleData : ScriptableObject
{
    public string BubbleName;

    public Sprite BubbleSprite;

    public BubbleData NextLevelBubble;

    public int ScoreValue;

    public int MergeLevel; //for camera shake
    public bool IsFinalBubble;
}
