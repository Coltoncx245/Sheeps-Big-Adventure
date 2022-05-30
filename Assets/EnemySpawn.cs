using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawn : MonoBehaviour
{

    public GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {

       Instantiate(enemy, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
