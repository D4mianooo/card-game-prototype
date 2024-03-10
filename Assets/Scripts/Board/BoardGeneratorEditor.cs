// using System;
// using UnityEditor;
// using UnityEditor.UIElements;
// using UnityEngine;
// using UnityEngine.UIElements;
//
// [CustomEditor(typeof(BoardGenerator))]
// [CanEditMultipleObjects]
// public class BoardGeneratorEditor : Editor {
//     public VisualTreeAsset editorXML;
//
//     public override VisualElement CreateInspectorGUI() {
//         VisualElement rootVisualElement = new VisualElement();
//         
//         editorXML.CloneTree(rootVisualElement);
//
//         rootVisualElement.Query<Button>("Generate").First().clicked += () =>
//         {
//             BoardGenerator boardGenerator = target as BoardGenerator;
//             if (boardGenerator == null) return;
//             boardGenerator.GenerateGrid();
//         };
//         rootVisualElement.Query<Button>("Clear").First().clicked += () =>
//         {
//             BoardGenerator boardGenerator = target as BoardGenerator;
//             if (boardGenerator == null) return;
//             boardGenerator.ClearGrid();
//         };
//         return rootVisualElement;
//     }
// }
