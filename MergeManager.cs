using System.Collections;
using UnityEngine;
public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance;

    public GameObject BubblePrefab;
    public GameObject MergeParticlePrefab;

    private void Awake()
    {
        Instance = this;
    }

    /* public void Merge(Bubble a, Bubble b)
     {
         Debug.Log("MERGE CALLED");
         Debug.Log(a.Data.name);
         Debug.Log(b.Data.name);
         if (a.Data != b.Data)
             return;

         if (a.Data.NextLevelBubble == null)
             return;

         Vector3 pos = (a.transform.position + b.transform.position) / 2f;

         BubbleData next = a.Data.NextLevelBubble;

         Destroy(a.gameObject);
         Destroy(b.gameObject);

         GameObject newBubble = Instantiate(BubblePrefab, pos, Quaternion.identity);

         newBubble.GetComponent<Bubble>().SetData(next);
     }*/

    public void Merge(Bubble a, Bubble b)
    {
        StartCoroutine(MergeRoutine(a, b));
    }

    IEnumerator MergeRoutine(Bubble a, Bubble b)
    {
        float duration = 0.1f;

        Vector3 startA = a.transform.position;

        Vector3 startB = b.transform.position;

        Vector3 center = (startA + startB) / 2f;

        float timer = 0;

        while (timer < duration)
        {
            if (a == null || b == null)
                yield break;

            timer += Time.deltaTime;

            float t = timer / duration;

            a.transform.position = Vector3.Lerp(startA, center, t);

            b.transform.position = Vector3.Lerp(startB, center, t);

            yield return null;
        }

        Debug.Log("A = " + a);
        Debug.Log("A Data = " + a.Data);

        if (a.Data == null)
        {
            Debug.LogError("Bubble Data is NULL");
            yield break;
        }
        /*
                if (a.Data.NextLevelBubble == null)
                {
                    // Debug.LogWarning("No next bubble assigned for " + a.Data.name);
                    Debug.LogError("THIS IS THE OLD BLOCK");
                    yield break;
                }*/

        Debug.Log("Next Level Bubble = " + a.Data.NextLevelBubble);

        if (a.Data == null)
            yield break;
        Debug.Log("Bubble Asset = " + a.Data.name);
        Debug.Log("Is Final = " + a.Data.IsFinalBubble);
        if (a.Data.IsFinalBubble)
        {
            Debug.Log("FINAL BUBBLE COLLECTED: " + a.Data.name);
            StartCoroutine(CollectFinalBubble(a, b));

            yield break;
        }

        if (a.Data.NextLevelBubble == null)
        {
            Debug.LogError(a.Data.name + " is not marked FinalBubble and has no NextLevelBubble assigned!");

            yield break;
        }

        Debug.Log("Next Level Bubble = " + a.Data.NextLevelBubble);

        BubbleData next = a.Data.NextLevelBubble;

        Destroy(a.gameObject);
        Destroy(b.gameObject);
        Debug.Log("PARTICLE SPAWNED");
        Instantiate(MergeParticlePrefab, center, Quaternion.identity);   //instantiae particles

        GameObject merged = Instantiate(BubblePrefab, center, Quaternion.identity);    //merges bubble to next level;
       // BubbleSpawner.Instance.FillBoard();
        AudioManager.Instance.Play(AudioManager.Instance.MergeSound);
        yield return new WaitForEndOfFrame();

        // GameManager.Instance.CheckNoPossibleMoves();

        GameManager.Instance.CheckFrozenLoseCondition();
        merged.GetComponent<Bubble>().IsMerging = false;

        merged.GetComponent<Bubble>().SetData(next);

        yield return new WaitForEndOfFrame();

        BubbleSpawner.Instance.FillBoard();
        //GameManager.Instance.DelayedMoveCheck();
        // GameManager.Instance.CheckNoPossibleMoves();
        // Camera Shake Based On Level
        /*if (CameraShake.Instance != null)
        {
            int level = next.MergeLevel;

            switch (level)
            {
                case 2:
                    CameraShake.Instance.Shake(0.03f);
                    break;

                case 3:
                    CameraShake.Instance.Shake(0.06f);
                    break;

                case 4:
                    CameraShake.Instance.Shake(0.1f);
                    break;

                default:
                    CameraShake.Instance.Shake(0.15f);
                    break;
            }
        }*/
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(next.MergeLevel * 0.02f);
        }
        StartCoroutine(PopEffect(merged.transform));
    }

    IEnumerator PopEffect(Transform target)
    {
        target.localScale = Vector3.zero;

        float timer = 0;

        while (timer < 0.15f)
        {
            timer += Time.deltaTime;

            float t = timer / 0.15f;

            float scale = Mathf.Lerp(0, 1.3f, t);

            target.localScale = Vector3.one * scale;

            yield return null;
        }

        target.localScale = Vector3.one;

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 push = Random.insideUnitCircle.normalized;

            rb.AddForce(push * 2f, ForceMode2D.Impulse);
        }
    }

    IEnumerator CollectFinalBubble(Bubble a, Bubble b)
    {
        /*Vector3 center =
            (a.transform.position +
             b.transform.position) / 2f;

        float timer = 0;

        GameObject visual = Instantiate(BubblePrefab, center, Quaternion.identity);

        visual.GetComponent<Bubble>()
              .SetData(a.Data);

        Destroy(a.gameObject);
        Destroy(b.gameObject);

        Transform t = visual.transform;

        Vector3 startPos = t.position;

        Vector3 endPos = startPos + Vector3.up * 2f;

        while (timer < 0.5f)
        {
            timer += Time.deltaTime;

            float progress = timer / 0.5f;

            t.position = Vector3.Lerp(startPos, endPos, progress);

            float scale = Mathf.Lerp(1f, 0f, progress);

            t.localScale = Vector3.one * scale;

            t.Rotate(0, 0, 300 * Time.deltaTime);

            yield return null;
        }

        Destroy(visual);*/
        Vector3 center = (a.transform.position + b.transform.position) / 2f;

        GameObject visual = Instantiate(BubblePrefab, center, Quaternion.identity);

        visual.GetComponent<Bubble>().SetData(a.Data);

        // Remove gameplay components from visual copy
        Destroy(visual.GetComponent<Rigidbody2D>());

        Destroy(visual.GetComponent<BubbleMerge>());

        Destroy(visual.GetComponent<CircleCollider2D>());

        Destroy(a.gameObject);
        Destroy(b.gameObject);
        // BubbleSpawner.Instance.SpawnReplacement();

        yield return new WaitForEndOfFrame();

        BubbleSpawner.Instance.FillBoard();

        Transform t = visual.transform;

        Vector3 startPos = center;

        Vector3 endPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 100, Screen.height - 100, 10));

        float timer = 0;

        while (timer < 0.8f)
        {
            timer += Time.deltaTime;

            float progress = timer / 0.8f;

            t.position = Vector3.Lerp(startPos, endPos, progress);

            float scale = Mathf.Lerp(1.2f, 0.2f, progress);

            t.localScale = Vector3.one * scale;

            t.Rotate(0, 0, 500 * Time.deltaTime);

            yield return null;
        }

        GameManager.Instance.AddGoalProgress(a.Data);
        Destroy(visual);

        if (GameManager.Instance.WinPanel.activeSelf)
        {
            Destroy(visual);
            //BubbleSpawner.Instance.FillBoard();
            yield break;
        }

        yield return null;

        GameManager.Instance.CheckFrozenLoseCondition();
        // GameManager.Instance.DelayedMoveCheck();
        //  GameManager.Instance.CheckNoPossibleMoves();



    }



}
