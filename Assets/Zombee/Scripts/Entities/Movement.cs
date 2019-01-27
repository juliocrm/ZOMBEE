using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Entity
{
    Rigidbody playerRigidbody;
    Transform playerTransform;
    Stamina playerStamina;
    public float movementSpeed = 3f;
    public float tiredSpeed = 2f;
    public float rotationSpeed = 2f;
    public float runStaminaCost = .5f;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransform = transform;
        playerStamina = GetComponent<Stamina>();
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        Quaternion currentRotation = playerRigidbody.rotation;

        currentRotation.x = Mathf.Lerp(currentRotation.x, 0f, .1f);
        currentRotation.z = Mathf.Lerp(currentRotation.z, 0f, .1f);


        try
        {
            playerRigidbody.rotation = currentRotation;
        }
        catch (Exception e)
        {
            playerRigidbody.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    public void Move(float forward)
    {
        if (enabled)
        {
            Move(playerTransform.forward * forward);
        }
    }

    public void Move(Vector3 direction)
    {
        if (enabled)
        {
            if (direction == Vector3.zero)
                return;
            //print(playerStamina.StaminaAmount);
            float currentSpeed = (playerStamina.StaminaAmount > 5) ? movementSpeed : tiredSpeed;
            Vector3 newPos = playerRigidbody.position + direction * currentSpeed * Time.deltaTime;
            if(playerStamina.StaminaAmount > 5)
                playerStamina.Hurt(runStaminaCost * Time.deltaTime, transform.position);
            playerRigidbody.MovePosition(newPos);
        }
    }

    public void Rotate(Vector3 direction)
    {
        if (enabled)
        {
            Quaternion currentRotation = playerRigidbody.rotation;
            currentRotation.y = Quaternion
                .Euler(playerRigidbody.rotation.eulerAngles + direction * rotationSpeed * Time.deltaTime).y;
            playerRigidbody.rotation = currentRotation;
        }
    }

    public void LookToStick(float xAxis, float yAxis)
    {
        if (enabled)
        {
            playerRigidbody.angularVelocity = Vector3.zero;
            playerRigidbody.velocity = Vector3.zero;
            if (xAxis == 0 && yAxis == 0)
                return;
            float heading = Mathf.Atan2(xAxis, yAxis) * Mathf.Rad2Deg;

            Vector3 currentAngle = playerTransform.eulerAngles;
            currentAngle.y = Mathf.LerpAngle(playerTransform.eulerAngles.y, heading, Time.deltaTime * rotationSpeed);
            playerRigidbody.MoveRotation(Quaternion.Euler(currentAngle));
        }
    }

    public override void Die()
    {
        enabled = false;
    }

    public Vector3 GetPlayerPosition() {
        return playerTransform.position;
    }
}
