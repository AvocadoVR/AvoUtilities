using UdonSharp;
using UdonSharpEditor;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEngine.UI;


namespace AvoUtils.Editor.UIExtensions
{
    public static class ButtonExtension
    {
        public static void AddCustomEvent(this Button button, UdonSharpBehaviour behavior, string eventName)
        {
            UnityAction<string> baseEvent = UdonSharpEditorUtility.GetBackingUdonBehaviour(behavior).SendCustomEvent;
            UnityEventTools.AddStringPersistentListener(button.onClick, baseEvent, eventName);
        }

        public static void SetCustomEvent(this Button button, UdonSharpBehaviour behavior, string eventName)
        {
            RemoveAllEvents(button);

            UnityAction<string> baseEvent = UdonSharpEditorUtility.GetBackingUdonBehaviour(behavior).SendCustomEvent;
            UnityEventTools.AddStringPersistentListener(button.onClick, baseEvent, eventName);
        }

        public static void RemoveCustomEvent(this Button button, UdonSharpBehaviour behavior, string eventName)
        {
            if (HasCustomEvent(button, behavior, eventName))
            {
                for (int i = 0; button.onClick.GetPersistentEventCount() > 0; i++)
                {
                    if (button.onClick.GetPersistentMethodName(i) != eventName) continue;

                    UnityEventTools.RemovePersistentListener(button.onClick, i);
                }
            }
        }

        public static void RemoveAllEvents(this Button button)
        {
            for (int i = 0; i < button.onClick.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(button.onClick, i);
            }
        }
        public static bool HasCustomEvent(this Button button, UdonSharpBehaviour behavior, string eventName)
        {
            for (int i = 0; button.onClick.GetPersistentEventCount() > 0; i++)
            {
                if (button.onClick.GetPersistentMethodName(i) == eventName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}