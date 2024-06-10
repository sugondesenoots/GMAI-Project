using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SheatheState : MeleeState
    {
        private bool draw;
        private int sheatheParam = Animator.StringToHash("SheathMelee");
        private int meleeParam = Animator.StringToHash("IsMelee");

        public SheatheState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(meleeParam, true);
            character.TriggerAnimation(sheatheParam);
            draw = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            draw = Input.GetKeyDown(KeyCode.E);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (draw)
            {
                stateMachine.ChangeState(character.drawing);
            }
        }
    }
}