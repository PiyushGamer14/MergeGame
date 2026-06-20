using System.Collections;
using UnityEngine;

public class MergeEffect : MonoBehaviour
{
    public IEnumerator PlayMerge()
    {
        Vector3 original = transform.localScale;

        transform.localScale = original * 1.3f;

        yield return new WaitForSeconds(0.05f);

        transform.localScale = original * 0.8f;

        yield return new WaitForSeconds(0.05f);

        transform.localScale = original;
    }
}
