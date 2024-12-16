using UnityEngine;
using UnityEditor;

public class Visual : MonoBehaviour
{
    [SerializeField] float range;

    void OnDrawGizmos()
    {
        // Draw the attack range as a flat circle
        Color attackColor = new Color(1, 0, 0, 0.7f);
        DrawCircle(range, attackColor);
    }

    void DrawCircle(float radius, Color color)
    {
        Handles.color = color;

        // Calculate the offset
        Vector3 offset = new Vector3(0, 0.75f, 0);

        // Draw a wire flat circle
        Handles.DrawWireDisc(transform.position - offset, Vector3.up, radius);
    }
}
