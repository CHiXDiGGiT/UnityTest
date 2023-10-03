using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRight : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float amp;

    float init_z;
    float velocity;

    // Start is called before the first frame update
    void Start()
    {
        init_z = transform.position.z;
        velocity = speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float z = transform.position.z + velocity;
        if (z > init_z + Mathf.Abs(amp))
        {
            z = init_z + Mathf.Abs(amp);
            velocity *= -1.0f;
        }
        if (z < init_z - Mathf.Abs(amp))
        {
            z = init_z - Mathf.Abs(amp);
            velocity *= -1.0f;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
}
