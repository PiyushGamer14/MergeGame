using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    public GameObject DotPrefab;

    public int DotCount = 20;

    public float DotSpacing = 0.2f;

    List<GameObject> dots = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < DotCount; i++)
        {
            GameObject dot = Instantiate(DotPrefab, transform);

            dot.SetActive(false);

            dots.Add(dot);
        }
    }

    public void ShowTrajectory(Vector2 startPos, Vector2 force)
    {
        for (int i = 0; i < dots.Count; i++)
        {
            float t = i * DotSpacing;

            Vector2 pos = startPos + force * t;

            dots[i].transform.position = pos;
            float scale = Mathf.Lerp(0.2f, 0.05f, (float)i / DotCount);

            dots[i].transform.localScale = Vector3.one * scale;

            dots[i].SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (GameObject dot in dots)
        {
            dot.SetActive(false);
        }
    }
}
