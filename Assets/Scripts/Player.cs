using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class Player : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype _cc;
    public float speed = 0.1f;
    public float rotationSpeed = 9f;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
    }
    public override void FixedUpdateNetwork()
    {
        //base.FixedUpdateNetwork();
        if(GetInput(out NetworkInputData data))
        {
            if(data.moveForward)
            {
                data.direction.Normalize();
                Vector3 currentPosition = _cc.transform.position;
                Vector3 newPosition = currentPosition + (transform.forward * speed * Time.deltaTime);

                // Assign the new position to the GameObject
                _cc.transform.position = newPosition;
            }
            if(data.moveBackward)
            {
                data.direction.Normalize();
                Vector3 currentPosition = _cc.transform.position;
                Vector3 newPosition = currentPosition - (transform.forward * speed * Time.deltaTime);

                // Assign the new position to the GameObject
                _cc.transform.position = newPosition;
            }
            float rotationAmount = rotationSpeed * Time.deltaTime;

            // Apply the rotation around the y-axis
            if(data.leftRotate && data.canMove)
            {
                transform.Rotate(Vector3.down, rotationAmount);
            }
            if(data.rightRotate && data.canMove)
            {
                transform.Rotate(Vector3.up, rotationAmount);
            }
            //_cc.Move(15 * data.direction * Runner.DeltaTime);
        }
    }
}
