using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

namespace TheraBytes.BetterUi.Editor {
    [CustomEditor(typeof(BetterButton))] [CanEditMultipleObjects]
    public class BetterButtonEditor : ButtonEditor {
        private readonly BetterElementHelper<Button, BetterButton> helper = new();

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            helper.DrawGui(serializedObject);

            serializedObject.ApplyModifiedProperties();
        }

        [MenuItem("CONTEXT/Button/♠ Make Better")]
        public static void MakeBetter(MenuCommand command) {
            Button btn = command.context as Button;
            Betterizer.MakeBetter<Button, BetterButton>(btn);
        }
    }
}
