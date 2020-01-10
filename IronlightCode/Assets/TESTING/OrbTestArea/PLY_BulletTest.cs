using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_BulletTest : MonoBehaviour
{
    //bullet components
    [SerializeField] private int _iDisableTimer;
    [SerializeField] private int _iSpeed;
    [SerializeField] private int _iDamageAmount;

    //explosion
    [SerializeField] private int _iExplosionDmg;
    private GameObject _goCollidedObject;
    [SerializeField] private int _iExplosionSphereRadius;

    public void StartOrb(Vector3 pDir)
    {
        //activate bullet
        gameObject.SetActive(true);

        //add force to bullets rigidbody in the right direction
        GetComponent<Rigidbody>().velocity = pDir * _iSpeed;

        //deactivate after 5 seconds
        StartCoroutine("BulletDisable");
    }

    private void Explosion()
    {
        //get all colliders in the sphere
        foreach(Collider pcollider in Physics.OverlapSphere(transform.position, _iExplosionSphereRadius))
        {
            IAttributes cIA = pcollider.gameObject.GetComponent<IAttributes>(); 
            //check if gameobject is damagable
            if(cIA != null)
            {
                //if gameobject is not the collided game object to avoid damaging twice
                if (_goCollidedObject != pcollider.gameObject)
                    cIA.TakeDamage(_iExplosionDmg, false);
            }
        }
    }

    IEnumerator BulletDisable()
    {
        //suspend execution for 5 seconds
        yield return new WaitForSeconds(_iDisableTimer);

        gameObject.SetActive(false);

        //set velocity to 0
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if collided object has IAtributes do damage
        if(collision.gameObject.GetComponent<IAttributes>() != null)
        {
            collision.gameObject.GetComponent<IAttributes>().TakeDamage(_iDamageAmount, false);
            _goCollidedObject = collision.gameObject;
        }

        //Explosion
        Explosion();

        //deactivate object
        gameObject.SetActive(false);

        //set velocity to 0
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //stop 
        StopCoroutine("BulletDisable");
    }
}
