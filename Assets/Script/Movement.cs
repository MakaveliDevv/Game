using UnityEngine;

[RequireComponent(typeof(ObstacleAgent))]
public class Movement : MonoBehaviour
{
    [SerializeField] Camera cam = null;
    private ObstacleAgent agent;

    private RaycastHit[] hits = new RaycastHit[1];

    private void Awake()
    {
        agent = GetComponent<ObstacleAgent>();
    }

    private void Update() 
    {
        if(Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.RaycastNonAlloc(ray, hits) > 0) 
            {
                agent.SetDestination(hits[0].point);
            }
        }
    }
}
