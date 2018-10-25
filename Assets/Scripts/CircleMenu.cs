using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMenu : MonoBehaviour
{
private Vector3 fp;   //Первая позиция касания
    private Vector3 lp;   //Последняя позиция касания
    int dragDistance = 10;
    Transform from;
    Transform to;
    Quaternion qto;

    void Start ()
    {
        from = transform;
        to = transform;
        qto = new Quaternion(0,0,0,0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            fp = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            lp = Input.mousePosition;
            if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
            { 
                if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                {   if ((lp.x > fp.x))
                    {
                        qto = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y - 90, 0);
                    }
                    else
                    qto= Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y + 90, 0);
                }

            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, qto, (float)(Time.time*0.01));

    }

}
