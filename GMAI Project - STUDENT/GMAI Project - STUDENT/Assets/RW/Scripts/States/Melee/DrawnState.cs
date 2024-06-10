using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DrawnState : State
    {
        private bool swing;
        private bool sheathe;

        private int drawnParam = Animator.StringToHash("IsMelee");
        private int swingParam = Animator.StringToHash("SwingMelee");

        public DrawnState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(swingParam, false);
            character.SetAnimationBool(drawnParam, true);
            swing = false; 
            sheathe = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            swing = Input.GetMouseButton(0);
            sheathe = Input.GetKeyDown(KeyCode.F);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (swing)
            {
                stateMachine.ChangeState(character.swinging);
            }
            else if (sheathe)
            {
                stateMachine.ChangeState(character.sheathing);
            }
        }
    }
}