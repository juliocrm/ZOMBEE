using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Entity
{
    Rigidbody playerRigidbody;
    Transform playerTransform;
    Stamina playerStamina;
    public float movementSpeed = 3f;
    public float rotationSpeed = 2f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransform = transform;
        playerStamina = GetComponent<Stamina>();
    }

    public void Move(float forward)
    {
        if (forward == 0)
            return;
        movementSpeed = (playerStamina.StaminaAmount > 5) ? 3 : 1;
        Vector3 direction = Vector3.zero;
        if (forward > 0)
            direction = playerTransform.forward;
        else
            direction = -playerTransform.forward;

        Vector3 newPos = playerRigidbody.position + direction * movementSpeed * Time.deltaTime;
        //if(playerStamina.StaminaAmount > 5)
        //  playerStamina.Hurt(runStaminaCost);
        playerRigidbody.MovePosition(newPos);
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;
        print(playerStamina.StaminaAmount);
        movementSpeed = (playerStamina.StaminaAmount > 5) ? 3 : 1;
        Vector3 newPos = playerRigidbody.position + direction * movementSpeed * Time.deltaTime;
        //if(playerStamina.StaminaAmount > 5)
        //  playerStamina.Hurt(runStaminaCost);
        playerRigidbody.MovePosition(newPos);
    }

    public void Rotate(Vector3 direction)
    {
        playerRigidbody.rotation = Quaternion.Euler(playerRigidbody.rotation.eulerAngles + direction * rotationSpeed * Time.deltaTime);
    }

    public void LookToStick(float xAxis, float yAxis)
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

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
