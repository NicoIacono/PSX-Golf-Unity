using UnityEngine;

public class CameraController : MonoBehaviour
{

    public BallController ball;
    public Transform ballT;
    public float sens;
    
    private float mouseX = 0;
    private float mouseY = 0;

    private float yRot;
    public float xRot;
    


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = ballT.position;

        if (!ball.adjusting)
            moveCamera();
    }

    void moveCamera()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot -= mouseY * sens;
        xRot += mouseX * sens;

        yRot = Mathf.Clamp(yRot, -20f, 65f);

        transform.rotation = Quaternion.Euler(0, xRot, yRot);
    }
}
