using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float amp;

    float init_y;
    float velocity;

    // Start is called before the first frame update
    void Start()
    {
        init_y = transform.position.y;
        velocity = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float y = transform.position.y + velocity;
        if (y > init_y + Mathf.Abs(amp))
        {
            y = init_y + Mathf.Abs(amp);
            velocity *= -1.0f;
        }
        if (y < init_y - Mathf.Abs(amp))
        {
            y = init_y - Mathf.Abs(amp);
            velocity *= -1.0f;
        }

        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
