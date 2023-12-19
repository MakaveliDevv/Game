using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    public Transform player;
    private Camera cam;
    private readonly float sensitivity = 25f;
    private readonly float smoothness = 10f;
    private Vector3 targetRotation;
    public Vector3 mousePosition;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        LookAround(mousePosition);
    }

    public void GetMousePosition() 
    {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        mousePosition = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.transform.position.y - player.position.y));
        // Debug.DrawLine(cam.transform.position, mousePosition, Color.red);
    } 

    private void LookAround(Vector3 vector3)
    {

        GetMousePosition();
        Vector3 newTargetRotation = (vector3 - player.position).normalized;
        newTargetRotation.y = 0f;

        // Accumulate the rotation over time
        targetRotation = Vector3.Slerp(targetRotation, newTargetRotation, sensitivity * Time.deltaTime);

        if (targetRotation.magnitude >= 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(targetRotation); // set the Vector3 target rotation into a quaternion to be able to use it in slerp
            player.rotation = Quaternion.Slerp(player.rotation, rotation, sensitivity * Time.deltaTime * smoothness);
        }
    }

    public Vector3 ReturnMousePosition() 
    {
        return mousePosition;
    }
}
