using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class ComboState : State
    {
        private bool combo;
        private int comboParam = Animator.StringToHash("ComboPunch");
        private int punchParam = Animator.StringToHash("Punch");

        public ComboState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();

            character.SetAnimationBool(punchParam, false);
            character.TriggerAnimation(comboParam);
            combo = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (combo)
            {
                stateMachine.ChangeState(character.melee);
            }
        }
    }
}
