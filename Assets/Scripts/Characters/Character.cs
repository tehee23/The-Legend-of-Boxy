// ============================
// 수정 : 2021-06-29
// 작성 : sujeong
// ============================

using System.Collections;
using UnityEngine;
using Characters.FSM;
using Characters.FSM.Actions;

namespace Characters
{
    // 캐릭터의 상태 열거형
    [System.Serializable]
    public enum States
    {
        IDLE,           // 0
        RUN,            // 1
        SPRINT,         // 2
        DASH,           // 3
        JUMP,           // 4
        LANDING,        // 5
        FALL,           // 6
        DOWNFALL,       // 7
        CLIMB,          // 8
        GLIDE,          // 9
        AUTOATTACK,     // 10
        STRONGATTACK,   // 11
        SKILLATTACK,    // 12
        DIE             // 13
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        public Controller Controller = null;

        [Header("Movement State")]
        public States State = States.IDLE;

        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        [HideInInspector] public Rigidbody m_rigidbody = null;
        [HideInInspector] public float curSpeed = 0.0f;

        protected Animator m_animator = null;
        protected FiniteStateMachine FSM = null;

        protected float m_smoothVelocity = 0.0f;        // Restore SmoothRotate Velocity 
        protected float m_distanceFromGround = 0.0f;
        protected float m_animtaionDelay = 0.0f;
        protected bool Climbable = false;

        protected virtual void Awake()
        {
            FSM = new FiniteStateMachine();
            Controller ??= GetComponent<Controller>();

            Controller.Character = this;
        }

        protected virtual void Update()
        {
            FSM.UpdateState();
        }

        protected virtual void LateUpdate()
        {
            m_distanceFromGround = Controller.GetDistanceFromGround();
        }

        protected virtual void FixedUpdate()
        {
            FSM.FixedUpdateState();
        }

        public virtual void UpdateMoveDirection() { }
        public virtual void OnRotate(float rotSpeed) { }

        // Behaviors
        // 상태에 따라 달라지는 행동은 매개변수를 받음
        public virtual void OnGravity(float gravity) { }
        public virtual void OnMove(float moveSpeed) { }
        public virtual void OnDecel() { }

        public void AddImpulseForce(Vector3 direction, float force)
        {
            m_rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        // Animation Func
        public void ToAnimaition(int value)
        {
            // 중복 리턴
            if (m_animator.GetInteger("State") == value)
                return;

            m_animator.SetInteger("State", value);
        }

        public void SetAnimtaionDelay(float delayTime)
        {
            m_animtaionDelay = delayTime;
        }
    }
}
