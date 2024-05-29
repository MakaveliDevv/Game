using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
   void OnDrawGizmos()
    {
        if (TryGetComponent<Collider>(out var collider))
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f); // Set the color (red with 50% transparency)

            if (collider is BoxCollider boxCollider)
            {
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
                Gizmos.DrawCube(boxCollider.center, boxCollider.size);
            }
            else if (collider is SphereCollider sphereCollider)
            {
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
                Gizmos.DrawSphere(sphereCollider.center, sphereCollider.radius);
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
                Gizmos.DrawWireSphere(capsuleCollider.center, capsuleCollider.radius);
            }
            // Add other collider types as needed
        }
    }
}
