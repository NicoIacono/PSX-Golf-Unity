using UnityEngine;

public class holeController : MonoBehaviour
{
    public GameObject outline;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("glub");
        outline.SetActive(false);
    }
}
