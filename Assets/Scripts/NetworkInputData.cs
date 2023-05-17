using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public Vector3 direction;
    public Vector3 position;
    public bool canMove;
    public bool moveForward;
    public bool moveBackward;
    public bool leftRotate;
    public bool rightRotate;
}
