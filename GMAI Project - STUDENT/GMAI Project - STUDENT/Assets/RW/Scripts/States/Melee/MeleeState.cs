using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class MeleeState : State
    {
        private bool equipMelee;
        private int meleeParam = Animator.StringToHash("IsMelee");

        public MeleeState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void HandleInput()
        {
            base.HandleInput();
            equipMelee = Input.GetKeyDown(KeyCode.V);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (equipMelee)
            {
                character.SetAnimationBool(meleeParam, true);
                Debug.Log("Melee equipped");

                stateMachine.ChangeState(character.sheathing);
                Debug.Log("Going into sheathed state");
            }
        }
    } 
}