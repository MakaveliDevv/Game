using UnityEngine;

/* This handles the interaction with the player */
public class Interactable : MonoBehaviour
{
    public float collideRadius = 3f;
    protected Transform target;
    public GameObject objectPrefab;

    private bool player_inRange = false;

    public virtual void Start() 
    {
        target = PlayerManager.instance.player.transform;
    }
    
    public virtual void Interact()
    {
        // this method is ment to be overwritten
        // Debug.Log("Interacting with " + transform.name);
    }

    public void Update() 
    {
        CheckDistance();
    }
    
    public void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= collideRadius && !player_inRange)
        {
            player_inRange = true;
            Interact();
        }
        else if (distance >= collideRadius && player_inRange)
        {
            player_inRange = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if(objectPrefab == null) 
            objectPrefab = gameObject;
    

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collideRadius);
    }
}
