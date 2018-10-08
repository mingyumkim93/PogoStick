using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoStick : MonoBehaviour {

    Animator animator;
    Rigidbody myRigidbody;
    float speed = 90f;
    float powerMuliplier = 100f;
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
        transform.Rotate(Vector3.back, h * speed * Time.deltaTime);
        float v = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.right, v * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(-Vector3.up, speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        powerMuliplier = 100f; 
        animator.Play("ShrinkSpring");
        StartCoroutine(GetPowerAndJump());
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKey(KeyCode.Space))
            powerMuliplier += Time.deltaTime * 300;
    }

    IEnumerator GetPowerAndJump()
    {
        yield return new WaitForSeconds(shrinkingSpringAnim.length + expandingSpringAnim.length);
        GetForceToJump();
    }

    void GetForceToJump()
    {
        myRigidbody.AddForce(transform.up * powerMuliplier);
        print("powermuliplier = " + powerMuliplier);
    }

}
