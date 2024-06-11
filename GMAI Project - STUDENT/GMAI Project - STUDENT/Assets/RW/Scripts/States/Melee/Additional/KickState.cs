using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class KickState : State
    {
        private bool backToIdle;

        private int kickParam = Animator.StringToHash("Kick");
        private int punchParam = Animator.StringToHash("Punch");
        private int comboParam = Animator.StringToHash("ComboPunch");

        public KickState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();

            character.SetAnimationBool(comboParam, false);
            character.SetAnimationBool(punchParam, false);

            character.TriggerAnimation(kickParam);
            backToIdle = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (backToIdle)
            {
                stateMachine.ChangeState(character.melee);
            }
        }
    }
}
