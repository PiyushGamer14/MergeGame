using UnityEngine;
using System.Collections;
public class PoisonWall : MonoBehaviour
{
    private void OnCollisionEnter2D(
        Collision2D collision)
    {
        Bubble bubble =
            collision.collider
            .GetComponent<Bubble>();

        if (bubble == null)
            return;

        StartCoroutine(
            RespawnBubble(
                bubble));
    }

    IEnumerator RespawnBubble(
        Bubble bubble)
    {
        Vector3 pos =
            bubble.transform.position;

        BubbleData data =
            bubble.Data;

        Destroy(
            bubble.gameObject);

        yield return new WaitForSeconds(3);

        GameObject obj =
            Instantiate(
                MergeManager.Instance
                .BubblePrefab,
                pos,
                Quaternion.identity);

        obj.GetComponent<Bubble>()
           .SetData(data);
    }
}
