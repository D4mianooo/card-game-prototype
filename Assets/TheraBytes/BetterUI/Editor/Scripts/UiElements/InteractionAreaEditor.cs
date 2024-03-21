﻿using UnityEditor;

namespace TheraBytes.BetterUi.Editor {
    [CustomEditor(typeof(InteractionArea))] [CanEditMultipleObjects]
    public class InteractionAreaEditor : UnityEditor.Editor {

        private InteractionArea ia;

        private SerializedProperty shapeProp,
            radiusFallbackProp,
            radiusConfigsProp,
            raycastProp;

        private void OnEnable() {
            ia = target as InteractionArea;
            shapeProp = serializedObject.FindProperty("ClickableShape");
            radiusFallbackProp = serializedObject.FindProperty("cornerRadiusFallback");
            radiusConfigsProp = serializedObject.FindProperty("cornerRadiusConfigs");
            raycastProp = serializedObject.FindProperty("m_RaycastTarget");

        }

        public override void OnInspectorGUI() {
            EditorGUILayout.PropertyField(raycastProp);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(shapeProp);

            if (shapeProp.intValue == (int)InteractionArea.Shape.RoundedRectangle) {
                ScreenConfigConnectionHelper.DrawSizerGui("Corner Radius", radiusConfigsProp, ref radiusFallbackProp);
                ia.UpdateCornerRadius();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
