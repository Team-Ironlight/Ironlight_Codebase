using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ROFO
{
    public class MultiStateMachine : MonoBehaviour
    {
        //two different AIs
        //private AI moveAI;
        //private AI attackAI;
        private AI[] allAI;

        //public IState currentState;
        //current state for each ai
        //public IState currentMove;
        //public IState currentAttack;

        private Transform parent;

        private void Awake()
        {
            List<AI> temp = new List<AI>();
            //get all AIs on this object, should be child objects
            foreach (Transform child in transform)
            {
                if (child.GetComponent<AI>())
                {
                    temp.Add(child.GetComponent<AI>());
                }
            }

            allAI = temp.ToArray();

            //get ai
            //find for each
            //foreach(Transform child in transform)
            //{
            //    if(child.name == "Movement")
            //    {
            //        moveAI = child.GetComponent<AI>();
            //    }
            //    else if(child.name == "Attack")
            //    {
            //        attackAI = child.GetComponent<AI>();
            //    }
            //}
        }



        private void Start()
        {
            parent = transform.parent;

            //sets starting state based on states this enemy has
            //currentMove = new None();
            //currentAttack = new None();
        }

        private void Update()
        {
            Run();
        }

        private void Run()
        {
            for (int i = 0; i < allAI.Length; i++)
            {
                if (allAI[i].lastState != allAI[i].CheckContainer(allAI[i].GetContainer()))
                {
                    allAI[i].lastState.Exit();
                    allAI[i].lastState = allAI[i].CheckContainer(allAI[i].GetContainer());
                    allAI[i].lastState.Enter();
                }
                else
                {
                    allAI[i].lastState.Execute(parent);
                }
            }


            ////check moveAI
            ////check if AI has changed the state, exit and enter
            //if (currentMove != moveAI.CheckContainer(moveAI.GetContainer()))
            //{
            //    currentMove.Exit();
            //    currentMove = moveAI.CheckContainer(moveAI.GetContainer());
            //    currentMove.Enter();
            //}
            ////run execute of current state
            //else
            //{
            //    currentMove.Execute(parent);
            //}

            ////check attackAI
            ////check if AI has changed the state, exit and enter
            //if (currentAttack != attackAI.CheckContainer(attackAI.GetContainer()))
            //{
            //    currentAttack.Exit();
            //    currentAttack = attackAI.CheckContainer(attackAI.GetContainer());
            //    currentAttack.Enter();
            //}
            ////run execute of current state
            //else
            //{
            //    currentAttack.Execute(parent);
            //}
        }
    }    
}
