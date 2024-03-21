using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

namespace TheraBytes.BetterUi.Editor {
    [CustomEditor(typeof(BetterDropdown))] [CanEditMultipleObjects]
    public class BetterDropDownEditor : DropdownEditor {
        private readonly BetterElementHelper<Dropdown, BetterDropdown> helper = new();

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            helper.DrawGui(serializedObject);

            serializedObject.ApplyModifiedProperties();
        }

        [MenuItem("CONTEXT/Dropdown/♠ Make Better")]
        public static void MakeBetter(MenuCommand command) {
            Dropdown sel = command.context as Dropdown;
            Betterizer.MakeBetter<Dropdown, BetterDropdown>(sel);
        }
    }
}
