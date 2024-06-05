using UdonSharp;
using UdonSharpEditor;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AvoUtils.Editor.UIExtensions
{
    public static class ToggleExtension
    {
        public static void AddCustomEvent(this Toggle toggle, UdonSharpBehaviour behavior, string eventName)
        {
            UnityAction<string> baseEvent = UdonSharpEditorUtility.GetBackingUdonBehaviour(behavior).SendCustomEvent;
            UnityEventTools.AddStringPersistentListener(toggle.onValueChanged, baseEvent, eventName);
        }

        public static void SetCustomEvent(this Toggle toggle, UdonSharpBehaviour behavior, string eventName)
        {
            RemoveAllEvents(toggle);

            UnityAction<string> baseEvent = UdonSharpEditorUtility.GetBackingUdonBehaviour(behavior).SendCustomEvent;
            UnityEventTools.AddStringPersistentListener(toggle.onValueChanged, baseEvent, eventName);
        }

        public static void RemoveCustomEvent(this Toggle toggle, UdonSharpBehaviour behavior, string eventName)
        {
            if (HasCustomEvent(toggle, behavior, eventName))
            {
                for (int i = 0; toggle.onValueChanged.GetPersistentEventCount() > 0; i++)
                {
                    if (toggle.onValueChanged.GetPersistentMethodName(i) != eventName) continue;

                    UnityEventTools.RemovePersistentListener(toggle.onValueChanged, i);
                }
            }
        }

        public static void RemoveAllEvents(this Toggle toggle)
        {
            for (int i = 0; i < toggle.onValueChanged.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(toggle.onValueChanged, i);
            }
        }
        public static bool HasCustomEvent(this Toggle toggle, UdonSharpBehaviour behavior, string eventName)
        {
            for (int i = 0; toggle.onValueChanged.GetPersistentEventCount() > 0; i++)
            {
                if (toggle.onValueChanged.GetPersistentMethodName(i) == eventName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
