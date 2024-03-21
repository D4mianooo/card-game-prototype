using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TheraBytes.BetterUi.Editor {
    [CustomEditor(typeof(BetterText), true)] [CanEditMultipleObjects]
    public class BetterTextEditor : GraphicEditor {
        private SerializedProperty font, style, lineSpace, rich, align, geoAlign, overflowH, overflowV;
        private SerializedProperty maskable;
        private SerializedProperty sizerFallback, sizerCollection, fitting;
        private SerializedProperty text;

        protected override void OnEnable() {
            base.OnEnable();
            text = serializedObject.FindProperty("m_Text");

            sizerFallback = serializedObject.FindProperty("fontSizerFallback");
            sizerCollection = serializedObject.FindProperty("customFontSizers");
            fitting = serializedObject.FindProperty("fitting");

            SerializedProperty fontData = serializedObject.FindProperty("m_FontData");
            font = fontData.FindPropertyRelative("m_Font");
            style = fontData.FindPropertyRelative("m_FontStyle");
            lineSpace = fontData.FindPropertyRelative("m_LineSpacing");
            rich = fontData.FindPropertyRelative("m_RichText");
            align = fontData.FindPropertyRelative("m_Alignment");
            geoAlign = fontData.FindPropertyRelative("m_AlignByGeometry");
            overflowH = fontData.FindPropertyRelative("m_HorizontalOverflow");
            overflowV = fontData.FindPropertyRelative("m_VerticalOverflow");
            maskable = serializedObject.FindProperty("m_Maskable");
        }

        [MenuItem("CONTEXT/Text/♠ Make Better")]
        public static void MakeBetter(MenuCommand command) {
            Text txt = command.context as Text;
            int size = txt.fontSize;
            bool bestFit = txt.resizeTextForBestFit;
            int min = txt.resizeTextMinSize;
            int max = txt.resizeTextMaxSize;

            Text newTxt = Betterizer.MakeBetter<Text, BetterText>(txt);

            BetterText betterTxt = newTxt as BetterText;
            if (betterTxt != null) {
                if (bestFit) {
                    betterTxt.FontSizer.MinSize = min;
                    betterTxt.FontSizer.MaxSize = max;
                }

                betterTxt.FontSizer.SetSize(betterTxt, size);
            }

            Betterizer.Validate(newTxt);
        }

        public override void OnInspectorGUI() {
            BetterText obj = target as BetterText;

            serializedObject.Update();
            EditorGUILayout.PropertyField(text);

            EditorGUILayout.LabelField("Better Sizing", EditorStyles.boldLabel);

            ScreenConfigConnectionHelper.DrawSizerGui("Font Size", sizerCollection, ref sizerFallback);

            EditorGUI.indentLevel += 1;
            EditorGUILayout.PropertyField(fitting);
            EditorGUI.indentLevel -= 1;

            EditorGUILayout.LabelField("Character", EditorStyles.boldLabel);

            EditorGUI.indentLevel += 1;
            EditorGUILayout.PropertyField(font);
            EditorGUILayout.PropertyField(style);
            EditorGUILayout.PropertyField(lineSpace);
            EditorGUILayout.PropertyField(rich);
            EditorGUI.indentLevel -= 1;

            EditorGUILayout.LabelField("Paragraph", EditorStyles.boldLabel);

            EditorGUI.indentLevel += 1;
            //EditorGUILayout.PropertyField(align);
            DrawAnchorIcons(align, obj.alignment);

            if (geoAlign != null) // not present in old unity versions
            {
                EditorGUILayout.PropertyField(geoAlign);
            }

            EditorGUILayout.PropertyField(overflowH);
            EditorGUILayout.PropertyField(overflowV);
            EditorGUI.indentLevel -= 1;

            AppearanceControlsGUI();
            RaycastControlsGUI();

            if (maskable != null) {
                EditorGUILayout.PropertyField(maskable);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawAnchorIcons(SerializedProperty prop, TextAnchor anchor) {
            bool hLeft = anchor == TextAnchor.LowerLeft || anchor == TextAnchor.MiddleLeft || anchor == TextAnchor.UpperLeft;
            bool hCenter = anchor == TextAnchor.LowerCenter || anchor == TextAnchor.MiddleCenter ||
                           anchor == TextAnchor.UpperCenter;
            bool hRight = anchor == TextAnchor.LowerRight || anchor == TextAnchor.MiddleRight || anchor == TextAnchor.UpperRight;

            bool vTop = anchor == TextAnchor.UpperLeft || anchor == TextAnchor.UpperCenter || anchor == TextAnchor.UpperRight;
            bool vCenter = anchor == TextAnchor.MiddleLeft || anchor == TextAnchor.MiddleCenter ||
                           anchor == TextAnchor.MiddleRight;
            bool vBottom = anchor == TextAnchor.LowerLeft || anchor == TextAnchor.LowerCenter || anchor == TextAnchor.LowerRight;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Alignment",
                GUILayout.Width(EditorGUIUtility.labelWidth - 12),
                GUILayout.ExpandWidth(false));

            bool hLeft2 = DrawAlignIcon(Styles.LeftAlignActive, Styles.LeftAlign, TextAlignment.Left, hLeft);
            bool hCenter2 = DrawAlignIcon(Styles.CenterAlignActive, Styles.CenterAlign, TextAlignment.Center, hCenter);
            bool hRight2 = DrawAlignIcon(Styles.RightAlignActive, Styles.RightAlign, TextAlignment.Right, hRight);

            if (hLeft != hLeft2 || hCenter != hCenter2 || hRight != hRight2) {
                hLeft = hLeft == hLeft2 ? false : hLeft2;
                hCenter = hCenter == hCenter2 ? false : hCenter2;
                hRight = hRight == hRight2 ? false : hRight2;
            }


            EditorGUILayout.Separator();

            bool vTop2 = DrawAlignIcon(Styles.TopAlignActive, Styles.TopAlign, TextAlignment.Left, vTop);
            bool vCenter2 = DrawAlignIcon(Styles.MiddleAlignActive, Styles.MiddleAlign, TextAlignment.Center, vCenter);
            bool vBottom2 = DrawAlignIcon(Styles.BottomAlignActive, Styles.BottomAlign, TextAlignment.Right, vBottom);

            if (vTop != vTop2 || vCenter != vCenter2 || vBottom != vBottom2) {
                vTop = vTop == vTop2 ? false : vTop2;
                vCenter = vCenter == vCenter2 ? false : vCenter2;
                vBottom = vBottom == vBottom2 ? false : vBottom2;
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            TextAnchor prev = anchor;
            if (hLeft) {
                anchor = vTop
                    ? TextAnchor.UpperLeft
                    : vCenter
                        ? TextAnchor.MiddleLeft
                        : TextAnchor.LowerLeft;
            }
            else if (hCenter) {
                anchor = vTop
                    ? TextAnchor.UpperCenter
                    : vCenter
                        ? TextAnchor.MiddleCenter
                        : TextAnchor.LowerCenter;
            }
            else if (hRight) {

                anchor = vTop
                    ? TextAnchor.UpperRight
                    : vCenter
                        ? TextAnchor.MiddleRight
                        : TextAnchor.LowerRight;
            }

            if (prev != anchor) {
                prop.intValue = (int)anchor;
                prop.serializedObject.ApplyModifiedProperties();
            }
        }

        private bool DrawAlignIcon(GUIContent contentActive, GUIContent contentInactive, TextAlignment align, bool value) {
            GUIStyle style = null;

            switch (align) {
                case TextAlignment.Left:
                    style = Styles.alignmentButtonLeft;


                    break;
                case TextAlignment.Center:
                    style = Styles.alignmentButtonMid;


                    break;
                case TextAlignment.Right:
                    style = Styles.alignmentButtonRight;


                    break;
            }

            if (value) {
                EditorGUI.BeginDisabledGroup(true);
            }

            bool result = GUILayout.Toggle(value,
                value ? contentActive : contentInactive,
                style,
                GUILayout.ExpandWidth(false));

            if (value) {
                EditorGUI.EndDisabledGroup();
            }


            return result;
        }


        private static class Styles {
            public static readonly GUIStyle alignmentButtonLeft;
            public static readonly GUIStyle alignmentButtonMid;
            public static readonly GUIStyle alignmentButtonRight;
            public static GUIContent EncodingContent;
            public static readonly GUIContent LeftAlign;
            public static readonly GUIContent CenterAlign;
            public static readonly GUIContent RightAlign;
            public static readonly GUIContent TopAlign;
            public static readonly GUIContent MiddleAlign;
            public static readonly GUIContent BottomAlign;
            public static readonly GUIContent LeftAlignActive;
            public static readonly GUIContent CenterAlignActive;
            public static readonly GUIContent RightAlignActive;
            public static readonly GUIContent TopAlignActive;
            public static readonly GUIContent MiddleAlignActive;
            public static readonly GUIContent BottomAlignActive;

            static Styles() {
                alignmentButtonLeft = new GUIStyle(EditorStyles.miniButtonLeft);
                alignmentButtonMid = new GUIStyle(EditorStyles.miniButtonMid);
                alignmentButtonRight = new GUIStyle(EditorStyles.miniButtonRight);

                EncodingContent = new GUIContent("Rich Text", "Use emoticons and colors");
                LeftAlign = EditorGUIUtility.IconContent("GUISystem/align_horizontally_left", "Left Align");
                CenterAlign = EditorGUIUtility.IconContent("GUISystem/align_horizontally_center", "Center Align");
                RightAlign = EditorGUIUtility.IconContent("GUISystem/align_horizontally_right", "Right Align");
                LeftAlignActive = EditorGUIUtility.IconContent("GUISystem/align_horizontally_left_active", "Left Align");
                CenterAlignActive = EditorGUIUtility.IconContent("GUISystem/align_horizontally_center_active", "Center Align");
                RightAlignActive = EditorGUIUtility.IconContent("GUISystem/align_horizontally_right_active", "Right Align");
                TopAlign = EditorGUIUtility.IconContent("GUISystem/align_vertically_top", "Top Align");
                MiddleAlign = EditorGUIUtility.IconContent("GUISystem/align_vertically_center", "Middle Align");
                BottomAlign = EditorGUIUtility.IconContent("GUISystem/align_vertically_bottom", "Bottom Align");
                TopAlignActive = EditorGUIUtility.IconContent("GUISystem/align_vertically_top_active", "Top Align");
                MiddleAlignActive = EditorGUIUtility.IconContent("GUISystem/align_vertically_center_active", "Middle Align");
                BottomAlignActive = EditorGUIUtility.IconContent("GUISystem/align_vertically_bottom_active", "Bottom Align");

                FixAlignmentButtonStyles(alignmentButtonLeft, alignmentButtonMid, alignmentButtonRight);
            }

            private static void FixAlignmentButtonStyles(params GUIStyle[] styles) {
                var gUIStyleArray = styles;
                for (int i = 0; i < gUIStyleArray.Length; i++) {
                    GUIStyle gUIStyle = gUIStyleArray[i];
                    gUIStyle.padding.left = 2;
                    gUIStyle.padding.right = 2;
                }
            }
        }

        private enum VerticalTextAligment {
            Top,
            Middle,
            Bottom
        }
    }
}
