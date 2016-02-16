using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dis = Input.mousePosition - objectPos;
        Quaternion num = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg));
        //            if (num.z > 0.2f)
        //            {
        //                num = Quaternion.Euler(num.x, num.y, 0.2f);
        //            }
        //
        //            if (num.z < -0.2f)
        //            {
        //                num = Quaternion.Euler(num.x, num.y, -0.2f);
        //            }
        /*if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-0.08f, 0.08f, 1);
            //transform.position = new Vector3(0.08f, transform.position.y,0);
        }*/

        transform.localRotation = num;
    }


}
