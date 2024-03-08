using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;
        private float jumpSpeed = 8;

        private bool isJumping;
        private bool isFalling;

        void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		}

		void Update (){
            anim.SetBool("isJumping", false);
            if (Input.GetKey ("w")) {
				anim.SetInteger ("AnimationPar", 1);
			}  else {
				anim.SetInteger ("AnimationPar", 0);
			}
            if (controller.isGrounded)
            {
                moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
				if (Input.GetKey("space"))
				{
                    moveDirection.y = jumpSpeed;
                    isJumping = true;
					anim.SetBool("isJumping", true);
				}
                else
                {
                    isJumping = false;
                    anim.SetBool("isJumping", false);
                }

            }
            else
            {
                if (isJumping)
                {
                    anim.SetBool("isFalling", true);
                } else
                {
                    anim.SetBool("isFalling", false);
                }
            }
			anim.SetBool("grounded", true);
            float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}
	}
}
