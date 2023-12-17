using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject objectPrefab;
    public Inventory _inventory;
    public GameObject player;

    public float radius = 3f;
    public bool player_inRange = false;

    void Update()
    {
        CheckDistance();
    }

    public virtual void Interact()
    {
        // this method is ment to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= radius && !player_inRange)
        {
            player_inRange = true;
            Interact();
        }
        else if (distance >= radius && player_inRange)
        {
            player_inRange = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if(objectPrefab == null) 
            objectPrefab = gameObject;
    

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
