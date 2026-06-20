using System.Collections;
using UnityEngine;
public class FlyingCage : MonoBehaviour
{
    public float MoveSpeed = 2f;

    /*public Transform PointA;
    public Transform PointB;*/

    public Transform[] Waypoints;

    int currentWaypoint;

    Bubble capturedBubble;
    bool stunned;
    bool justCaptured;

    bool movingToB;

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        /*Transform target =
            movingToB
            ? PointB
            : PointA;

        transform.position =
            Vector3.MoveTowards(
                transform.position,
                target.position,
                MoveSpeed *
                Time.deltaTime);

        if (Vector3.Distance(
            transform.position,
            target.position) < 0.1f)
        {
            movingToB =
                !movingToB;
        }*/

        if (Waypoints.Length == 0)
            return;

        Transform target = Waypoints[currentWaypoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypoint++;

            if (currentWaypoint >= Waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (stunned)
            return;

        if (justCaptured)
            return;
        Bubble bubble = other.GetComponentInParent<Bubble>();

        if (bubble == null)
            return;
        if (bubble.IsFrozen)
            return;

        if (bubble == capturedBubble)
            return;

        // Cage already has a prisoner
        if (capturedBubble != null)
        {
            Debug.Log("CAGE STUNNED");

            StartCoroutine(StunRoutine());

            return;
        }

        CaptureBubble(bubble);
    }

    /* IEnumerator StunRoutine()
     {

     }*/

    IEnumerator StunRoutine()
    {
        stunned = true;
        GetComponent<SpriteRenderer>().color = Color.yellow;
        float oldSpeed = MoveSpeed;

        MoveSpeed = 0;

        if (capturedBubble != null)
        {
            Rigidbody2D rb = capturedBubble.GetComponent<Rigidbody2D>();

            capturedBubble.transform.SetParent(null);
            capturedBubble.IsCaptured = false;

            rb.bodyType = RigidbodyType2D.Dynamic;

            rb.AddForce(Random.insideUnitCircle.normalized * 3f, ForceMode2D.Impulse);

            capturedBubble = null;
        }

        yield return new WaitForSeconds(3f);

        MoveSpeed = oldSpeed;

        stunned = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void CaptureBubble(Bubble bubble)
    {
        justCaptured = true;
        bubble.IsCaptured = true;
        capturedBubble = bubble;

        bubble.transform.SetParent(transform);

        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Static;
        StartCoroutine(CaptureCooldown());
        GameManager.Instance.CheckFrozenLoseCondition();
    }

    IEnumerator CaptureCooldown()
    {
        yield return new WaitForSeconds(0.5f);

        justCaptured = false;
    }

    public void Stun()
    {
        Debug.Log("STUN CALLED");
        if (capturedBubble != null)
        {
            Rigidbody2D rb =
                capturedBubble
                .GetComponent<Rigidbody2D>();

            rb.bodyType =
                RigidbodyType2D.Dynamic;

            capturedBubble.transform
                .SetParent(null);

            capturedBubble = null;
        }
    }

    /*private void OnCollisionEnter2D(
    Collision2D collision)
    {
        Bubble bubble =
            collision.collider.GetComponent<Bubble>();

        if (bubble == null)
            return;

        Stun();
    }*/

    public void TryCaptureBubble(
    Bubble bubble)
    {
        if (capturedBubble != null)
            return;

        CaptureBubble(bubble);
    }
}
