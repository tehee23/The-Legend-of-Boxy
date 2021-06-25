// ============================
// ���� : 2021-06-25
// �ۼ� : sujeong
// ============================

using UnityEngine;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Jump : ActionBase
    {
        private readonly float _jumpForce;
        private readonly float _gravity;

        private bool IsJumped = false;

        public PlayerAction_Jump(Character owner, States state, float jumpForce, float gravity)
            : base(owner, state)
        {
            _jumpForce = jumpForce;
            _gravity = gravity;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Enter the Jump State");

            IsJumped = false;
        }

        public override void FixedUpdateState()
        {
            if (!IsJumped)
            {
                _owner.AddImpulseForce(_owner.transform.up, _jumpForce);
                IsJumped = true;
            }
            _owner.OnGravity(_gravity);
            _owner.FixedUpdateIsGrounded();

        }

    }

}
