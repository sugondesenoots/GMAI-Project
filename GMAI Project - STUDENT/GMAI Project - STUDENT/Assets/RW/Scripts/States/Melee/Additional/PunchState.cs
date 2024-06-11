using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class PunchState : State
    {
        private bool punch;
        private int punchParam = Animator.StringToHash("Punch");
        private int comboParam = Animator.StringToHash("ComboPunch");

        public PunchState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();

            character.SetAnimationBool(comboParam, false);
            character.TriggerAnimation(punchParam);
            punch = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (punch)
            {
                stateMachine.ChangeState(character.melee);
            }
        }
    }
}
