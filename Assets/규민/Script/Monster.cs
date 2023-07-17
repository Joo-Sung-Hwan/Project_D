using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    int index = 0;
    int preIndex = 0;
    float speed = 1;
    float rotated = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //index - 왼쪽:0, 아래:1, 오른쪽:2, 위:3;
        switch (index)
        {
            case 0:
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                if (transform.position.x >= 7)
                    index = -1;
                break;
            case 1:
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                if (transform.position.z >= 7)
                {
                    index = -1;
                    preIndex = 1;
                }
                break;
            case 2:
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                if (transform.position.x <= 1)
                {
                    index = -1;
                    preIndex = 2;
                }
                break;
            case 3:
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                if (transform.position.z <= 2)
                    Destroy(gameObject);
                break;
            default:
                transform.Rotate(new Vector3(0, -1, 0) * 120 * Time.deltaTime) ;
                rotated += 120 * Time.deltaTime;
                if (rotated >=90)
                {
                    index = preIndex + 1;
                    rotated = 0;
                }
                break;
        }
    }
}
