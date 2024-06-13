using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollision : MonoBehaviour
{
    public Hint hint;
    public Transform magnet;
    public float posX;
    public float posY;
    public bool magneticItem = false;

    public Transform[] magnetAnchor;
    public int indexAnchor;

    public static MagnetCollision instance;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        posX = magnet.position.x;
        posY = magnet.position.y;
    }
}
