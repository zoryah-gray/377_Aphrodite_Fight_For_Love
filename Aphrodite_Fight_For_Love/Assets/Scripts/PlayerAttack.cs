using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace AphroditeFightCode
{
    public class PlayerAttack : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        //public UnityEngine.Transform meleeBox;
        public GameObject meleeBoxGO;
        public GameObject bullet;
        public float bulletSpeed = 10f;
        public UnityEngine.Transform attackBox;
        public LayerMask enemyLayers;
        public Vector3 curPosition;
        public bool isAttacking;
        private float lastShotGun;
        public float gunInterval = 0.5f;
        private Animator meleeBoxAnimator;
        private Animator playerAnimator;
        private Rigidbody2D rb;
        public GameObject restartScreen;
        public GameObject diedScreen;
        Vector2 rectangleSize = new Vector2(0.9f, 0.45f);

        public float health = 10f;
        [Header("Controlling Weapon")]
        public int curWeapon;
        public Image GUISlot1;
        public Image GUISlot2;
        [SerializeField] private Vector3 activeSlotOriginalPos;
        [SerializeField] private Vector3 nonActiveSlotOriginalPos;

        [Header("Audio Clips")]
        public AudioClip rangedClip;


        void Start()
        {
            meleeBoxAnimator = meleeBoxGO.GetComponent<Animator>();
            playerAnimator = GetComponent<Animator>();
            bullet.GetComponent<SpriteRenderer>().enabled = false;

            activeSlotOriginalPos = GUISlot1.rectTransform.position;

            nonActiveSlotOriginalPos = GUISlot2.rectTransform.position;

            //curWeapon = 0; // initialized to melee
            //GameData.currWeapon = 0;
            if (GameData.currWeapon == 0)
            {
                // Move active in front of nonActive
                GUISlot1.transform.SetSiblingIndex(GUISlot2.transform.GetSiblingIndex() + 1);

                Color slotColor = GUISlot2.color;
                slotColor.a = 0.5f;
                GUISlot2.color = slotColor;

                Color slot1Color = GUISlot1.color;
                slot1Color.a = 1f;
                GUISlot1.color = slot1Color;

                GUISlot1.rectTransform.position = activeSlotOriginalPos;

                GUISlot2.rectTransform.position = nonActiveSlotOriginalPos;

            }
            else
            {
                // Move active in front of nonActive
                GUISlot2.transform.SetSiblingIndex(GUISlot1.transform.GetSiblingIndex() + 1);

                Color slotColor = GUISlot1.color;
                slotColor.a = 0.5f;
                GUISlot1.color = slotColor;

                Color slot2Color = GUISlot2.color;
                slot2Color.a = 1f;
                GUISlot2.color = slot2Color;

                GUISlot2.rectTransform.position = activeSlotOriginalPos;

                GUISlot1.rectTransform.position = nonActiveSlotOriginalPos;
            }

            

        }

        private void OnEnable()
        {
            if (!PlayerInputsSingleton.PlayerInputsInstance.Player.enabled)
            {
                PlayerInputsSingleton.PlayerInputsInstance.Player.Enable();
            }
            PlayerInputsSingleton.PlayerInputsInstance.Player.Attack.performed += OnAttackPerformed;
            PlayerInputsSingleton.PlayerInputsInstance.Player.Melee.performed += OnMeleePerformed;
            PlayerInputsSingleton.PlayerInputsInstance.Player.Shoot.performed += OnShootPerformed;
        }

        private void OnDisable()
        {
            if (PlayerInputsSingleton.PlayerInputsInstance.Player.enabled)
            {
                PlayerInputsSingleton.PlayerInputsInstance.Player.Disable();
            }
            PlayerInputsSingleton.PlayerInputsInstance.Player.Attack.performed -= OnAttackPerformed;
            PlayerInputsSingleton.PlayerInputsInstance.Player.Melee.performed -= OnMeleePerformed;
            PlayerInputsSingleton.PlayerInputsInstance.Player.Shoot.performed -= OnShootPerformed;

        }

        private void OnAttackPerformed(InputAction.CallbackContext val)
        {
            if (curWeapon == 0)
            {
                meleeBoxAnimator.SetBool("animCanAttack", true);
                curPosition = meleeBoxGO.transform.position;
                Attack();
                //Debug.Log("Attack!");
                StartCoroutine(ResetAfterAnim());

                //Debug.Log(health + "health cur");
                //Debug.Log(GameData.playerHeath + " health cur");
            }
            if (Time.time - lastShotGun >= gunInterval && curWeapon == 1)
            {
                ShootGun();
                lastShotGun = Time.time;
            }
        }

        private void OnMeleePerformed(InputAction.CallbackContext val)
        {
            // KeyCode.Alpha1
            curWeapon = 0;
            //GameData.currWeapon = 0;
            SwitchSlots(1);
        }

        private void OnShootPerformed(InputAction.CallbackContext val)
        {
            // KeyCode.Alpha2
            curWeapon = 1;
            //GameData.currWeapon = 1;
            SwitchSlots(2);
        }

        private void SwitchSlots(int activeSlotInt)
        {
            // if making weapon1 active but its already active ignore
            int slot = activeSlotInt - 1;
            //Debug.Log("switching from weapon " + GameData.currWeapon + " to " + slot);
            if (GameData.currWeapon == activeSlotInt - 1)
            {
                // the weapon we are trying to switch to is already the current weapon
                return;
            }

            Image activeSlot;
            //Vector3 activeSlotTargetPos;
            Image nonActiveSlot;
            //Vector3 nonActiveSlotTargetPos;
            if (activeSlotInt == 1)
            {
                activeSlot = GUISlot1;

                nonActiveSlot = GUISlot2;
            }
            else 
            {
                //(activeSlot == 2)
                activeSlot = GUISlot2;

                nonActiveSlot = GUISlot1;
            }

            // Move active in front of nonActive
            activeSlot.transform.SetSiblingIndex(nonActiveSlot.transform.GetSiblingIndex() + 1);
            // decrease alpha of nonActive
            Color slot2Color = nonActiveSlot.color;
            slot2Color.a = 0.5f;
            nonActiveSlot.color = slot2Color;

            Color slot1Color = activeSlot.color;
            slot1Color.a = 1f;
            activeSlot.color = slot1Color;

            

            RectTransform rectTransform1 = activeSlot.rectTransform;
            
            RectTransform rectTransform2 = nonActiveSlot.rectTransform;
            Debug.Log("-> active slot currently at pos (" + rectTransform1.position + ") needs to move to pos (" + activeSlotOriginalPos + ")");

            // active (1) -> nonactive (2)
            LeanTween.move(activeSlot.gameObject, activeSlotOriginalPos, 1f)
                    .setEase(LeanTweenType.easeInOutQuad);

            // nonactive(2) -> active (1)
            LeanTween.move(nonActiveSlot.gameObject, nonActiveSlotOriginalPos, 1f)
                        .setEase(LeanTweenType.easeInOutQuad);

            //LeanTween.move(rectTransform1.gameObject, rectTransform2.position, 1f)
            //        .setEase(LeanTweenType.easeInOutQuad);

            //LeanTween.move(rectTransform2.gameObject, rectTransform1.position, 1f)
            //            .setEase(LeanTweenType.easeInOutQuad);


            GameData.currWeapon = activeSlotInt - 1;


        }

        // Update is called once per frame
        void Update()
        {
            if (GameData.playerHeath <= 0f && !GameData.playerDead)
            {
                PlayerDeath();
            }
        }

        private void PlayerDeath()
        {
            GameData.playerDead = true;
            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            playerAnimator.SetTrigger("death");
            PlayerInputsSingleton.PlayerInputsInstance.Player.Disable();
            if (SceneManager.GetActiveScene().name == "HadesBossCombined")
            {
                Respawn();
            }
            else
            {
                diedScreen.SetActive(true);
            }
            //Destroy(gameObject);
        }

        public void OpenRestartScreen()
        {
            restartScreen.SetActive(true);
        }

        public void Respawn()
        {
            GameData.ongoingQuests.Clear();
            GameData.completedQuests.Clear();
            GameData.playerHeath = 60f;
            GameData.onHeart = 1;
            GameData.playerDead = false;
            restartScreen.SetActive(false);
            diedScreen.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void ShootGun()
        {
            var bullInitPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            AudioSource.PlayClipAtPoint(rangedClip, transform.localPosition);
            GameObject bulletGO = Instantiate(bullet, bullInitPos, Quaternion.identity);
            bulletGO.tag = "Bullet";
            bulletGO.GetComponent<SpriteRenderer>().enabled = true;
            Rigidbody2D bulletRB = bulletGO.GetComponent<Rigidbody2D>();
            

            if (playerMovement.directionInt == 1) //Up
            {
                bulletGO.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                bulletRB.velocity = transform.up * bulletSpeed;
            }
            else if (playerMovement.directionInt == 2) //Right
            {
                bulletRB.velocity = transform.right * bulletSpeed;
            }
            else if (playerMovement.directionInt == 3) //Down
            {
                bulletGO.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                bulletRB.velocity = ((-1) * transform.up) * bulletSpeed;
            }
            else if (playerMovement.directionInt == 4) //Left
            {
                bulletGO.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                bulletRB.velocity = ((-1) * transform.right) * bulletSpeed;
            }
        }

        void Attack()
        {
            // do correct animation
            //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeBox.position, 4.0f, enemyLayers);
            if (playerMovement.directionInt == 1) //Up
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            }
            else if (playerMovement.directionInt == 2) //Right
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 180f, 180f);
            }
            else if (playerMovement.directionInt == 3) //Down
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else if (playerMovement.directionInt == 4) //Left
            {
                meleeBoxGO.transform.position = new Vector3(transform.position.x - 0.4f, transform.position.y, transform.position.z);
                meleeBoxGO.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            Debug.Log(meleeBoxGO.transform.position + "cur pos" + enemyLayers.ToString());

            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackBox.position, rectangleSize, 0f, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.tag == "Hestia")
                {
                    enemy.gameObject.GetComponent<Hestia>().TakeDamage(1);
                    Debug.Log("We hit Hestia!" + enemy.name);
                }

                else if (enemy.gameObject.tag == "Hades")
                {
                    enemy.gameObject.GetComponent<Hades>().TakeDamage(1);
                    Debug.Log("We hit Hades!" + enemy.name);
                }
                else
                { 
                    enemy.gameObject.GetComponent<MinionScript>().TakeDamage(1);
                    Debug.Log("We hit an enemy!" + enemy.name);
                }
            }
        }

        private IEnumerator ResetAfterAnim()
        {
            yield return new WaitForSeconds(0.1f);
            meleeBoxAnimator.SetBool("animCanAttack", false);
        }

        private void OnDrawGizmosSelected()
        {
            //    Gizmos.DrawWireCube(attackBox.position, new Vector2(1.1f, 0.6f));
            Gizmos.DrawWireSphere(meleeBoxGO.transform.position, 0.5f);
            //    Gizmos.DrawCube(curPosition, new Vector2(2f, 2f));

        }
    }
}






//Transform:
//void Start()
//{
//    meleeBox = transform.Find("MeleeBox");
//}
//// Update is called once per frame
//void Update()
//{
//    if (playerMovement.directionInt == 1) //Up
//    {
//        meleeBox.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
//        meleeBox.rotation = Quaternion.Euler(0f, 0f, 270f);
//    }
//    else if (playerMovement.directionInt == 2) //Right
//    {
//        meleeBox.position = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
//        meleeBox.rotation = Quaternion.Euler(0f, 180f, 180f);
//    }
//    else if (playerMovement.directionInt == 3) //Down
//    {
//        meleeBox.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
//        meleeBox.rotation = Quaternion.Euler(0f, 0f, 90f);
//    }
//    else if (playerMovement.directionInt == 4) //Left
//    {
//        meleeBox.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
//        meleeBox.rotation = Quaternion.Euler(0f, 0f, 0f);
//    }
