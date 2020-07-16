using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectController : MonoBehaviour
{

    private int breakCount = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("タッチされたよ");
        breakCount++;
        Debug.Log(breakCount);

        if (this.gameObject.tag == "RoadBlockerTag" && breakCount == 2)
        {
			Destroy(this.gameObject);
        }
		else if(this.gameObject.tag == "PianoTag" && breakCount == 4)
		{
            Destroy(this.gameObject);
		}
		else if(this.gameObject.tag == "TankTag" && breakCount == 10)
		{
            Destroy(this.gameObject);
		}
    }


}
