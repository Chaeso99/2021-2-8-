using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyDestroy : MonoBehaviour
{
    void Update()
    {
        if(gameObject.transform.position.x<=-20f)
        {
            Destroy(gameObject);
        }
    }
}
