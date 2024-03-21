using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

namespace TheraBytes.BetterUi.Editor {
    [CustomEditor(typeof(BetterSelectable))] [CanEditMultipleObjects]
    public class BetterSelectableEditor : SelectableEditor {
        private readonly BetterElementHelper<Selectable, BetterSelectable> helper = new();

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            helper.DrawGui(serializedObject);

            serializedObject.ApplyModifiedProperties();
        }

        [MenuItem("CONTEXT/Selectable/♠ Make Better")]
        public static void MakeBetter(MenuCommand command) {
            Selectable sel = command.context as Selectable;
            Betterizer.MakeBetter<Selectable, BetterSelectable>(sel);
        }
    }
}
