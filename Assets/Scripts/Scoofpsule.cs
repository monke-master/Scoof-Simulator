using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoofpsule : MonoBehaviour
{
    private static float minY = -10f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (transform.position.y < minY)
        {
            Destroy(this.gameObject);
            var basketScript = GameObject.Find("Basket").GetComponent<Basket>();
            basketScript.OnScoofpsuleDestroyed();
        }
    }
}
