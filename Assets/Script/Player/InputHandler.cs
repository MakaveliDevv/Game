using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVector {get; private set;}

    public Vector3 MousePosition {get; private set;}

    // void Update() 
    // {
    //     var h = Input.GetAxisRaw("Horizontal");
    //     var v = Input.GetAxisRaw("Vertical");
    //     InputVector = new(h, v);

    //     MousePosition = Input.mousePosition; 
    // }

    void Update() 
    {
        InputVector = new(Input.GetAxisRaw(Tags.HORIZONTAL), Input.GetAxisRaw(Tags.VERTICAL));
        MousePosition = Input.mousePosition;
    }
}
