using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace AphroditeFightCode
{
    public class ChasePlayer : StateMachineBehaviour
    {

        public GameObject playerObject;
        MinionScript minionVariables;

        //public bool isTrue = true;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            minionVariables = animator.GetComponentInParent<MinionScript>();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            minionVariables.GetComponentInParent<AIPath>().enabled = true;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // animator.gameObject.transform.position = Vector2.MoveTowards(animator.transform.position, playerObject.transform.position, minionVariables.moveSpeed * Time.deltaTime);
            /*animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerObject.transform.position, minionVariables.moveSpeed * Time.deltaTime);*/
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            minionVariables.GetComponentInParent<AIPath>().enabled = false;
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}

        /*private void FollowPlayer(GameObject playerObject)
       {
           //Vector2 enemyDirection = playerCollider.GameObject().;
           //move the parent object to follow the player
           minionVariables.gameObject.transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, minionBase.moveSpeed * Time.deltaTime);


       }
    }*/
    }
}
