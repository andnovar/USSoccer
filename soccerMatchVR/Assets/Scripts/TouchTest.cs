using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchTest : MonoBehaviour {

    public Text pos;
    Vector2 xbounds;
    Vector2 ybounds;

	// Use this for initialization
	void Start () {
        xbounds = new Vector2(129.0f, 1080.0f);
        ybounds = new Vector2(590.0f, 1130.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //Touch myTouch = Input.GetTouch(0);
        Touch[] myTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            if(myTouches[i].position.x >= xbounds.x && myTouches[i].position.x <= xbounds.y && myTouches[i].position.y >= ybounds.x && myTouches[i].position.y <= ybounds.y)
                pos.text = "x: " + myTouches[i].position.x + "y: " + myTouches[i].position.y;
        }
        
    }

}
