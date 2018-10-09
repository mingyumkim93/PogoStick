using UnityEngine;

public class PogoStick : MonoBehaviour {

    Rigidbody pogoStickBody;
    [SerializeField]
    float steeringSpeed = 100f;
    [SerializeField]
    float powerMultiplier = 2f;
    [SerializeField]
    private float _maxDepth = 0.25f;
    [SerializeField]
    private float _springStiffness = 3f;
    private float _collisionTime;

    void Start()
    {
        pogoStickBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RespondToSteeringInput();
        if (pogoStickBody.position.y <= 0)
            UpdateJumpSequence();
    }

    private void UpdateJumpSequence() {
        var relativeForce = -pogoStickBody.position.y / _maxDepth;
        if (relativeForce >= 1f)
            pogoStickBody.velocity = new Vector3(pogoStickBody.velocity.x, 0, pogoStickBody.velocity.z);
        Vector3 force = pogoStickBody.mass * Physics.gravity * _springStiffness * powerMultiplier * relativeForce;
        force = Vector3.Dot(force, pogoStickBody.transform.up) * pogoStickBody.transform.up.normalized;
        pogoStickBody.AddForce(-force);
    }

    private void RespondToSteeringInput()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back, h * steeringSpeed * Time.deltaTime);
        float v = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.right, v * steeringSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(-Vector3.up, steeringSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, steeringSpeed * Time.deltaTime);
    }
}
