using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mushroom : MonoBehaviour
{
    
    public GameObject scoofpsulePrefab;

    public float speed = 10f;

    public float leftAndRightEdge = 10f;

    public float chanceToChangeDirections = 0.02f;

    public float secondsBetweenAppleDrops = 1f;
    
    
    void Start()
    {
        Invoke(nameof(DropScoofpsule), 2f);
    }

    void DropScoofpsule()
    {
        GameObject scoofpsule = Instantiate(scoofpsulePrefab);
        scoofpsule.transform.position = transform.position;
        Invoke(nameof(DropScoofpsule), secondsBetweenAppleDrops);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x > leftAndRightEdge || pos.x < -leftAndRightEdge)
            speed *= -1;

        
    }

    private void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections)
            speed *= -1;
    }
}
