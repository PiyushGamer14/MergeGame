using UnityEngine;

public class MagnetTrap : MonoBehaviour
{
    public float PullForce = 10f;

    private void OnTriggerStay2D(
        Collider2D other)
    {
        Bubble bubble =
            other.GetComponent<Bubble>();

        if (bubble == null)
            return;

        Rigidbody2D rb =
            bubble.GetComponent<Rigidbody2D>();

        Vector2 dir =
            transform.position -
            bubble.transform.position;

        rb.AddForce(
            dir.normalized *
            PullForce);
    }
}
