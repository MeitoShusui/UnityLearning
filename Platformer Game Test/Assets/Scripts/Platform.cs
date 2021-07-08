using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;


    public Vector3 ReturnEndPoint()
    {
        Vector3 calculatedEndPoint;
        calculatedEndPoint.x = spriteRenderer.bounds.size.x + this.transform.position.x + Random.Range(0,5);
        calculatedEndPoint.y = Random.Range(2,-3);
        calculatedEndPoint.z = 0;
        return calculatedEndPoint;


    }

 

}
