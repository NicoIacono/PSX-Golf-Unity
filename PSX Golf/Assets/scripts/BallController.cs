using TMPro;
using System;
using UnityEngine;

public class BallController : MonoBehaviour
{ 
    public GameObject outline;
    public TextMeshProUGUI powerText;
    public CameraController cam;
    public int power = 0;
    public bool adjusting;

    Rigidbody rb;
    private float accumulatedMouseMovement = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        powerText.text = "Power: " + power;

        if (Math.Abs(rb.velocity.x) < 0.3f && Math.Abs(rb.velocity.y) < 0.3f && Math.Abs(rb.velocity.z) < 0.3f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            outline.SetActive(true);

            adjustPower();

            if (Input.GetMouseButtonUp(0))
            {
                transform.rotation = Quaternion.Euler(0, cam.xRot, 0);

                Vector3 impulse = -transform.right * power;
                rb.AddForce(impulse, ForceMode.Impulse);
                power = 0;
                adjusting = false;
            }
        } else
            outline.SetActive(false);
    }

    void adjustPower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            adjusting = true;
            power = 0;
            accumulatedMouseMovement = 0f;
        }
        
        if (adjusting)
        {
            float deltaX = Input.GetAxis("Mouse X"); 
            accumulatedMouseMovement += deltaX;

            power = Mathf.Clamp(Mathf.RoundToInt(accumulatedMouseMovement * cam.sens/3), 0, 300);
        }
    }
}