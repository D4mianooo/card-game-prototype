using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TheraBytes.BetterUi.Editor {
    [CustomEditor(typeof(BetterLocator))] [CanEditMultipleObjects]
    public class BetterLocatorEditor : UnityEditor.Editor {

        private static bool autoPullFromTransform = true;
        private static bool autoPushToTransform;

        private readonly Dictionary<RectTransformData, bool> anchorExpands = new();

        private bool autoPull, autoPush;
        private bool hasOffsetter;
        private BetterLocator locator;

        private bool pauseAutoPushOnce; // recursive infinite loop protection

        private SerializedProperty transformFallback, transformConfigs;

        protected virtual void OnEnable() {
            locator = target as BetterLocator;
            transformFallback = serializedObject.FindProperty("transformFallback");
            transformConfigs = serializedObject.FindProperty("transformConfigs");

            hasOffsetter = locator.gameObject.GetComponent<BetterOffsetter>() != null;
            autoPull = autoPullFromTransform && !hasOffsetter;
            autoPush = autoPushToTransform && !hasOffsetter;

            locator.OnValidate();
        }

        public override void OnInspectorGUI() {
            string currentScreen = ResolutionMonitor.CurrentScreenConfiguration?.Name;
            bool isCurrentScreenConfig = currentScreen == null // fallback is always present
                                         || locator.CurrentTransformData.ScreenConfigName == currentScreen;

            if (isCurrentScreenConfig) {
                EditorGUILayout.PrefixLabel("Live Update");
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginDisabledGroup(!locator.isActiveAndEnabled);
                bool newPull = GUILayout.Toggle(autoPull, "↓   Auto-Pull", "ButtonLeft", GUILayout.MinHeight(30));
                bool newPush = GUILayout.Toggle(autoPush, "↑   Auto-Push", "ButtonRight", GUILayout.MinHeight(30));
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndHorizontal();

                if (newPull != autoPull) {
                    autoPull = newPull;
                    autoPullFromTransform = newPull;
                }

                if (newPush != autoPush) {
                    autoPush = newPush;
                    autoPushToTransform = newPush;
                }
            }
            else {
                EditorGUILayout.BeginVertical(GUILayout.MinHeight(52));
                GUILayout.FlexibleSpace();

                EditorGUILayout.HelpBox(
                    $"To prevent accidentally overriding the wrong config, live update is disabled. It is enabled only if you have a config for the current screen configuration ('{currentScreen}') present.",
                    MessageType.Info);

                EditorGUILayout.EndHorizontal();
            }



            if (autoPull && locator.isActiveAndEnabled && isCurrentScreenConfig) {
                locator.CurrentTransformData.PullFromTransform(locator.transform as RectTransform);
            }

            ScreenConfigConnectionHelper.DrawGui("Rect Transform Override",
                transformConfigs,
                ref transformFallback,
                DrawTransformData);

            if (autoPush && !pauseAutoPushOnce && isCurrentScreenConfig) {
                locator.CurrentTransformData.PushToTransform(locator.transform as RectTransform);
            }

            pauseAutoPushOnce = false;
        }

        private void DrawTransformData(string configName, SerializedProperty prop) {
            RectTransformData data = prop.GetValue<RectTransformData>();
            bool isCurrent = locator.CurrentTransformData == data;

            if (!anchorExpands.ContainsKey(data)) {
                anchorExpands.Add(data, true);
            }

            bool anchorExpand = anchorExpands[data];
            float height = anchorExpand
                ? RectTransformDataDrawer.HeightWithAnchorsExpanded
                : RectTransformDataDrawer.HeightWithoutAnchorsExpanded;

            EditorGUILayout.BeginVertical("box");
            Rect bounds = EditorGUILayout.GetControlRect(false, height);


            bool canEdit = !isCurrent || !autoPull || autoPush;

            // Pull
            EditorGUI.BeginDisabledGroup(isCurrent && autoPull);
            if (GUI.Button(new Rect(bounds.position + new Vector2(5, 5), new Vector2(40, 40)), "Pull\n↓")) {
                Undo.RecordObject(locator, "Pull From Rect Transform");
                data.PullFromTransform(locator.transform as RectTransform);
            }
            EditorGUI.EndDisabledGroup();

            // Push
            EditorGUI.BeginDisabledGroup(!canEdit || isCurrent && autoPush);
            if (GUI.Button(new Rect(bounds.position + new Vector2(50, 25), new Vector2(40, 40)), "↑\nPush")) {
                Undo.RecordObject(locator.transform, "Push To Rect Transform");

                data.PushToTransform(locator.transform as RectTransform);
                pauseAutoPushOnce = true;
            }
            EditorGUI.EndDisabledGroup();

            // Fields
            if (!canEdit) {
                EditorGUI.BeginDisabledGroup(true);
            }

            RectTransformDataDrawer.DrawFields(prop, data, bounds, ref anchorExpand);
            anchorExpands[data] = anchorExpand;

            if (!canEdit) {
                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.EndVertical();
        }

        [MenuItem("CONTEXT/RectTransform/♠ Add Better Locator", false)]
        public static void AddBetterLocator(MenuCommand command) {
            RectTransform ctx = command.context as RectTransform;
            BetterLocator locator = ctx.gameObject.AddComponent<BetterLocator>();

            while (ComponentUtility.MoveComponentUp(locator)) {
            }
        }

        [MenuItem("CONTEXT/RectTransform/♠ Add Better Locator", true)]
        public static bool CheckBetterLocator(MenuCommand command) {
            RectTransform ctx = command.context as RectTransform;


            return ctx.gameObject.GetComponent<BetterLocator>() == null;
        }
    }

}
