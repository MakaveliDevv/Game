using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisualizer : MonoBehaviour
{
    void OnDrawGizmos()
    {
        if (TryGetComponent<Collider>(out var collider))
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f); 

            if (collider is BoxCollider boxCollider)
            {
                // Set the color to red with 50% transparency for BoxCollider
                Gizmos.color = new Color(1, 0, 0, 0.5f); 
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
                Gizmos.DrawCube(boxCollider.center, boxCollider.size);
            }
        }
    }
}
