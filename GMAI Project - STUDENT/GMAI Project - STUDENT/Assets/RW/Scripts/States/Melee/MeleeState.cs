using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class MeleeState : State
    {
        private bool equipMelee;
        private bool punch;
        private bool combo;
        private bool kick;
         
        private int meleeParam = Animator.StringToHash("IsMelee");

        public MeleeState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter(); 
            equipMelee = false;
        }

        public override void HandleInput()
        {
            base.HandleInput(); 

            equipMelee = Input.GetKeyDown(KeyCode.V); 
            punch = Input.GetMouseButton(0);
            combo = Input.GetMouseButton(1); 
            kick = Input.GetKeyDown(KeyCode.M);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (equipMelee)
            {
                character.SetAnimationBool(meleeParam, true);
                Debug.Log("Melee equipped");

                stateMachine.ChangeState(character.sheathing);
                //Debug.Log("Going into sheathed state");
            } 
            else if (!equipMelee && punch)
            {
                stateMachine.ChangeState(character.punching);
                //Debug.Log("Going into punch state");
            }
            else if (!equipMelee && combo)
            {
                stateMachine.ChangeState(character.combo);
                //Debug.Log("Going into combo state");
            }
            else if (!equipMelee && kick)
            {
                stateMachine.ChangeState(character.kicking);
                //Debug.Log("Going into kicking state");
            }
        }
    } 
}