using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    Transform playerTransform;
    public float movementSpeed = 3f;
    public float rotationSpeed = 2f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransform = transform;
    }

    void FixedUpdate()
    {
        // Right Stick to rotate and Left stick to move forward and backwards
        //Move(Input.GetAxis("Vertical"));
        //Rotate(new Vector3(0, Input.GetAxis("Right Horizontal")));

        Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        LookToStick(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Debug.Log(Input.GetAxis("Right Trigger"));
    }

    void Move(float forward)
    {
        Vector3 direction =  Vector3.zero;
        if (forward > 0)
            direction = playerTransform.forward;
        if (forward < 0)
            direction = -playerTransform.forward;

        Vector3 newPos = playerRigidbody.position + direction * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(newPos);
    }

    void Move(Vector3 direction)
    {
        Vector3 newPos = playerRigidbody.position + direction * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(newPos);
    }

    void Rotate(Vector3 direction)
    {
        playerRigidbody.rotation = Quaternion.Euler(playerRigidbody.rotation.eulerAngles+direction * rotationSpeed * Time.deltaTime);
    }

    void LookToStick(float xAxis, float yAxis)
    {
        playerRigidbody.angularVelocity = Vector3.zero;
        playerRigidbody.velocity = Vector3.zero;
        if (xAxis == 0 && yAxis == 0)
            return;
        float heading = Mathf.Atan2(xAxis, yAxis) * Mathf.Rad2Deg;

        Vector3 currentAngle = playerTransform.eulerAngles;
        currentAngle.y = Mathf.LerpAngle(playerTransform.eulerAngles.y, heading, Time.deltaTime*rotationSpeed);
        playerRigidbody.MoveRotation(Quaternion.Euler(currentAngle));
    }
}
