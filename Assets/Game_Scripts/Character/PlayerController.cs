using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core.Character;
using Core.Utility;
using MoreMountains.Feedbacks;
namespace Core.Character
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float playerSpeed = 5f;
        [SerializeField]
        private float jumpForce = 5f; 
        [SerializeField]
        private float climbSpeed = 5f;

        [SerializeField]
        private CircleCollider2D groundCheckCollider;

        private Rigidbody2D playerRb;
        public  Animator playerAnim { get; private set; }
        [SerializeField]
        private CircleCollider2D playerCollider;

        public float knockbackForce = 5;

        bool isGrounded;
        public bool isStun = false;
        bool canClimb;

        public MMF_Player doorFeedback;
        public MMF_Player attackFeedbacl;

       // public UnityEvent OnAttack;
        //public UnityEvent onJumped;
        //public UnityEvent OnLanded;
        private bool isJumped;


       // public UnityEvent OnDoorOpen;

        public PlayerInteraction playerInteraction;

        private void OnEnable()
        {
            //Trigger By Door once Open Animation finisherd 
            Door_Portle.OnDoorEnter  += EntrerDoor;
            Door_Portle.OnDoorExit  += ExitDoor;
        }
        private void OnDisable()
        {
            Door_Portle.OnDoorEnter  -= EntrerDoor;
            Door_Portle.OnDoorExit -= ExitDoor;
        }


        void Start()
        {
            playerRb = GetComponent<Rigidbody2D>();
            playerAnim  = GetComponentInChildren<Animator >();
        }

        // Update is called once per frame
        void Update()
        {

            if (!isStun)
            {
                Run();
                Jump();
                Climb();
                Attack();
                Interact();
            }
        }

        public void EntrerDoor()
        {
            playerRb.velocity = Vector2.zero;
            playerAnim.SetTrigger("DoorIn");
            isStun = true;
        }
        public void  ExitDoor()
        {
            MakeVisiable();
            playerAnim.SetTrigger("DoorOut");
            StartCoroutine(Stun(1f));
        }
        public void Interact()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerInteraction.Interact();
            }
        }
        public void Attack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                attackFeedbacl.PlayFeedbacks();
              //  OnAttack?.Invoke();
            }
        }
        private void Climb()
        {
            canClimb = playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
            playerRb.gravityScale = canClimb ? 0 : 1;
            if (canClimb)
            {
                float inputY = Input.GetAxis("Vertical");
                playerRb.velocity = new Vector2(playerRb.velocity.x, climbSpeed * inputY);
                playerAnim.SetBool("Climbing", true);
                //blend Tree For up and down move direction // Trasnsition to empty//Climb anim  by checking Climbing 
            }
            else
            {
                playerAnim.SetBool("Climbing", false);
            }
        }

        public void MakeInvisiable( ) => GetComponentInChildren<SpriteRenderer>().enabled = false;
        public void MakeVisiable( ) => GetComponentInChildren<SpriteRenderer>().enabled = true;
        public void ApplyDamage(float _damage)
        {
            float damage =Mathf.Abs( _damage);
            float knockBackDirection =damage / _damage;
            StartCoroutine(Stun(0.23f));
            playerRb.velocity = knockbackForce * new Vector2( knockBackDirection, 1f );
            playerAnim.SetTrigger("Hit");

        }
        private void Jump()
        {
            isGrounded = groundCheckCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
            bool jumped = Input.GetKeyDown(KeyCode.Space);
            if (jumped && isGrounded /*&& !isJumped*/ )
            {
                Vector2 jumpVelocity = new Vector2(playerRb.velocity.x, jumpForce);
                playerRb.velocity = jumpVelocity;
            }

        }

        private bool isRunning;
        private void Run()
        {
            float inputX = Input.GetAxis("Horizontal");
            playerRb.velocity = new Vector2(inputX * playerSpeed, playerRb.velocity.y);
            OnRunning();
            FlipCharacter();
        }

        private void OnRunning()
        {
            isRunning = (Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon);
            playerAnim.SetBool("Running", isRunning);
        }

         IEnumerator  Stun(float _stunTime)
        {
            isStun = true;
            yield return new WaitForSeconds(_stunTime);
            isStun = false;
        }

        private void FlipCharacter()
        {
            if (isRunning)
            {
                transform.localScale = new Vector2(Mathf.Sign(playerRb.velocity.x), 1);
            }
        }

    }
}
