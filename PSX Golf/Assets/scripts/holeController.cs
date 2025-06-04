using UnityEngine;

public class holeController : MonoBehaviour
{
    public GameObject outline;
    public BallController ball;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("glub");
        ball.enabled = false;
        outline.SetActive(false);
    }
}
