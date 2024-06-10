using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class MeleeState : State
    {
        public MeleeState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }
    }
}