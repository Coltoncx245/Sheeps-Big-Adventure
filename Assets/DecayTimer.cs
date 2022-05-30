using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayTimer : MonoBehaviour
{
    public Timer t;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        t.Start(5);
        if (t.Done())
        {
            Destroy(gameObject);
        }
        
        
        
    }
}
