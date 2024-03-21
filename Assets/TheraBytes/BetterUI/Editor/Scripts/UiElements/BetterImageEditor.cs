using System.Linq;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TheraBytes.BetterUi.Editor {
    [CustomEditor(typeof(BetterImage))] [CanEditMultipleObjects]
    public class BetterImageEditor : GraphicEditor {
        private BetterImage image;
        private GUIContent m_ClockwiseContent;
        private SerializedProperty m_FillAmount;
        private SerializedProperty m_FillCenter;
        private SerializedProperty m_FillClockwise;
        private SerializedProperty m_FillMethod;
        private SerializedProperty m_FillOrigin;
        private SerializedProperty m_maskable;
        private SerializedProperty m_pixelPerUnitMultiplyer;
        private SerializedProperty m_PreserveAspect;
        private AnimBool m_ShowFilled;
        private AnimBool m_ShowSliced;

        private AnimBool m_ShowSlicedOrTiled;
        private AnimBool m_ShowType;
        private SerializedProperty m_Sprite;

        private GUIContent m_SpriteContent;
        private GUIContent m_SpriteTypeContent;
        private SerializedProperty m_Type;
        private SerializedProperty m_useSpriteMesh;
        private GUIContent m_UseSpriteMeshContent;

        private ImageAppearanceProviderEditorHelper materialDrawer;
        private SerializedProperty spriteSetingsCollectionProp;

        private SerializedProperty spriteSetingsFallbackProp;

        protected override void OnEnable() {
            image = target as BetterImage;
            materialDrawer = new ImageAppearanceProviderEditorHelper(serializedObject, image);

            base.OnEnable();
            m_SpriteContent = new GUIContent("Source Image");
            m_SpriteTypeContent = new GUIContent("Image Type");
            m_ClockwiseContent = new GUIContent("Clockwise");
            m_UseSpriteMeshContent = new GUIContent("Use Sprite Mesh",
                "In Better UI, this option is not supported. If you need it, please use an Image, not Better Image.");

            m_Sprite = serializedObject.FindProperty("m_Sprite");
            m_Type = serializedObject.FindProperty("m_Type");
            m_FillCenter = serializedObject.FindProperty("m_FillCenter");
            m_FillMethod = serializedObject.FindProperty("m_FillMethod");
            m_FillOrigin = serializedObject.FindProperty("m_FillOrigin");
            m_FillClockwise = serializedObject.FindProperty("m_FillClockwise");
            m_FillAmount = serializedObject.FindProperty("m_FillAmount");
            m_PreserveAspect = serializedObject.FindProperty("m_PreserveAspect");
            m_useSpriteMesh = serializedObject.FindProperty("m_UseSpriteMesh");
            m_pixelPerUnitMultiplyer = serializedObject.FindProperty("m_PixelsPerUnitMultiplier");
            m_maskable = serializedObject.FindProperty("m_Maskable");

            spriteSetingsFallbackProp = serializedObject.FindProperty("fallbackSpriteSettings");
            spriteSetingsCollectionProp = serializedObject.FindProperty("customSpriteSettings");


            m_ShowType = new AnimBool(m_Sprite.objectReferenceValue != null);
            m_ShowType.valueChanged.AddListener(Repaint);
            Image.Type mType = (Image.Type)m_Type.enumValueIndex;
            m_ShowSlicedOrTiled = new AnimBool(m_Type.hasMultipleDifferentValues ? false : mType == Image.Type.Sliced);
            m_ShowSliced = new AnimBool(m_Type.hasMultipleDifferentValues ? false : mType == Image.Type.Sliced);
            m_ShowFilled = new AnimBool(m_Type.hasMultipleDifferentValues ? false : mType == Image.Type.Filled);
            m_ShowSlicedOrTiled.valueChanged.AddListener(Repaint);
            m_ShowSliced.valueChanged.AddListener(Repaint);
            m_ShowFilled.valueChanged.AddListener(Repaint);
            SetShowNativeSize(true);

        }

        public override bool HasPreviewGUI() {
            image = target as BetterImage;
            if (image == null) {
                return false;
            }


            return image.sprite != null;
        }

        public override string GetInfoString() {
            image = target as BetterImage;
            Rect rect = image.rectTransform.rect;
            object num = Mathf.RoundToInt(Mathf.Abs(rect.width));
            Rect rect1 = image.rectTransform.rect;


            return string.Format("Image Size: {0}x{1}", num, Mathf.RoundToInt(Mathf.Abs(rect1.height)));
        }

        private void SetShowNativeSize(bool instant) {
            Image.Type mType = (Image.Type)m_Type.enumValueIndex;
            base.SetShowNativeSize(mType == Image.Type.Simple ? true : mType == Image.Type.Filled, instant);
        }


        public override void OnInspectorGUI() {
            serializedObject.Update();

            ScreenConfigConnectionHelper.DrawGui("Sprite Settings",
                spriteSetingsCollectionProp,
                ref spriteSetingsFallbackProp,
                DrawSpriteSettings);


            EditorGUILayout.Separator();
            if (image.type == Image.Type.Filled) {
                // materials not (yet) supported for filled images
                EditorGUILayout.PropertyField(m_Material);
            }
            else {
                // draw color and material
                materialDrawer.DrawMaterialGui(m_Material);
            }
            EditorGUILayout.Separator();


            RaycastControlsGUI();
            m_ShowType.target = m_Sprite.objectReferenceValue != null;
            if (EditorGUILayout.BeginFadeGroup(m_ShowType.faded)) {
                TypeGUI();
            }
            EditorGUILayout.EndFadeGroup();
            SetShowNativeSize(false);
            if (EditorGUILayout.BeginFadeGroup(m_ShowNativeSize.faded)) {
                EditorGUI.indentLevel = EditorGUI.indentLevel + 1;
                if (m_useSpriteMesh != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.Toggle(m_UseSpriteMeshContent, false);
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUILayout.PropertyField(m_PreserveAspect);
                EditorGUI.indentLevel = EditorGUI.indentLevel - 1;
            }
            EditorGUILayout.EndFadeGroup();
            NativeSizeButtonGUI();
            serializedObject.ApplyModifiedProperties();

            if (image.type == Image.Type.Sliced) {

                SerializedProperty prop = serializedObject.FindProperty("keepBorderAspectRatio");
                EditorGUILayout.PropertyField(prop);
                serializedObject.ApplyModifiedProperties();
            }

            if (image.type == Image.Type.Sliced || image.type == Image.Type.Tiled) {
                SerializedProperty prop = serializedObject.FindProperty("spriteBorderScaleFallback");
                SerializedProperty collection = serializedObject.FindProperty("customBorderScales");
                //EditorGUILayout.PropertyField(prop);

                ScreenConfigConnectionHelper.DrawSizerGui("Border Scale", collection, ref prop);
                serializedObject.ApplyModifiedProperties();
            }

            if (m_maskable != null) {
                EditorGUILayout.PropertyField(m_maskable);
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DrawSpriteSettings(string configName, SerializedProperty property) {
            SerializedProperty spriteProp = property.FindPropertyRelative("Sprite");
            SerializedProperty primeColorProp = property.FindPropertyRelative("PrimaryColor");

            SpriteGUI(spriteProp);

            // coloring not supported for Filled yet.
            if (image.type != Image.Type.Filled) {
                SerializedProperty colorModeProp = property.FindPropertyRelative("ColorMode");
                SerializedProperty secondColorProp = property.FindPropertyRelative("SecondaryColor");
                ImageAppearanceProviderEditorHelper.DrawColorGui(colorModeProp, primeColorProp, secondColorProp);
            }
            else {
                EditorGUILayout.PropertyField(primeColorProp);
            }
        }

        /// <summary>
        ///     <para>Custom preview for Image component.</para>
        /// </summary>
        /// <param name="rect">Rectangle in which to draw the preview.</param>
        /// <param name="background">Background image.</param>
        public override void OnPreviewGUI(Rect rect, GUIStyle background) {
            if (Event.current.type == EventType.Repaint) {
                Image image = target as Image;
                if (image == null) {
                    return;
                }
                Sprite sprite = image.sprite;
                if (sprite == null) {
                    return;
                }

                Texture2D preview = AssetPreview.GetAssetPreview(sprite);
                EditorGUI.DrawTextureTransparent(rect, preview, ScaleMode.ScaleToFit, 1f);
            }
        }


        /// <summary>
        ///     <para>GUI for showing the Sprite property.</para>
        /// </summary>
        protected void SpriteGUI(SerializedProperty spriteProp) {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(spriteProp, m_SpriteContent);
            if (EditorGUI.EndChangeCheck()) {
                Sprite mSprite = spriteProp.objectReferenceValue as Sprite;
                if (mSprite) {
                    Image.Type mType = (Image.Type)m_Type.enumValueIndex;
                    if (mSprite.border.SqrMagnitude() > 0f) {
                        m_Type.enumValueIndex = 1;
                    }
                    else if (mType == Image.Type.Sliced) {
                        m_Type.enumValueIndex = 0;
                    }
                }
            }
        }

        /// <summary>
        ///     <para>GUI for showing the image type and associated settings.</para>
        /// </summary>
        protected void TypeGUI() {
            bool flag;
            EditorGUILayout.PropertyField(m_Type, m_SpriteTypeContent);
            EditorGUI.indentLevel = EditorGUI.indentLevel + 1;
            Image.Type mType = (Image.Type)m_Type.enumValueIndex;
            if (m_Type.hasMultipleDifferentValues) {
                flag = false;
            }
            else {
                flag = mType == Image.Type.Sliced ? true : mType == Image.Type.Tiled;
            }
            bool flag1 = flag;
            if (flag1 && targets.Length > 1) {
                flag1 = (
                    from obj in targets
                    select obj as Image).All(img => img.hasBorder);
            }
            m_ShowSlicedOrTiled.target = flag1;
            m_ShowSliced.target = !flag1 || m_Type.hasMultipleDifferentValues ? false : mType == Image.Type.Sliced;
            m_ShowFilled.target = m_Type.hasMultipleDifferentValues ? false : mType == Image.Type.Filled;
            Image image = target as Image;
            if (EditorGUILayout.BeginFadeGroup(m_ShowSlicedOrTiled.faded) || image.hasBorder) {
                if (mType == Image.Type.Sliced) {
                    EditorGUILayout.PropertyField(m_FillCenter);
                }

                if (m_pixelPerUnitMultiplyer != null) {
                    EditorGUILayout.PropertyField(m_pixelPerUnitMultiplyer);
                }
            }
            EditorGUILayout.EndFadeGroup();
            if (EditorGUILayout.BeginFadeGroup(m_ShowSliced.faded) && image.sprite != null && !image.hasBorder) {
                EditorGUILayout.HelpBox("This Image doesn't have a border.", MessageType.Warning);
            }
            EditorGUILayout.EndFadeGroup();
            if (EditorGUILayout.BeginFadeGroup(m_ShowFilled.faded)) {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(m_FillMethod);
                if (EditorGUI.EndChangeCheck()) {
                    m_FillOrigin.intValue = 0;
                }
                switch (m_FillMethod.enumValueIndex) {
                    case 0:
                    {
                        m_FillOrigin.intValue =
                            (int)(Image.OriginHorizontal)EditorGUILayout.EnumPopup("Fill Origin",
                                (Image.OriginHorizontal)m_FillOrigin.intValue);


                        break;
                    }
                    case 1:
                    {
                        m_FillOrigin.intValue =
                            (int)(Image.OriginVertical)EditorGUILayout.EnumPopup("Fill Origin",
                                (Image.OriginVertical)m_FillOrigin.intValue);


                        break;
                    }
                    case 2:
                    {
                        m_FillOrigin.intValue =
                            (int)(Image.Origin90)EditorGUILayout.EnumPopup("Fill Origin", (Image.Origin90)m_FillOrigin.intValue);


                        break;
                    }
                    case 3:
                    {
                        m_FillOrigin.intValue =
                            (int)(Image.Origin180)EditorGUILayout.EnumPopup("Fill Origin",
                                (Image.Origin180)m_FillOrigin.intValue);


                        break;
                    }
                    case 4:
                    {
                        m_FillOrigin.intValue =
                            (int)(Image.Origin360)EditorGUILayout.EnumPopup("Fill Origin",
                                (Image.Origin360)m_FillOrigin.intValue);


                        break;
                    }
                }
                EditorGUILayout.PropertyField(m_FillAmount);
                if (m_FillMethod.enumValueIndex > 1) {
                    EditorGUILayout.PropertyField(m_FillClockwise, m_ClockwiseContent);
                }
            }
            EditorGUILayout.EndFadeGroup();
            EditorGUI.indentLevel = EditorGUI.indentLevel - 1;
        }

        [MenuItem("CONTEXT/Image/♠ Make Better")]
        public static void MakeBetter(MenuCommand command) {
            Image img = command.context as Image;
            Image newImg = Betterizer.MakeBetter<Image, BetterImage>(img);
            Sprite sprite = img.sprite;
            Color col = img.color;

            if (newImg != null) {
                newImg.SetAllDirty();

                BetterImage better = newImg as BetterImage;
                if (better != null) {
                    // set border scale both to height as default to make default scale uniform.
                    better.SpriteBorderScale.ModX.SizeModifiers[0].Mode = ImpactMode.PixelHeight;
                    better.SpriteBorderScale.ModY.SizeModifiers[0].Mode = ImpactMode.PixelHeight;

                    better.CurrentSpriteSettings.Sprite = sprite;
                    better.CurrentSpriteSettings.PrimaryColor = col;

#if !UNITY_4 && !UNITY_5_0 && !UNITY_5_1 && !UNITY_5_2 && !UNITY_5_3 && !UNITY_5_4 && !UNITY_5_5 // from UNITY 5.6 on
                    // ensure shader channels in canvas
                    Canvas canvas = better.transform.GetComponentInParent<Canvas>();
                    canvas.additionalShaderChannels = canvas.additionalShaderChannels
                                                      | AdditionalCanvasShaderChannels.TexCoord1
                                                      | AdditionalCanvasShaderChannels.Tangent;
#endif
                }

            }
        }
    }
}
