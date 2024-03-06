using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace AphroditeFightCode
{
    public class AttackPlayer : StateMachineBehaviour
    {
        float timeElasped = 0;
        public GameObject playerObject;
        MinionScript minionVariables;
        float minionAttackTime;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            minionVariables = animator.GetComponentInParent<MinionScript>();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            minionAttackTime = minionVariables.attackSpeed;
            //Attack(playerObject, minionVariables.strength);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timeElasped += Time.deltaTime;
            if (timeElasped >= minionAttackTime)
            {
                Attack(playerObject, minionVariables.strength);
                timeElasped = 0;
            }

        }

        void Attack(GameObject playerObject, float minionDamage)
        {

            if (!GameData.freezePlayer)
            {


                Color originalColor = playerObject.GetComponent<SpriteRenderer>().color;
                Color flashColor = Color.red;
                float flashDuration = 0.1f;
                int numberOfFlashes = 2;
                AudioSource.PlayClipAtPoint(minionVariables.audioClips[0], playerObject.transform.localPosition);

                LeanTween.value(playerObject, originalColor, flashColor, flashDuration)
                   .setEase(LeanTweenType.easeInOutSine)
                   .setLoopPingPong(numberOfFlashes)
                   .setOnUpdate((Color val) =>
                   {
                   // Update the object's color during the tween
                   playerObject.GetComponent<SpriteRenderer>().color = val;
                   })
                   .setOnComplete(() =>
                   {
                   // Reset the color to the original after the flash is complete
                   playerObject.GetComponent<SpriteRenderer>().color = Color.white;
                   });
                GameData.CheckPlayerHealth(minionDamage);
                Debug.Log("Player taking " + minionDamage + " damage. Curr Player Health = " + GameData.playerHeath);
            }
            //playerObject.GetComponent<QuickPlayerMove>().health -= minionDamage;
            //Debug.Log("Player took " + minionDamage + " damage. "); // + playerObject.GetComponent<QuickPlayerMove>().health + " health remaining.");
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

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
    }
}
