using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SwingState : State
    {
        private bool drawn;
        private int swingParam = Animator.StringToHash("SwingMelee");

        public SwingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            character.TriggerAnimation(swingParam);
            drawn = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (drawn)
            {
                stateMachine.ChangeState(character.drawn);
            }
        }
    }
}
