using UnityEngine;

public class Visualizer : MonoBehaviour
{
    void OnDrawGizmos()
    {
        if (TryGetComponent<Collider>(out var collider))
        {
            // Set the default color to green with 50% transparency
            Gizmos.color = new Color(0, 1, 0, 0.5f);

            if (collider is BoxCollider boxCollider)
            {
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
                Gizmos.DrawCube(boxCollider.center, boxCollider.size);
            }
            // else if (collider is SphereCollider sphereCollider)
            // {
            //     Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            //     Gizmos.DrawSphere(sphereCollider.center, sphereCollider.radius);
            // }
            // else if (collider is CapsuleCollider capsuleCollider)
            // {
            //     Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            //     Gizmos.DrawWireSphere(capsuleCollider.center, capsuleCollider.radius);
            // }
            // else if (collider is MeshCollider meshCollider)
            // {
            //     // Set the color to yellow with 50% transparency for MeshCollider
            //     Gizmos.color = new Color(1, 1, 0, 0.5f); 
            //     Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            //     Gizmos.DrawWireMesh(meshCollider.sharedMesh);
            // }
            // // Add other collider types as needed
        }

        if(transform.CompareTag("SpawnLocation") && TryGetComponent<Collider>(out var col)) 
        {
            Gizmos.color = Color.yellow;

            if(collider is BoxCollider boxCol)
            {
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
                Gizmos.DrawCube(boxCol.center, boxCol.size);
            }
        }
    }
}
