using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public void Collision(string c)
    {
        if (c == "wall")
        {
            print("Collided with wall!");
        }
    }
}
