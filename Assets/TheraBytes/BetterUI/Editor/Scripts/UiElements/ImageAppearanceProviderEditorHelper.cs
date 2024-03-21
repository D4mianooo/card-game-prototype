using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheraBytes.BetterUi.Editor {
    public class ImageAppearanceProviderEditorHelper {
        private static readonly string DEFAULT = "Default";
        private static readonly string CUSTOM = "Custom";
        private readonly IImageAppearanceProvider img;
        private int materialIndex, materialEffectIndex;

        private readonly SerializedProperty materialProperty1;
        private readonly SerializedProperty materialProperty2;
        private readonly SerializedProperty materialProperty3;

        private readonly string[] materials;
        private readonly SerializedProperty propMatType;
        private readonly SerializedProperty propEffType;

        private readonly SerializedObject serializedObject;

        public ImageAppearanceProviderEditorHelper(SerializedObject serializedObject, IImageAppearanceProvider img) {
            this.serializedObject = serializedObject;
            this.img = img;

            materialProperty1 = serializedObject.FindProperty("materialProperty1");
            materialProperty2 = serializedObject.FindProperty("materialProperty2");
            materialProperty3 = serializedObject.FindProperty("materialProperty3");

            propMatType = serializedObject.FindProperty("materialType");
            propEffType = serializedObject.FindProperty("materialEffect");


            var materialOptions = new List<string>();
            materialOptions.Add(DEFAULT);
            materialOptions.AddRange(Materials.Instance.GetAllMaterialNames());
            materialOptions.Add(CUSTOM);
            materials = materialOptions.ToArray();

            materialIndex = materialOptions.IndexOf(img.MaterialType);
            if (materialIndex < 0) {
                materialIndex = 0;
            }

            var effectOptions = Materials.Instance.GetAllMaterialEffects(img.MaterialType).ToList();
            materialEffectIndex = effectOptions.IndexOf(img.MaterialEffect);
            if (materialEffectIndex < 0) {
                materialEffectIndex = 0;
            }
        }

        public void DrawMaterialGui(SerializedProperty materialProp) {

            // MATERIAL
            materialIndex = EditorGUILayout.Popup("Material", materialIndex, materials);
            string materialType = materials[materialIndex];

            MaterialEffect effect;
            if (materialType == CUSTOM || materialType == DEFAULT) {
                effect = MaterialEffect.Normal;
            }
            else {
                string[] options = Materials.Instance.GetAllMaterialEffects(materialType).Select(o => o.ToString()).ToArray();
                materialEffectIndex = EditorGUILayout.Popup("Effect", materialEffectIndex, options);
                if (materialEffectIndex >= options.Length) {
                    materialEffectIndex = 0;
                }

                effect = (MaterialEffect)Enum.Parse(typeof(MaterialEffect), options[materialEffectIndex]);
            }


            Materials.MaterialInfo materialInfo = Materials.Instance.GetMaterialInfo(materialType, effect);
            Material material = materialInfo != null ? materialInfo.Material : null;
            SerializedProperty propVars = serializedObject.FindProperty("materialProperties");

            // material type changed
            bool materialChanged = propMatType.stringValue != materialType;
            bool effectChanged = propEffType.enumValueIndex != (int)effect;
            if (materialChanged || effectChanged) {
                propMatType.stringValue = materialType;
                materialProp.objectReferenceValue = material;

                propEffType.enumValueIndex = (int)effect;

                int infoIdx = Materials.Instance.GetMaterialInfoIndex(materialType, effect);
                if (infoIdx >= 0) {
                    SerializedObject obj = new(Materials.Instance);
                    SerializedProperty source = obj.FindProperty("materials")
                        .GetArrayElementAtIndex(infoIdx)
                        .FindPropertyRelative("Properties");

                    SerializedPropertyUtil.Copy(source, propVars);
                    propVars = serializedObject.FindProperty("materialProperties");
                    serializedObject.ApplyModifiedPropertiesWithoutUndo();

                    // update material properties
                    SerializedProperty floats = propVars.FindPropertyRelative("FloatProperties");
                    if (floats != null) {
                        for (int i = 0; i < floats.arraySize; i++) {
                            SerializedProperty p = floats.GetArrayElementAtIndex(i);
                            SerializedProperty innerProp = p.FindPropertyRelative("Value");
                            if (innerProp == null) {
                                continue;
                            }

                            SerializedProperty valProp;
                            switch (i) {
                                case 0:
                                    valProp = materialProperty1;


                                    break;
                                case 1:
                                    valProp = materialProperty2;


                                    break;
                                case 2:
                                    valProp = materialProperty3;


                                    break;
                                default: throw new ArgumentOutOfRangeException();
                            }

                            if (materialChanged) {
                                valProp.floatValue = innerProp.floatValue;
                            }
                            else if (effectChanged) {
                                innerProp.floatValue = valProp.floatValue;
                            }
                        }
                    }
                }
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }

            if (materialType == CUSTOM) {
                EditorGUILayout.PropertyField(materialProp);
            }
            else if (materialType != DEFAULT) {
                SerializedProperty floats = propVars.FindPropertyRelative("FloatProperties");
                if (floats != null) {
                    for (int i = 0; i < floats.arraySize; i++) {
                        VertexMaterialData.FloatProperty f = img.MaterialProperties.FloatProperties[i];
                        SerializedProperty p = floats.GetArrayElementAtIndex(i);
                        string displayName = p.FindPropertyRelative("Name").stringValue;

                        SerializedProperty valProp;
                        switch (i) {
                            case 0:
                                valProp = materialProperty1;


                                break;
                            case 1:
                                valProp = materialProperty2;


                                break;
                            case 2:
                                valProp = materialProperty3;


                                break;
                            default: throw new ArgumentOutOfRangeException();
                        }

                        if (f.IsRestricted) {
                            EditorGUILayout.Slider(valProp, f.Min, f.Max, displayName);
                        }
                        else {
                            EditorGUILayout.PropertyField(valProp, new GUIContent(displayName));
                        }

                        SerializedProperty innerProp = p.FindPropertyRelative("Value");
                        innerProp.floatValue = valProp.floatValue;
                    }
                }

            }

            if (materialType == CUSTOM && materialProp.objectReferenceValue != null) {
                bool isOrig = !materialProp.objectReferenceValue.name.EndsWith("(Clone)"); // TODO: find better check
                EditorGUILayout.BeginHorizontal();

                GUILayout.Label(isOrig ? "Material: SHARED" : "Material: CLONED",
                    GUILayout.Width(EditorGUIUtility.labelWidth));

                if (GUILayout.Button(isOrig ? "Clone" : "Remove",
                        EditorStyles.miniButton)) {
                    materialProp.objectReferenceValue = isOrig
                        ? Object.Instantiate(img.material)
                        : null;

                    img.SetMaterialDirty();
                }

                EditorGUILayout.EndHorizontal();
            }

        }

        public static void DrawColorGui(SerializedProperty colorMode, SerializedProperty firstColor,
                                        SerializedProperty secondColor) {
            // COLOR
            EditorGUILayout.PropertyField(colorMode);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();

            EditorGUILayout.PropertyField(firstColor);

            ColorMode mode = (ColorMode)colorMode.intValue;
            if (mode != ColorMode.Color) {
                EditorGUILayout.PropertyField(secondColor);
            }

            EditorGUILayout.EndVertical();
            if (mode != ColorMode.Color) {
                if (GUILayout.Button("↕",
                        GUILayout.Width(25),
                        GUILayout.Height(2 * EditorGUIUtility.singleLineHeight))) {
                    Color a = firstColor.colorValue;
                    firstColor.colorValue = secondColor.colorValue;
                    secondColor.colorValue = a;
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();
        }
    }
}
