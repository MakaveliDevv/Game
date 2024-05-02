using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVector {get; private set;}

    public Vector3 MousePosition {get; private set;}

    void Update() 
    {
        InputVector = new(Input.GetAxisRaw(Tags.HORIZONTAL), Input.GetAxisRaw(Tags.VERTICAL));
        MousePosition = Input.mousePosition;
    }
}
