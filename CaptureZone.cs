using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    public FlyingCage Cage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bubble bubble = other.GetComponent<Bubble>();

        if (bubble == null)
            return;

        Cage.TryCaptureBubble(bubble);
    }
}
