using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightDisplay : MonoBehaviour {

    private float height;
    public Text heightText;
    public GameObject pogoStick;

	// Update is called once per frame
	void Update () {
        height = pogoStick.transform.position.y;
        heightText.text = "Height : " + height + "M";

	}
}
