#if !COMPILER_UDONSHARP && UNITY_EDITOR
using UdonSharp;
using UdonSharpEditor;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AvoUtils.Editor.UIExtensions
{
    public static class DropdownExtension
    {
        public static void AddCustomEvent(this Dropdown dropdown, UdonSharpBehaviour behavior, string eventName)
        {
            UnityAction<string> baseEvent = UdonSharpEditorUtility.GetBackingUdonBehaviour(behavior).SendCustomEvent;
            UnityEventTools.AddStringPersistentListener(dropdown.onValueChanged, baseEvent, eventName);
        }

        public static void SetCustomEvent(this Dropdown dropdown, UdonSharpBehaviour behavior, string eventName)
        {
            RemoveAllEvents(dropdown);

            UnityAction<string> baseEvent = UdonSharpEditorUtility.GetBackingUdonBehaviour(behavior).SendCustomEvent;
            UnityEventTools.AddStringPersistentListener(dropdown.onValueChanged, baseEvent, eventName);
        }

        public static void RemoveCustomEvent(this Dropdown dropdown, UdonSharpBehaviour behavior, string eventName)
        {
            if (HasCustomEvent(dropdown, behavior, eventName))
            {
                for (int i = 0; dropdown.onValueChanged.GetPersistentEventCount() > 0; i++)
                {
                    if (dropdown.onValueChanged.GetPersistentMethodName(i) != eventName) continue;

                    UnityEventTools.RemovePersistentListener(dropdown.onValueChanged, i);
                }
            }
        }

        public static void RemoveAllEvents(this Dropdown dropdown)
        {
            for (int i = 0; i < dropdown.onValueChanged.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(dropdown.onValueChanged, i);
            }
        }
        public static bool HasCustomEvent(this Dropdown dropdown, UdonSharpBehaviour behavior, string eventName)
        {
            for (int i = 0; dropdown.onValueChanged.GetPersistentEventCount() > 0; i++)
            {
                if (dropdown.onValueChanged.GetPersistentMethodName(i) == eventName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
#endif