using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySample : MonoBehaviour
{
    Vector2 position = new Vector2(0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position += new Vector2(0.01f, 0);
        transform.position = position;
    }
}
