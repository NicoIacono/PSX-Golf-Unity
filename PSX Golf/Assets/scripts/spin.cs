using UnityEngine;

public class spin : MonoBehaviour
{
    public float speed;

    public bool y = false;

    float rot = 0;

    void Update()
    {
        if (!y)
        {
            rot += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 90, rot);
        }
        else
        {
            rot += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, rot, 0);
        }
    }
}
