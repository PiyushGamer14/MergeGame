using UnityEngine;

public class ShapePreview : MonoBehaviour
{
    public LevelShape Shape;

    private void OnDrawGizmos()
    {
        if (Shape == null)
            return;

        Gizmos.color = Color.green;

        foreach (var pos in Shape.Positions)
        {
            Gizmos.DrawSphere(
                transform.position +
                (Vector3)pos,
                0.2f);
        }
    }
}
