using UnityEngine;

public class Temproray : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5f, ForceMode2D.Impulse);
    }
}
