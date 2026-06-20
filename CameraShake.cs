using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    Vector3 startPos;

    private void Awake()
    {
        Instance = this;

        startPos = transform.position;
    }

    public void Shake(float strength)
    {
        StartCoroutine(ShakeRoutine(strength));
    }

    IEnumerator ShakeRoutine(float strength)
    {
        float timer = 0;

        while (timer < 0.1f)
        {
            timer += Time.deltaTime;

            transform.position = startPos + (Vector3)Random.insideUnitCircle * strength;

            yield return null;
        }

        transform.position = startPos;
    }
}
