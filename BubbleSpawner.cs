using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public static BubbleSpawner Instance;

    public GameObject BubblePrefab;

    public LevelData CurrentLevel;

    public Transform[] SpawnPoints;
    Queue<BubbleData> bubbleQueue = new Queue<BubbleData>();
    public int VisibleBubbleCount = 5;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Debug.Log("BubbleSpawner Start");
        SpawnLevel();
        /* foreach (var bubble in CurrentLevel.StartingBubbles)
         {
             bubbleQueue.Enqueue(bubble);
         }*/
    }

    void SpawnLevel()
    {
        Debug.Log("Spawning Level");

        bubbleQueue.Clear();

        foreach (BubbleData bubble in CurrentLevel.StartingBubbles)
        {
            bubbleQueue.Enqueue(bubble);
        }

        int spawnCount = Mathf.Min(VisibleBubbleCount, SpawnPoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnNextBubble(i);
        }

        GameManager.Instance.SetupGoals(CurrentLevel.Goals);
    }

    public void FillBoard()
    {
        /*Bubble[] currentBubbles =
         FindObjectsByType<Bubble>(
             FindObjectsSortMode.None);

        Debug.Log("Visible Count = " + currentBubbles.Length);
        Debug.Log("Queue Count = " + bubbleQueue.Count);

        int missingCount =
            VisibleBubbleCount -
            currentBubbles.Length;

        Debug.Log("Missing Count = " + missingCount);

        for (int i = 0;
             i < missingCount;
             i++)
        {
            SpawnReplacement();
        }*/

        Bubble[] currentBubbles =
        FindObjectsByType<Bubble>(
            FindObjectsSortMode.None);

        Debug.Log("Current Bubble Count = " + currentBubbles.Length);
        Debug.Log("Visible Target = " + VisibleBubbleCount);
        Debug.Log("Queue Count = " + bubbleQueue.Count);

        int missingCount =
            VisibleBubbleCount -
            currentBubbles.Length;

        Debug.Log("Missing Count = " + missingCount);

        for (int i = 0; i < missingCount; i++)
        {
            SpawnReplacement();
        }
    }

    public void SpawnReplacement()
    {
        if (bubbleQueue.Count == 0)
            return;

        BubbleData data =
            bubbleQueue.Dequeue();

        Transform freePoint =
            GetFreeSpawnPoint();

        if (freePoint == null)
            return;

        GameObject bubble =
            Instantiate(
                BubblePrefab,
                freePoint.position,
                Quaternion.identity);

        bubble.GetComponent<Bubble>()
              .SetData(data);
    }

    Transform GetFreeSpawnPoint()
    {
        foreach (Transform point in SpawnPoints)
        {
            bool occupied = false;

            Collider2D[] hits =
                Physics2D.OverlapCircleAll(
                    point.position,
                    0.5f);

            foreach (Collider2D hit in hits)
            {
                if (hit.GetComponent<Bubble>())
                {
                    occupied = true;
                    break;
                }
            }

            if (!occupied)
                return point;
        }

        return null;
    }

    /*public void SpawnReplacement()
    {
        if (bubbleQueue.Count == 0)
            return;

        int randomPoint = Random.Range(0, SpawnPoints.Length);

        SpawnNextBubble(randomPoint);
    }*/

    /*void SpawnLevel()
    {
        *//*Debug.Log("Spawning Level");
        for (int i = 0; i < CurrentLevel.StartingBubbles.Length; i++)
        {
            Debug.Log("Spawning " + CurrentLevel.StartingBubbles[i].name);
            Debug.Log(SpawnPoints[i].name + " Position = " + SpawnPoints[i].position);

            GameObject bubble = Instantiate(BubblePrefab, SpawnPoints[i].position, Quaternion.identity);
            bubble.transform.localScale = Vector3.one;
            bubble.GetComponent<Bubble>().SetData(CurrentLevel.StartingBubbles[i]);
        }
        GameManager.Instance.SetupGoals(CurrentLevel.Goals);*//*

        //---current code---//

        Debug.Log("Spawning Level");

        for (int i = 0; i < CurrentLevel.BubbleCount; i++)
        {
            BubbleData randomBubble = CurrentLevel.PossibleBubbles[Random.Range(0, CurrentLevel.PossibleBubbles.Length)];

            GameObject bubble = Instantiate(BubblePrefab, SpawnPoints[i].position, Quaternion.identity);

            bubble.GetComponent<Bubble>().SetData(randomBubble);
        }

        GameManager.Instance.SetupGoals(
            CurrentLevel.Goals);
    }*/


    void SpawnNextBubble(int spawnIndex)
    {
        if (bubbleQueue.Count == 0)
            return;

        BubbleData data =
            bubbleQueue.Dequeue();

        GameObject bubble =
            Instantiate(
                BubblePrefab,
                SpawnPoints[spawnIndex].position,
                Quaternion.identity);

        bubble.GetComponent<Bubble>()
              .SetData(data);
    }

    /* void Start()
     {


         *//* GameObject b = Instantiate(BubblePrefab, new Vector2(-7, 0), Quaternion.identity);

          b.GetComponent<Bubble>().SetData(StartBubble);

          b.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f, ForceMode2D.Impulse);*//*


         Spawn(new Vector2(-3, 0), Vector2.right);

         Spawn(new Vector2(3, 0), Vector2.left);


     }

     void Spawn(Vector2 pos, Vector2 forceDir)
     {
         GameObject b = Instantiate(BubblePrefab, pos, Quaternion.identity);

         b.GetComponent<Bubble>().SetData(StartBubble);

         b.GetComponent<Rigidbody2D>().AddForce(forceDir * 5f, ForceMode2D.Impulse);
     }*/
}
