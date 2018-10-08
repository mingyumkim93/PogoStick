using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoStick : MonoBehaviour {

    private Rigidbody myRigidbody;
    public float delta = 1f;
    public float powerMultiplier = 15f;
    Animator animator;
	
	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        ReactToInput();
    }

    private void ReactToInput()
    {
        if (Input.GetMouseButton(0))
            myRigidbody.AddForce(Vector3.left);
        if (Input.GetKey(KeyCode.W))
            transform.Rotate(new Vector3(delta, 0, 0) * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Rotate(new Vector3(-delta, 0, 0));
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, 0, delta));
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, 0, -delta));
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(new Vector3(0, -delta, 0), Space.World);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(new Vector3(0, delta, 0), Space.World);
        if (Input.GetKey(KeyCode.Space))
            powerMultiplier++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(GettingExtraPower());
    }

    IEnumerator GettingExtraPower()
    {
        animator.Play("ShrinkSpring");
        powerMultiplier = 15f;
        yield return new WaitForSeconds(.5f);
        ExpandSpringAndAddForce();
    }

    void ExpandSpringAndAddForce()
    {
        powerMultiplier = powerMultiplier / 3;
        print("power multiplier = " + powerMultiplier);
        myRigidbody.AddForce(powerMultiplier * transform.up, ForceMode.Impulse);
        animator.Play("ExpandSpring");
    }
}
