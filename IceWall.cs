using UnityEngine;

public class IceWall : MonoBehaviour
{
    private void OnCollisionEnter2D(
         Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
            return;

        Bubble bubble = collision.collider.GetComponent<Bubble>();

        if (bubble == null)
            return;

        bubble.Freeze();
    }
}
