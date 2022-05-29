using RoM.Code.Core.NPCs.StateMachine;
using UnityEditor;
using UnityEngine;

namespace RoM.Code.Editor.Inspectors
{
    [CustomEditor(typeof(NPCStateMachine), true)]
    public class NPCStateMachineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (Application.isPlaying)
            {
                EditorGUILayout.Space(10);
                var npcStateMachine = (NPCStateMachine)target;
                var stateType = npcStateMachine.CurrentState != null ? npcStateMachine.CurrentState.GetType().Name : string.Empty;
                EditorGUILayout.LabelField("Current State", stateType);   
            }
        }
    }
}