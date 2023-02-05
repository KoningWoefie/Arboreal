using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damageDealtTxt : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject TextObj;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        //slowly fadeout the text colour (a child of this object) over time
        TextObj = transform.GetChild(0).gameObject;
        // the text has an sprite renderer component, which has a colour
        TextObj.GetComponent<SpriteRenderer>().color = new Color(TextObj.GetComponent<SpriteRenderer>().color.r, TextObj.GetComponent<SpriteRenderer>().color.g, TextObj.GetComponent<SpriteRenderer>().color.b, TextObj.GetComponent<SpriteRenderer>().color.a - 0.01f);
        if (TextObj.GetComponent<SpriteRenderer>().color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
