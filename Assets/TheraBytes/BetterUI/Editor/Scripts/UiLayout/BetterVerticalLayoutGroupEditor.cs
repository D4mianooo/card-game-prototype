using UnityEditor;
using UnityEngine.UI;

namespace TheraBytes.BetterUi.Editor {
#pragma warning disable 0618

    [CustomEditor(typeof(BetterVerticalLayoutGroup))] [CanEditMultipleObjects]
    public class BetterVerticalLayoutGroupEditor
        : BetterHorizontalOrVerticalLayoutGroupEditor<VerticalLayoutGroup, BetterVerticalLayoutGroup> {
        public override void OnInspectorGUI() {
            DrawObsoleteWarning();
            base.OnInspectorGUI();
        }
    }

#pragma warning restore 0618

}
