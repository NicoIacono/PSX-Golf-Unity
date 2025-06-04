using UnityEngine;

public class spin : MonoBehaviour
{
    public float speed;

    float rot = 0;

    void Update()
    {
        rot += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 90, rot);
    }
}
