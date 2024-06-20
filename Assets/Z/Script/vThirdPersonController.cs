using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace Invector.vCharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        float albedo = 0;
        bool dead = false;
        bool usingSkill = false;
        public static bool usingHyper = false;
        public static int jump_cnt = 0;
        public static int potion = 3;
        public GameObject character;
        public GameObject attack;
        public GameObject attack2;
        public GameObject skill1;
        public GameObject skill2;
        public GameObject hyper;
        public GameObject particle;
        public GameObject hyperDmg;

        public GameObject hide_sword;

        public GameObject cameraPos;
        public GameObject jumpcameraPos;

        public Material sky1;
        public Material sky2;

        public GameObject flower;

        public GameObject panel;

        public GameObject FailScreen;
        public GameObject btn;

        public Audio sound;

        public ParticleSystem heal;


        private void Start()
        {
            heal.Stop();
        }
        public virtual void ControlAnimatorRootMotion()
        {
            if (!this.enabled) return;

            if (inputSmooth == Vector3.zero)
            {
                transform.position = animator.rootPosition;
                transform.rotation = animator.rootRotation;
            }

            if (useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlLocomotionType()
        {
            if (lockMovement) return;

            if (locomotionType.Equals(LocomotionType.FreeWithStrafe) && !isStrafing || locomotionType.Equals(LocomotionType.OnlyFree))
            {
                SetControllerMoveSpeed(freeSpeed);
                SetAnimatorMoveSpeed(freeSpeed);
            }
            else if (locomotionType.Equals(LocomotionType.OnlyStrafe) || locomotionType.Equals(LocomotionType.FreeWithStrafe) && isStrafing)
            {
                isStrafing = true;
                SetControllerMoveSpeed(strafeSpeed);
                SetAnimatorMoveSpeed(strafeSpeed);
            }

            if (!useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlRotationType()
        {
            if (lockRotation) return;

            bool validInput = input != Vector3.zero || (isStrafing ? strafeSpeed.rotateWithCamera : freeSpeed.rotateWithCamera);

            if (validInput)
            {
                // calculate input smooth
                inputSmooth = Vector3.Lerp(inputSmooth, input, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);

                Vector3 dir = (isStrafing && (!isSprinting || sprintOnlyFree == false) || (freeSpeed.rotateWithCamera && input == Vector3.zero)) && rotateTarget ? rotateTarget.forward : moveDirection;
                RotateToDirection(dir);
            }
        }

        public virtual void UpdateMoveDirection(Transform referenceTransform = null)
        {
            if (input.magnitude <= 0.01)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);
                return;
            }

            if (referenceTransform && !rotateByWorld)
            {
                //get the right-facing direction of the referenceTransform
                var right = referenceTransform.right;
                right.y = 0;
                //get the forward direction relative to referenceTransform Right
                var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                moveDirection = (inputSmooth.x * right) + (inputSmooth.z * forward);
            }
            else
            {
                moveDirection = new Vector3(inputSmooth.x, 0, inputSmooth.z);
            }
        }

        public virtual void Sprint(bool value)
        {
            var sprintConditions = (input.sqrMagnitude > 0.1f && isGrounded &&
                !(isStrafing && !strafeSpeed.walkByDefault && (horizontalSpeed >= 0.5 || horizontalSpeed <= -0.5 || verticalSpeed <= 0.1f)));

            if (value && sprintConditions)
            {
                if (input.sqrMagnitude > 0.1f)
                {
                    if (isGrounded && useContinuousSprint)
                    {
                        isSprinting = !isSprinting;
                    }
                    else if (!isSprinting)
                    {
                        isSprinting = true;
                    }
                }
                else if (!useContinuousSprint && isSprinting)
                {
                    isSprinting = false;
                }
            }
            else if (isSprinting)
            {
                isSprinting = false;
            }
        }

        public virtual void Strafe()
        {
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // trigger jump behaviour
            jumpCounter = jumpTimer;
            isJumping = true;

            // trigger jump animations
            if (jump_cnt == 0)
            {
                animator.CrossFadeInFixedTime("Jump 0", 0.2f);
            }

            if (jump_cnt == 1)
            {
                animator.CrossFadeInFixedTime("Jump", 0.3f);
            }

            jump_cnt++;
        }

        public virtual void Attack()
        {
            if (Player.attack_cooldown <= 0 && !usingSkill)
            {
                animator.CrossFadeInFixedTime("attack1", 0.2f);
                Instantiate(attack, transform);
                Player.attack_cooldown = 0.5f;
                sound.AudioStart(2);
            }
        }

        public virtual void Attack2()
        {
            if (Player.attack2_cooldown <= 0 && !usingSkill)
            {
                animator.CrossFadeInFixedTime("attack2", 0.1f);
                Instantiate(attack2, transform);
                Player.attack2_cooldown = 5;
                sound.AudioStart(2);
            }
        }

        public virtual void Skill1()
        {
            if (Player.skill1_cooldown <= 0 && !usingSkill)
            {
                Vector3 pos = transform.forward * 10f;
                pos.y = 0;
                Instantiate(skill1, transform.position, transform.rotation);
                Player.skill1_cooldown = 15;
            }
        }

        public virtual void Skill2()
        {
            if (Player.skill2_cooldown <= 0 && !usingSkill)
            {
                Instantiate(skill2, transform);
                character.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                vThirdPersonCamera.defaultDistance = 8f;
                Invoke("Display", 7f);
                usingSkill = true;
                Player.skill2_cooldown = 25;
                sound.AudioStart(3);
            }
        }

        void Display()
        {
            character.transform.localScale = new Vector3(1, 1, 1);
            vThirdPersonCamera.defaultDistance = 2.5f;
            usingSkill = false;
        }

        public virtual void HyperSkill()
        {
            if (Player.hyper_cooldown <= 0 && !usingSkill && isGrounded)
            {
                hide_sword.SetActive(false);
                Player.hyper_cooldown = 60;
                animator.CrossFadeInFixedTime("crouch", 0.2f);
                flower.SetActive(true);
                transform.Rotate(0, 90, 0);
                usingSkill = true;
                usingHyper = true;
                Invoke("HyperCameraOn", 0.2f);
            }
        }

        void HyperCameraOn()
        {
            StateCamera.main_camera_pos.GetComponent<Skybox>().material = sky2;
            StateCamera.main_camera.SetActive(false);
            StateCamera.hyper_camera.SetActive(true);
            StateCamera.hyper_camera.transform.position = cameraPos.transform.position;
            Invoke("HyperJump", 3f);
        }

        void HyperJump()
        {
            hide_sword.SetActive(true);
            animator.CrossFadeInFixedTime("hyperJump", 0.2f);
            transform.Rotate(0, -90, 0);
            _rigidbody.velocity = new Vector3(0, 30, 0);
            Invoke("HyperCamera", 2f);
        }

        void HyperCamera()
        {
            _rigidbody.isKinematic = true;
            StateCamera.hyper_camera.transform.position = jumpcameraPos.transform.position;
            Invoke("HyperFall", 1f);
        }

        void HyperFall()
        {
            Vector3 pos = transform.TransformDirection(0, -1, 1);
            animator.CrossFadeInFixedTime("hyperFall", 0.2f);
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = pos * 50;
            Invoke("HyperLand", 0.3f);
        }

        void HyperLand()
        {
            Instantiate(particle, transform);
            Instantiate(hyper, transform);
            Invoke("HyperEnd", 10f);
        }

        void HyperEnd()
        {
            Instantiate(hyperDmg, transform);
            flower.SetActive(false);
            animator.SetTrigger("HyperEnd");
            StateCamera.main_camera_pos.GetComponent<Skybox>().material = sky1;
            usingSkill = false;
            Invoke("ResetHyperCamera", 1.5f);
        }
        void ResetHyperCamera()
        {
            StateCamera.main_camera.SetActive(true);
            StateCamera.hyper_camera.SetActive(false);
            Invoke("DeHyper", 1f);
        }
        void DeHyper()
        {
            usingHyper = false;
        }

        public void CheckDead()
        {
            if (Player.hp <= 0 && !dead)
            {
                StartCoroutine(End());
                animator.CrossFadeInFixedTime("Die", 0.2f);
                dead = true;
            }
        }

        IEnumerator End()
        {
            yield return new WaitForSeconds(3);
            
            while(albedo <= 1)
            {
                albedo += 1f / 255f;
                panel.GetComponent<Image>().color = new Color(0, 0, 0, albedo);
                yield return null;
            }
            FailScreen.SetActive(true);
            btn.SetActive(true);
            UIDisplay.UI_display = true;
        }

        public void Potion()
        {
            if (potion > 0 && Player.potion_cooldown <= 0) 
            {
                heal.Play();
                Player.hp += 30;

                if(Player.hp > 100)
                    Player.hp = 100;

                potion--;
                Player.potion_cooldown = 10f;
            }
        }
    }
}