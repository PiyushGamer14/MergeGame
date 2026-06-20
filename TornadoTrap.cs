using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoTrap : MonoBehaviour
{
    HashSet<Rigidbody2D> trapped = new HashSet<Rigidbody2D>();
    public float PullForce = 10f;
    //Dictionary<Rigidbody2D, float>

    private void Update()
    {
        trapped.RemoveWhere(item => item == null);
        transform.Rotate(0, 0, 250 * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Bubble bubble = other.GetComponent<Bubble>();

        if (bubble == null)
            return;

        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        if (rb == null)
            return;

        if (bubble.IsMerging)
            return;
        if (trapped.Contains(rb))
            return;

        trapped.Add(rb);

        StartCoroutine(TornadoRoutine(rb));
    }

    IEnumerator ThrowBubble(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(2f);

        Vector2 dir = Random.insideUnitCircle.normalized;

        rb.AddForce(dir * 10f, ForceMode2D.Impulse);
    }

    IEnumerator TornadoRoutine(Rigidbody2D rb)
    {
        float timer = 0f;

        while (timer < 2f)
        {
            if (rb == null)
            {
                trapped.Remove(rb);
                yield break;
            }

            timer += Time.deltaTime;

            Vector2 pullDir =
                ((Vector2)transform.position -
                 rb.position).normalized;

            Vector2 spinDir =
                new Vector2(
                    -pullDir.y,
                     pullDir.x);

            rb.AddForce(
                (pullDir + spinDir) *
                PullForce,
                ForceMode2D.Force);

            yield return null;
        }

        if (rb == null)
        {
            trapped.Remove(rb);
            yield break;
        }

        Vector2 launch =
            Random.insideUnitCircle.normalized;

        rb.AddForce(
            launch * 10f,
            ForceMode2D.Impulse);

        trapped.Remove(rb);
    }
}
