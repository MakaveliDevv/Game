using UnityEngine;

/* This handles the interaction with the player */
public class Interactable : MonoBehaviour
{
    // private float boxRadius;
    // protected Transform target;
    // private GameObject objectPrefab;
    // private BoxCollider boxCollider;

    private bool inRange = false;

    public virtual void Start() 
    {
        // target = PlayerManager.instance.player.transform;
        // objectPrefab = gameObject;

        // boxCollider = GetComponent<BoxCollider>();    
        // Vector3 colliderSize = boxCollider.size; // Get the size of the box collider
        // boxRadius = colliderSize.magnitude; // Calculate the radius as the magnitude of the size
    }
    
    public virtual void Interact()
    {
        // this method is ment to be overwritten
    }

    void OnTriggerEnter(Collider collider) 
    {   
        if(collider.CompareTag("Player") && !inRange) 
        {
            inRange = true;
            Interact();
        }

    }

    void OnTriggerExit(Collider collider) 
    {   
        if(collider.CompareTag("Player") && inRange) 
        {
            inRange = false;
        }
    }



    #region PROLLY NOT NEEDED
    // public void CheckDistance()
    // {
    //     distance = Vector3.Distance(target.position, transform.position);
    //     if (distance <= collideRadius && !inRange)
    //     {
    //         inRange = true;
    //         Interact();
    //     }
    //     else if (distance >= collideRadius && inRange)
    //     {
    //         inRange = false;
    //     }
    // }

    // public virtual void OnDrawGizmosSelected()
    // {
    //     if(objectPrefab == null) 
    //         objectPrefab = gameObject;
    

    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, collideRadius);
    // }
    #endregion
}
