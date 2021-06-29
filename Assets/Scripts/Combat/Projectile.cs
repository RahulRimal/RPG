using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    
    Health target = null;
    float damage = 0f;

    // Update is called once per frame
    void Update()
    {
        if(target == null)
            return;
        
        transform.LookAt(getAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void setTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    Vector3 getAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule == null)
            return target.transform.position;

        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != target)
            return;

        target.damage(damage);
        Destroy(gameObject);

    }




     
}
