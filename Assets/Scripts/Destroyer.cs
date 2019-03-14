using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ObjectDestroying(GameObject go);

public class Destroyer : MonoBehaviour
{
    public event ObjectDestroying ObjectDestroyingEvent;

    private void OnTriggerEnter(Collider other)
    {
        ObjectDestroyingEvent(other.gameObject);
        Destroy(other.gameObject);
    }
}
