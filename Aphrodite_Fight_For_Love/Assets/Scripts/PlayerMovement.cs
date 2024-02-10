using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AphroditeFightCode
{
    public class PlayerMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        private PlayerInputs input = null;
        private Vector2 moveVector = Vector2.zero;
        private Rigidbody2D rb = null;
<<<<<<< Updated upstream
        private float moveSpeed = 10f;
=======
        private float moveSpeed = 4f;

        [Header("Attached Objects and References")]
        public Animator animator;
        public GameObject playerGO;
        public GameObject meleeBoxGO;

        [Header("Scene Camera")]
        [SerializeField] private Camera sceneCamera;

        [Header("Movement Animation Paramters")]
        public int directionInt = 0;
        public bool animAfterLeft = true;

        [Header("Click Collision Control")]
        public int collisionCount;
        public float collisionOffset = 0.5f;
        public float clickDist = 1f;

        public ContactFilter2D clickFilter;
        List<RaycastHit2D> clickCastCollisions = new List<RaycastHit2D>();

        //[Header("Collision Control")]
        //public int collisionCount;
        //public float collisionOffset = 0.5f;
        
        ////public ContactFilter2D movementFilter;
        //public ContactFilter2D movementFilter;
        //List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

        //[Header("Collision Control Attempt 2")]
        //public float circleCastRadius = 5f;
        //public List<RaycastHit2D> collisionRslts = new List<RaycastHit2D>();
        //public ContactFilter2D castCollisions2;
        //public Vector2 boxCastSize;
        //public Vector3 boxOffset;




>>>>>>> Stashed changes
        private void Awake()
        {
            input = new PlayerInputs();
            rb = GetComponent<Rigidbody2D>();
<<<<<<< Updated upstream
=======
            input.Player.Enable();
            sceneCamera = Camera.main;

            // lock the cursor and turn it invisible
            // => in playMode if you want to click away press 'Esc'
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
>>>>>>> Stashed changes
        }
        private void OnEnable()
        {
            input.Enable();
            input.Player.Movement.performed += OnMovementPerformed;
            input.Player.Movement.canceled += OnMovementCancelled;
            input.Player.Click.performed += OnClickPerformed;
        }
        private void OnDisable()
        {
            input.Disable();
            input.Player.Movement.performed -= OnMovementPerformed;
            input.Player.Movement.canceled -= OnMovementCancelled;
        }
        private void FixedUpdate()
        {
            rb.velocity = moveVector * moveSpeed;
        }
<<<<<<< Updated upstream
=======

        public void AddKey(ClickableKey key)
        {
            //print out what the key is
            string keyName = key.keyName;
            string questName = key.keyQuestName;
            KeyQuestManager quest = key.quest;
            Debug.Log("This is the key " + keyName + "from the Quest " + questName + " (adding it)");
            key.AddToQuest(quest);

        }




        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = transform.position + new Vector3(moveVector.x, moveVector.y, 0);
            Gizmos.DrawLine(startPosition, endPosition);

            Vector3 cubeCenter = endPosition + new Vector3(0, 0, collisionOffset * 0.5f);
            Vector3 cubeSize = new Vector3(collisionOffset, collisionOffset, collisionOffset);
            Gizmos.DrawWireCube(cubeCenter, cubeSize);
        }

>>>>>>> Stashed changes
        private void OnMovementPerformed(InputAction.CallbackContext val)
        {
            moveVector = val.ReadValue<Vector2>();
        }
        private void OnMovementCancelled(InputAction.CallbackContext val)
        {
            moveVector = Vector2.zero;
        }
<<<<<<< Updated upstream
=======
        
        private void OnClickPerformed(InputAction.CallbackContext context)
        {

            if (context.action.ReadValue<float>() == 1)
            {
                //check what was clicked at this position
                var mouseWorldPos = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
                
                int clickHit2 = Physics2D.Raycast(mouseWorldPos, sceneCamera.transform.position - mouseWorldPos, clickFilter, clickCastCollisions,0);
                if (clickHit2 != 0)
                {
                    // get the first object
                    RaycastHit2D hit = clickCastCollisions[0];
                    GameObject hitObject = hit.transform.gameObject;
                    int hitLayer = hitObject.layer;
                    string hitTag = hitObject.tag;
                    // make sure the objects is an interactable (interactable layer = 10
                    if (hitLayer == 10)
                    {

                        if (hitObject.TryGetComponent<QuestKeyController>(out QuestKeyController keyCtrl))
                        {
                            // Add key to its quest
                            AddKey(keyCtrl.key);
                            Destroy(hitObject);
                        }
                        //Debug.Log("click detected on collider: " + hit.collider.name + " | Object: " + hitObject.name + " | Layer: " + hitLayer + "| tag: " + hitTag);
                    }

                    
                    
                }

            }
            
        }




        private void HandleMovementAnimBlendTree()
        {
            if (moveVector != Vector2.zero)
            {
                animator.SetFloat("Horizontal", moveVector.x);
                animator.SetFloat("Vertical", moveVector.y);

                if (moveVector.x < 0.01f)
                {
                    // moving left
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    // moving right
                    transform.localScale = new Vector3((-1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }


                if (animator.GetFloat("Vertical") == 1)
                {
                    // moving up
                    directionInt = 1;
                }
                else if (animator.GetFloat("Vertical") == -1)
                {
                    // moving down
                    directionInt = 3;
                }


                if (animator.GetFloat("Horizontal") == 1)
                {
                    // move right
                    animAfterLeft = false;
                    directionInt = 2;
                }
                else if (animator.GetFloat("Horizontal") == -1)
                {
                    // move left
                    animAfterLeft = true;
                    directionInt = 4;
                }


            }
            animator.SetFloat("Speed", moveVector.sqrMagnitude);
        }
>>>>>>> Stashed changes
    }
}
