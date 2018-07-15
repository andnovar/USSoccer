using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour {

    public Text debug;
    NetworkService service;

    // Use this for initialization
    void Start () {
        service = NetworkService.Instance;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        string text = GetComponentInChildren<TextMesh>().text;
        //service.SendTapToAddSth(text);
        Debug.Log("This is a test");
        debug.text = "Clicked";
    }
}
