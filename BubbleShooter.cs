using UnityEngine;

public class BubbleShooter : MonoBehaviour
{
    private Bubble selectedBubble;

    public TrajectoryDrawer Trajectory;

    private Vector2 dragStart;
    private Vector2 dragEnd;

    public float ShootForce = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectBubble();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateAim();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ShootBubble();
        }
    }

    void SelectBubble()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null)
            return;

        Bubble bubble = hit.collider.GetComponent<Bubble>();

        if (bubble == null)
            return;

        Trajectory.Hide();

        if (selectedBubble != null)
        {
            selectedBubble.Deselect();
        }

        selectedBubble = bubble;
        selectedBubble.StopMovement();

        selectedBubble.Select();

        dragStart = mousePos;
    }

    void UpdateAim()
    {
        if (selectedBubble == null)
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        /*Vector2 force = (dragStart - mousePos) * ShootForce;

        Trajectory.ShowTrajectory(selectedBubble.transform.position, force * 0.1f);*/
        Vector2 force = (dragStart - mousePos);

        float distance = Mathf.Clamp(force.magnitude, 0, 3f);

        force = force.normalized * distance;

        Trajectory.ShowTrajectory(selectedBubble.transform.position, force);
    }

    void ShootBubble()
    {
        if (selectedBubble == null)
            return;

        dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = dragStart - dragEnd;

        float distance = Mathf.Clamp(dir.magnitude, 0, 3f);

        dir = dir.normalized * distance;

        Rigidbody2D rb = selectedBubble.GetComponent<Rigidbody2D>();

        rb.AddForce(dir * ShootForce, ForceMode2D.Impulse);   //shooting bubble
        GameManager.Instance.UseMove();
        AudioManager.Instance.Play( AudioManager.Instance.ShootSound);

        Trajectory.Hide();
        selectedBubble.Deselect();
        selectedBubble = null;
    }
}
