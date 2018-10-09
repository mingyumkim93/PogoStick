using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoStick : MonoBehaviour {

    Animator animator;
    Rigidbody myRigidbody;
    float steeringSpeed = 100f;
    float powerMultiplierIncresingSpeed = 300f;
    float powerMultiplier = 100f;
    [SerializeField] AnimationClip shrinkingSpringAnim;
    [SerializeField] AnimationClip expandingSpringAnim;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RespondToSteeringInput();

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

    private void OnCollisionEnter(Collision collision)
    {
        powerMultiplier = 100f; 
        animator.Play("ShrinkSpring");
        StartCoroutine(GetPowerAndJump());
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKey(KeyCode.Space))
            powerMultiplier += Time.deltaTime * powerMultiplierIncresingSpeed;
    }

    IEnumerator GetPowerAndJump()
    {
        yield return new WaitForSeconds(shrinkingSpringAnim.length + expandingSpringAnim.length);
        GetForceToJump();
    }

    void GetForceToJump()
    {
        myRigidbody.AddForce(transform.up * powerMultiplier);
        print("powermuliplier = " + powerMultiplier);
    }

}
