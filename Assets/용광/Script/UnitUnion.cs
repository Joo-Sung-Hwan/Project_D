using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    None,
    Fire,
    Wind,
    Earth,
    Water
}

public class UnitUnion : MonoBehaviour
{
    public UnitType uType = UnitType.None;
    [SerializeField] MovableObj movable;

    bool canAttack = true;
    bool union;
    public int level = 0;
    float first = 0;
    float atk = 30;
    float atkDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        uType = RandomValue();
    }

    // Update is called once per frame
    void Update()
    {
        //if (movable.block.isWaiting || !MapManager.instance.monsterManager.isWave)
        //return;

        if (canAttack)
            StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        first = 0;
        Collider[] monsters = Physics.OverlapSphere(transform.position, 2);
        Monster first_mob = null;
        foreach (var item in monsters)
        {
            Monster mob;
            if (mob = item.GetComponent<Monster>())
            {
                if (first <= mob.moved)
                {
                    first = mob.moved;
                    first_mob = mob;
                }
            }
        }
        if (first_mob == null)
            yield break;

        //first_mob.Damaged(atk);
        SplashAttack(first_mob);

        canAttack = false;
        StartCoroutine(ColorChange());
        yield return new WaitForSeconds(atkDelay);
        canAttack = true;
    }

    IEnumerator ColorChange()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    void SplashAttack(Monster target)
    {
        Collider[] monsters = Physics.OverlapSphere(target.transform.position, 1);
        foreach (var item in monsters)
        {
            Monster mob;
            if (mob = item.GetComponent<Monster>())
                mob.Damaged(atk, Damage_Type.physic);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
    }

    private void OnMouseDown()
    {
        union = true;
    }
    private void OnMouseUp()
    {
        union = false;
    }

    //Enum 랜덤값부여 스크립트
    public UnitType RandomValue()
    {
        var enumValues = System.Enum.GetValues(enumType: typeof(UnitType));
        return (UnitType)enumValues.GetValue(Random.Range(1, enumValues.Length));
    }

    //유닛 잡아먹는 방식
    private void OnTriggerEnter(Collider other)
    {
        if (union)
        {
            if (!other.GetComponent<UnitUnion>())
                return;

            UnitUnion otherUnit = other.GetComponent<UnitUnion>();
            if (uType == otherUnit.uType && level == otherUnit.level && level < 3)
            {
                otherUnit.level++;
                Destroy(gameObject);
            }
        }

        /*
        if (union == true)
        {
            switch (uType)
            {
                case UnitType.Fire:
                    if (other.gameObject.GetComponent<UnitUnion>().uType == UnitType.Fire && level == 1)
                    {
                        if (other.gameObject.GetComponent<UnitUnion>().level == 2)
                            return;
                        else if (other.gameObject.GetComponent<UnitUnion>().level == 3)
                            return;

                        level = 2;
                        Destroy(other.gameObject);
                    }
                    else if (other.gameObject.GetComponent<UnitUnion>().uType == UnitType.Fire && level == 2)
                    {
                        if (other.gameObject.GetComponent<UnitUnion>().level == 1)
                            return;
                        else if (other.gameObject.GetComponent<UnitUnion>().level == 3)
                            return;

                        level = 3;
                        Destroy(other.gameObject);
                    }
                    break;
                case UnitType.Wind:
                    if (other.gameObject.GetComponent<UnitUnion>().uType == UnitType.Wind && level == 1)
                    {
                        if (other.gameObject.GetComponent<UnitUnion>().level == 2)
                            return;
                        else if (other.gameObject.GetComponent<UnitUnion>().level == 3)
                            return;

                        level = 2;
                        Destroy(other.gameObject);
                    }
                    else if (other.gameObject.GetComponent<UnitUnion>().uType == UnitType.Wind && level == 2)
                    {
                        if (other.gameObject.GetComponent<UnitUnion>().level == 1)
                            return;
                        else if (other.gameObject.GetComponent<UnitUnion>().level == 3)
                            return;

                        level = 3;
                        Destroy(other.gameObject);
                    }
                    break;
                case UnitType.Earth:
                    if (other.gameObject.GetComponent<UnitUnion>().uType == UnitType.Earth && level == 1)
                    {
                        if (other.gameObject.GetComponent<UnitUnion>().level == 2)
                            return;
                        else if (other.gameObject.GetComponent<UnitUnion>().level == 3)
                            return;

                        level = 2;
                        Destroy(other.gameObject);
                    }
                    else if (other.gameObject.GetComponent<UnitUnion>().uType == UnitType.Earth && level == 2)
                    {
                        if (other.gameObject.GetComponent<UnitUnion>().level == 1)
                            return;
                        else if (other.gameObject.GetComponent<UnitUnion>().level == 3)
                            return;

                        level = 3;
                        Destroy(other.gameObject);
                    }
                    break;
                case UnitType.Water:
                    if (other.gameObject.GetComponent<UnitUnion>().uType == UnitType.Water && level == 1)
                    {
                        if (other.gameObject.GetComponent<UnitUnion>().level == 2)
                            return;
                        else if (other.gameObject.GetComponent<UnitUnion>().level == 3)
                            return;

                        level = 2;
                        Destroy(other.gameObject);
                    }
                    else if (other.gameObject.GetComponent<UnitUnion>().uType == UnitType.Water && level == 2)
                    {
                        if (other.gameObject.GetComponent<UnitUnion>().level == 1)
                            return;
                        else if (other.gameObject.GetComponent<UnitUnion>().level == 3)
                            return;

                        level = 3;
                        Destroy(other.gameObject);
                    }
                    break;
            }
        }*/
    }
}

