using TMPro;
using System;
using UnityEngine;
using Unity.VisualScripting;
using System.Runtime.ConstrainedExecution;

public class BallController : MonoBehaviour
{
    [Header("UI")]
    public GameObject outline;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI distText;
    public TextMeshProUGUI strokesText;
    public GameObject points;
    [Header("Scripts")]
    public CameraController cam;
    public GameObject hole;
    [Header("Bools")]
    public bool adjusting;
    public bool grounded;
    [Header("Floats")]
    public int power = 0;
    public float downforceStrength;
    public int par = 3; //idk change later

    [Header("Drag")]
    public float minDrag = 0.05f;
    public float maxDrag = 3f;
    public float lowDragSpeed = 7f;

    Rigidbody rb;
    private float accumulatedMouseMovement = 0f;
    int strokes = 0;
    int distance = 0;

    private float maxPower = 150f;

    void Start()
    {
        points.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        distance = Mathf.RoundToInt(Vector3.Distance(transform.position, hole.transform.position));
        distText.text = distance + "m";
        powerText.text = Mathf.RoundToInt(Mathf.Lerp(0f, 100f, power / maxPower)) + "%";

        if (Math.Abs(rb.velocity.x) < 0.3f && Math.Abs(rb.velocity.y) < 0.3f && Math.Abs(rb.velocity.z) < 0.3f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            outline.SetActive(true);

            adjustPower();

            if (Input.GetMouseButtonUp(0) && power > 0)
            {
                transform.rotation = Quaternion.Euler(0, cam.xRot, 0);

                Vector3 impulse = -transform.right * power;
                rb.AddForce(impulse, ForceMode.Impulse);
                power = 0;
                adjusting = false;
                strokes++;
                strokesText.text = "Strokes:" + strokes;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                adjusting = false;
            }
        }
        else
            outline.SetActive(false);

        if (hole.GetComponent<holeController>().scored)
        {
            points.SetActive(true);

            if (strokes == 1)
                points.GetComponent<TextMeshProUGUI>().text = "Hole in One!";
            else if (par - strokes == 2)
                points.GetComponent<TextMeshProUGUI>().text = "Eagle!";
            else if (par - strokes == 1)
                points.GetComponent<TextMeshProUGUI>().text = "Birdie!";
            else if (par - strokes == 0)
                points.GetComponent<TextMeshProUGUI>().text = "Par";
            else if (par - strokes == -1)
                points.GetComponent<TextMeshProUGUI>().text = "Bogey";
            else if (par - strokes == -2)
                points.GetComponent<TextMeshProUGUI>().text = "Double Bogey";
        }
    }

    void FixedUpdate()
    {
        downforce();
        AdjustDragBasedOnSpeed(rb, lowDragSpeed, maxDrag, minDrag);
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

            power = Mathf.Clamp(Mathf.RoundToInt(accumulatedMouseMovement * cam.sens / 2), 0, (int)maxPower);
        }
    }

    void downforce()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.2f);

        if (grounded)
        {
            rb.AddForce(-hit.normal * downforceStrength, ForceMode.Acceleration);
        }
    }
    
    public void AdjustDragBasedOnSpeed(Rigidbody rb, float maxSpeed, float maxDrag, float _drag)
    {
        if (rb == null) return;

        float speed = rb.velocity.magnitude;

        if (speed > maxSpeed)
        {
            // If moving faster than maxSpeed, apply custom drag value
            rb.drag = _drag;
        }
        else
        {
            // When slower, interpolate drag from maxDrag to 0
            float speedFactor = Mathf.Clamp01(speed / maxSpeed); // 0 (slow) → 1 (maxSpeed)
            rb.drag = Mathf.Lerp(maxDrag, 0f, speedFactor);
        }
    }
}