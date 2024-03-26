using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class FolderCreator : EditorWindow{
    [MenuItem("Assets/Structurizer/Example")]
    private static void Example() {
        FolderCreator folderCreator = GetWindow<FolderCreator>();
        folderCreator.titleContent = new GUIContent("Folder Creator");
    }
    private void CreateGUI() {
        VisualElement root = rootVisualElement;

        Button createBtn = new Button();
        createBtn.name = "create";
        createBtn.text = "Create Folder Structure";
        createBtn.clicked += CreateFolders;
        root.Add(createBtn);

    }

    private void CreateFolders() {
        List<String> folders = new List<string>()
        {
            "Animations",
            "Audio",
            "Editor",
            "Fonts",
            "Materials",
            "Meshes",
            "Particles",
            "Prefabs",
            "Scripts",
            "Settings",
            "Shaders",
            "Scenes",
            "Textures",
            "ThirdParty"
        };
        foreach (string folder in folders) {
            string path = "Assets/" + folder;
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }
        AssetDatabase.Refresh();
    }
    
}
