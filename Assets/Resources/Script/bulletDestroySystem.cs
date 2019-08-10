using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestroySystem : MonoBehaviour
{
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            //Destroy(other.gameObject);
        }else if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
