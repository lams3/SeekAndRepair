using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        float angle = -Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        this.transform.Rotate(Vector3.up, angle, Space.Self);
    }
}
