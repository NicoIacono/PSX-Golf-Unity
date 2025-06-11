using UnityEngine;

public class PositionLerp : MonoBehaviour
{
    [Header("Target Positions")]
    public Vector3 positionA;
    public Vector3 positionB;

    [Header("Timing (in seconds)")]
    public float period = 2f;
    public float restPeriod = 1f;

    private float time;
    private bool isResting = false;
    private float restTimer = 0f;
    private bool goingForward = true;

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
                goingForward = !goingForward;
            }
            return;
        }

        time += Time.deltaTime;
        float t = Mathf.Clamp01(time / halfPeriod);

        Vector3 from = goingForward ? positionA : positionB;
        Vector3 to   = goingForward ? positionB : positionA;

        transform.localPosition = Vector3.Slerp(from, to, t);

        if (t >= 1f)
        {
            isResting = true;
        }
    }
}
