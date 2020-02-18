using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iman.Owl
{


    public class TestVector : MonoBehaviour
    {
        public GameObject Player;

        public int YPos;
        public int GroundPos;
        private Vector3 startPos;

        public Vector3 SweepEndPos;
        private Vector3 PlayerAttackPos;
        private Vector3 Target;
        public bool Attack;

        Vector3 owlpos;
        // Start is called before the first frame update
        void Start()
        {
            Attack = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!Attack)
            {
                if (Vector3.Distance(startPos, transform.position) > 0.1)
                {
                    var direction = startPos - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3 * Time.deltaTime);
                    //move forward
                    transform.Translate(0, 0, Time.deltaTime * 4);
                }
                else
                {
                    var direction = Player.transform.position - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3 * Time.deltaTime);
                }
                calculateAttackPositions();
            }

            if (Attack)
            {
                if (Vector3.Distance(PlayerAttackPos, transform.position) < 0.1)
                {
                    Target = SweepEndPos;
                }
                if (Vector3.Distance(SweepEndPos, transform.position) < 0.01)
                {
                    Attack = false;
                }
                var direction = Target - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 4 * Time.deltaTime);
                //move forward
                transform.Translate(0, 0, Time.deltaTime * 7);
            }
            calculate();
        }

        void calculate()
        {
            owlpos = transform.position;
            owlpos.y = Player.transform.position.y;
            Vector3 dist = -(Player.transform.position - owlpos);
            dist = dist.normalized * GroundPos;
            dist = dist + Player.transform.position;
            dist.y = Player.transform.position.y + YPos;
            startPos = dist;
        }

        private void calculateAttackPositions()
        {
            //End owl Pos
            var PPos = Player.transform.position;
            PPos.y = transform.position.y;
            SweepEndPos = ((PPos - transform.position).normalized * GroundPos) + Player.transform.position;
            SweepEndPos.y = Player.transform.position.y + YPos;

            //PlayerPos
            PlayerAttackPos = Player.transform.position;

            Target = PlayerAttackPos;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, SweepEndPos);
        }
    }
}
