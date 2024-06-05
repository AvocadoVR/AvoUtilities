using UdonSharp;
using UdonSharpEditor;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEngine.UI;



namespace AvoUtils.Editor.UIExtensions
{
    public enum InputFieldEvent
    {
        OnChangeEvent,
        EndEditEvent,
        SubmitEvent
    }

    public static class InputFieldExtension
    {
        public static void AddCustomEvent(this InputField inputField, InputFieldEvent inputFieldEvent, UdonSharpBehaviour behavior, string eventName)
        {
            UnityEventBase unityEventBase = null;

            switch (inputFieldEvent)
            {
                case InputFieldEvent.OnChangeEvent:
                    unityEventBase = inputField.onValueChanged;
                    break;
                case InputFieldEvent.EndEditEvent:
                    unityEventBase = inputField.onEndEdit;
                    break;
                case InputFieldEvent.SubmitEvent:
                    unityEventBase = inputField.onSubmit;
                    break;
            }

            UnityAction<string> baseEvent = UdonSharpEditorUtility.GetBackingUdonBehaviour(behavior).SendCustomEvent;
            UnityEventTools.AddStringPersistentListener(unityEventBase, baseEvent, eventName);
        }

        public static void SetCustomEvent(this InputField inputField, InputFieldEvent inputFieldEvent, UdonSharpBehaviour behavior, string eventName)
        {
            UnityEventBase unityEventBase = null;

            switch (inputFieldEvent)
            {
                case InputFieldEvent.OnChangeEvent:
                    unityEventBase = inputField.onValueChanged;
                    break;
                case InputFieldEvent.EndEditEvent:
                    unityEventBase = inputField.onEndEdit;
                    break;
                case InputFieldEvent.SubmitEvent:
                    unityEventBase = inputField.onSubmit;
                    break;
            }

            RemoveAllEvents(inputField);

            UnityAction<string> baseEvent = UdonSharpEditorUtility.GetBackingUdonBehaviour(behavior).SendCustomEvent;
            UnityEventTools.AddStringPersistentListener(unityEventBase, baseEvent, eventName);
        }

        public static void RemoveCustomEvent(this InputField inputField, InputFieldEvent inputFieldEvent, UdonSharpBehaviour behavior, string eventName)
        {
            UnityEventBase unityEventBase = null;

            switch (inputFieldEvent)
            {
                case InputFieldEvent.OnChangeEvent:
                    unityEventBase = inputField.onValueChanged;
                    break;
                case InputFieldEvent.EndEditEvent:
                    unityEventBase = inputField.onEndEdit;
                    break;
                case InputFieldEvent.SubmitEvent:
                    unityEventBase = inputField.onSubmit;
                    break;
            }

            if (HasCustomEvent(inputField, inputFieldEvent, behavior, eventName))
            {
                for (int i = 0; inputField.onValueChanged.GetPersistentEventCount() > 0; i++)
                {
                    if (unityEventBase.GetPersistentMethodName(i) != eventName) continue;

                    UnityEventTools.RemovePersistentListener(inputField.onValueChanged, i);
                }
            }
        }

        public static void RemoveAllEvents(this InputField inputField)
        {
            for (int i = 0; i < inputField.onValueChanged.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(inputField.onValueChanged, i);
            }

            for (int i = 0; i < inputField.onValueChanged.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(inputField.onEndEdit, i);
            }

            for (int i = 0; i < inputField.onValueChanged.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(inputField.onSubmit, i);
            }
        }

        public static bool HasCustomEvent(this InputField inputField, InputFieldEvent inputFieldEvent, UdonSharpBehaviour behavior, string eventName)
        {
            switch (inputFieldEvent)
            {
                case InputFieldEvent.OnChangeEvent:
                    for (int i = 0; inputField.onValueChanged.GetPersistentEventCount() > 0; i++)
                    {
                        if (inputField.onValueChanged.GetPersistentMethodName(i) == eventName)
                        {
                            return true;
                        }
                    }
                    break;
                case InputFieldEvent.EndEditEvent:
                    for (int i = 0; inputField.onValueChanged.GetPersistentEventCount() > 0; i++)
                    {
                        if (inputField.onEndEdit.GetPersistentMethodName(i) == eventName)
                        {
                            return true;
                        }
                    }
                    break;
                case InputFieldEvent.SubmitEvent:
                    for (int i = 0; inputField.onValueChanged.GetPersistentEventCount() > 0; i++)
                    {
                        if (inputField.onSubmit.GetPersistentMethodName(i) == eventName)
                        {
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }
    }


}
