using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraa : MonoBehaviour
{
    [SerializeField]private Vector3 cameraPosition;
    [SerializeField]private Vector3 cameraRotation;
    [SerializeField]private GameObject player;
    public float followSpeed;

    void Start()
    {
        ResetCamera();
        SetPositionAndRotation(cameraPosition, Quaternion.Euler(cameraRotation));
    }

    void Update() 
    {
        FollowPlayer();
    }

    private void ResetCamera()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    private void SetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    private void FollowPlayer() 
    {
        if(player != null) 
        {
            Vector3 targetPosition = player.transform.position + cameraPosition; // calculated target
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime); // smooth the camera movement
        }
    }
}