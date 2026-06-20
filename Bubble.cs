using UnityEngine;

public class Bubble : MonoBehaviour
{
    public BubbleData Data;

    SpriteRenderer sr;
    public bool IsMerging;
    Rigidbody2D rb;
    public bool IsCaptured;


    //ice trap
    public bool IsFrozen;

    public int FreezeHitsRemaining;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        Refresh();
    }

    /*  private void Start()
      {
          transform.localScale = Vector3.one;
      }*/
    public void SetData(BubbleData data)
    {
        Data = data;

        Refresh();
    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void Refresh()
    {
        if (Data == null)
            return;

        if (Data.BubbleSprite != null)
            sr.sprite = Data.BubbleSprite;
    }

    public void Select()
    {
        transform.localScale = Vector3.one * 1.15f;
    }

    public void Deselect()
    {
        transform.localScale = Vector3.one;
    }

    public void Freeze()
    {
        if (IsFrozen)
            return;

        IsFrozen = true;

        FreezeHitsRemaining = 2;

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        GetComponent<SpriteRenderer>().color = Color.cyan;
        GameManager.Instance.CheckFrozenLoseCondition();
    }

    public void HitFrozen()
    {
        FreezeHitsRemaining--;

        if (FreezeHitsRemaining <= 0)
        {
            Unfreeze();
        }
    }

    public void Unfreeze()
    {
        IsFrozen = false;

        GetComponent<Rigidbody2D>()
            .bodyType =
            RigidbodyType2D.Dynamic;

        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
