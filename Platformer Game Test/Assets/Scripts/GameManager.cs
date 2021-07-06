using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] PlatformGrasses;
    public int  PlatformSpawnCount;
    public Vector3 EndPointTransform;



    // Start is called before the first frame update

    public void PlatformSpawner()
    {
        
        for (int i = 0; i < PlatformSpawnCount; i++)
        {

            GameObject platform = GameObject.Instantiate(PlatformGrasses[Random.Range(0, PlatformGrasses.Length)]);            
            Platform platformScrpit = platform.GetComponent<Platform>();
            platform.transform.position = EndPointTransform;
            EndPointTransform = platformScrpit.ReturnEndPoint();
            
        }



    }
    void Start()
    {
        PlatformSpawner();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
