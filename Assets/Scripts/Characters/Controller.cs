// ============================
// 수정 : 2021-06-18
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters
{
    public class Controller : MonoBehaviour
    {
        public LayerMask staticLayer = 0;

        protected bool IsGrounded = false;    // Check Grounded

        protected virtual void Awake()
        {
            if (staticLayer == 0)
                staticLayer = LayerMask.NameToLayer("STATICMESH");
        }

        public virtual void UpdateControl() { }         // Update()

        public virtual void FixedUpdateControl() { }    // FixedUpdate()

        protected virtual void SetCharacterState() { }  // Set FSM
        
        // 캐릭터가 접지 상태인지 체크
        protected void CheckGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 1.0f, staticLayer))
            {
                if (hit.distance < float.Epsilon)
                    IsGrounded = true;
            }
            else IsGrounded = false;
        }
    }
}

