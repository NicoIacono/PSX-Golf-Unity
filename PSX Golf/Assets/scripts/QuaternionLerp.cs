using UnityEngine;

public class QuaternionLerp : MonoBehaviour
{
    [Header("Target Rotations")]
    public Quaternion rotationA = Quaternion.identity;
    public Quaternion rotationB = Quaternion.Euler(0f, 180f, 0f);

    [Header("Timing (in seconds)")]
    public float period = 2f;        // Time for a full A→B→A cycle
    public float restPeriod = 1f;    // Time to pause between each half-rotation

    private float time;
    private bool isResting = false;
    private float restTimer = 0f;
    private bool goingForward = true; // True = A→B, False = B→A

    void Update()
    {
        if (period <= 0f) return;

        float halfPeriod = period / 2f;

        if (isResting)
        {
            restTimer += Time.deltaTime;
            if (restTimer >= restPeriod)
            {
                isResting = false;
                restTimer = 0f;
                time = 0f;
                goingForward = !goingForward; // Reverse direction
            }
            return;
        }

        time += Time.deltaTime;
        float t = Mathf.Clamp01(time / halfPeriod);

        // Choose rotation direction
        Quaternion from = goingForward ? rotationA : rotationB;
        Quaternion to   = goingForward ? rotationB : rotationA;

        // Interpolate
        transform.rotation = Quaternion.Slerp(from, to, t);

        // When one half-cycle is complete, enter rest state
        if (t >= 1f)
        {
            isResting = true;
        }
    }
}
