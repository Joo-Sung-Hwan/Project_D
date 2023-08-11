using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack : MonoBehaviour
{
    [SerializeField] SingleAttack sa;
    public Monster target;

    public float damage = 100;
    void Start()
    {
        
    }

    void Update()
    {
        Find();
    }

    public void Find()
    {
        target = FindObjectOfType<Monster>();
        if (target != null)
        {
            transform.Translate(Vector3.right * 4 * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            target.md.curHP -= damage;
            Destroy(gameObject);
        }
    }
}
