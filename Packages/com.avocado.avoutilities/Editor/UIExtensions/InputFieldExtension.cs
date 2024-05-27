using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum InputFieldEvent
{
    OnChangeEvent,
    EndEditEvent,
    SubmitEvent
}

namespace AvoUtils.Editor.UIExtensions
{
    public class InputFieldExtension : UIExtensionBase<InputField, InputFieldEvent>
    {
        protected override UnityEventBase GetUnityEvent(InputField Component, InputFieldEvent inputFieldEvent)
        {
            return inputFieldEvent switch
            {
                InputFieldEvent.OnChangeEvent => Component.onValueChanged,
                InputFieldEvent.EndEditEvent => Component.onEndEdit,
                InputFieldEvent.SubmitEvent => Component.onSubmit,
                _ => null
            };
        }
    }
}
