using UnityEngine;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        int cnt = 0;
        public Audio sound;
        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;
        public KeyCode attackInput = KeyCode.Mouse0;
        public KeyCode attack2Input = KeyCode.Mouse1;
        public KeyCode skill1Input = KeyCode.E;
        public KeyCode skill2Input = KeyCode.F;
        public KeyCode hyperskillInput = KeyCode.Q;
        public KeyCode potionInput = KeyCode.C;

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        #endregion

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
            cc.CheckDead();
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            CameraInput();
            SprintInput();

            if (vThirdPersonController.usingHyper || Player.stuned || Player.hp <= 0 || UIDisplay.UI_display)
            {
                cc.input.x = 0;
                cc.input.z = 0;
                return; 
            }

            MoveInput();
            StrafeInput();
            JumpInput();
            AttackInput();
            Attack2Input();
            Skill1Input();
            Skill2Input();
            HyperskillInput();
            PotionInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);

            if(!(Input.GetAxis(horizontalInput) == 0) || !(Input.GetAxis(verticallInput) == 0))
            {
                if(cc.isGrounded)
                {
                    if (cnt == 0)
                        sound.AudioWalk();

                    cnt++;

                    if (cnt == 16)
                        cnt = 0;
                }
            }
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && vThirdPersonController.jump_cnt != 2)
                cc.Jump();
        }

        protected virtual void AttackInput()
        {
            if (Input.GetKeyDown(attackInput))
                cc.Attack();
        }

        protected virtual void Attack2Input()
        {
            if (Input.GetKeyDown(attack2Input))
                cc.Attack2();
        }

        protected virtual void Skill1Input()
        {
            if (Input.GetKeyDown(skill1Input))
                cc.Skill1();
        }

        protected virtual void Skill2Input()
        {
            if(Input.GetKeyDown(skill2Input))
                cc.Skill2();
        }

        protected virtual void HyperskillInput()
        {
            if (Input.GetKeyDown(hyperskillInput))
                cc.HyperSkill();
        }

        protected virtual void PotionInput()
        {
            if (Input.GetKeyDown(potionInput))
                cc.Potion();
        }

        #endregion       
    }
}