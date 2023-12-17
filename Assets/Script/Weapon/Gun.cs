using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    public Camera topDownCam;
    [SerializeField] private MouseBehaviour _mouseScript;
    [SerializeField] private LayerMask layerMask;

    void Awake()
    {
        topDownCam = Camera.main;
        _mouseScript = Object.FindFirstObjectByType<MouseBehaviour>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePos = _mouseScript.ReturnMousePosition(); // Get the mouse position
        Vector3 screenMousePos = topDownCam.WorldToScreenPoint(mousePos);

        Ray ray = topDownCam.ScreenPointToRay(screenMousePos); // cast a ray from the camera to the mouse position

        bool hit = Physics.Raycast(ray, out RaycastHit info, range, layerMask);

        if (hit && info.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log(info.collider.name);
            // Target target = info.transform.GetComponent<Target>();
            // target?.TakeDamage(damage);
        } 
    }
}
