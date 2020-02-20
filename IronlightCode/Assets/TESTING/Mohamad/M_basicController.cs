using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Tools;
using Sharmout.attacks;


namespace Sharmout
{


    public class M_basicController : MonoBehaviour
    {
        public bool fire = false;
        attacks.R_OrbAttack orbAttack;
       
        public dObjectPooler pooler;

        
        

        public string tagName = "Orb";

        // Start is called before the first frame update
        void Start()
        {
            orbAttack = new attacks.R_OrbAttack();

            

            pooler = GetComponent<dObjectPooler>();
        }

        // Update is called once per frame
        void Update()
        {
            GetInput();


            if (fire)
            {
                
                orbAttack.Go_bitch(pooler, tagName, this.transform);
                fire = false;
            }
        }

        void GetInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fire = true;
            }
        }
    }
}