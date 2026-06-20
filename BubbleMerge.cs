using UnityEngine;

public class BubbleMerge : MonoBehaviour
{
    Bubble bubble;

    bool merged;


    private void Awake()
    {
        bubble = GetComponent<Bubble>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        Bubble other = collision.collider.GetComponent<Bubble>();

        if (other == null)
            return;

        if (bubble.IsFrozen)
        {
            bubble.Unfreeze();
            return;
        }

        if (other.IsFrozen)
        {
            other.HitFrozen();

            return;
        }

        if (bubble.IsMerging || other.IsMerging)
            return;

        if (other.Data != bubble.Data)
            return;

        if (collision.relativeVelocity.magnitude < 2f)
            return;

        bubble.IsMerging = true;
        other.IsMerging = true;

        MergeManager.Instance.Merge(bubble, other);
    }
}
