using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilelHitbox : MonoBehaviour
{
    public int damageAmount;
    public float maxTime;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > maxTime)
        {
            timer = 0;
            DestroyFireball();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttributes otherAttributes = other.GetComponent<IAttributes>();

        if (otherAttributes != null)
        {
            otherAttributes.TakeDamage(damageAmount, true);
            DestroyFireball();
        }
    }

    public void DestroyFireball()
    {
        Destroy(this.gameObject);
    }
}
