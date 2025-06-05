using UnityEngine;

public class holeController : MonoBehaviour
{
    public bool scored = false;

    void OnTriggerEnter(Collider collider)
    {
        scored = true;
    }
}
