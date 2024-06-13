using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetObject : MonoBehaviour
{
    public Hint hint;
    public bool startMagnet = false;
    private int indexAnchor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MagnetCollision.instance.magneticItem && startMagnet)
        {
            transform.position = new Vector3(MagnetCollision.instance.magnetAnchor[indexAnchor].position.x, MagnetCollision.instance.magnetAnchor[indexAnchor].position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Object")
        {
            startMagnet = true;
            indexAnchor = MagnetCollision.instance.indexAnchor;
            MagnetCollision.instance.indexAnchor += MagnetCollision.instance.indexAnchor < 6 ? 1 : 0;
        }
    }
}
