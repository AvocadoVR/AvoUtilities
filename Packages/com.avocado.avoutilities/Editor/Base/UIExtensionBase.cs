using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UdonSharp;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using VRC.Udon.Graph.NodeRegistries;

public abstract class UIExtensionBase<TComponent, TEnum>
    where TComponent : Component
    where TEnum : struct, System.Enum
{
    protected virtual UnityEventBase GetUnityEvent(TComponent Component, TEnum Enum = default)
    {
        return null;
    }

    public void AddCustomEvent(TComponent component, TEnum componentEvent, UdonSharpBehaviour behavior, string eventName)
    {
        UnityEventBase unityEvent = GetUnityEvent(component, componentEvent);
        UnityAction<string> baseEvent = behavior.SendCustomEvent;
        UnityEventTools.AddStringPersistentListener(unityEvent, baseEvent, eventName);
    }

    public void SetCustomEvent(TComponent component, TEnum componentEvent, UdonSharpBehaviour behavior, string eventName)
    {
        ResetEventInput(component, componentEvent);
        UnityEventBase unityEvent = GetUnityEvent(component, componentEvent);
        UnityAction<string> baseEvent = behavior.SendCustomEvent;
        UnityEventTools.AddStringPersistentListener(unityEvent, baseEvent, eventName);
    }

    public void ResetEventInput(TComponent component, TEnum componentEvent)
    {
        UnityEventBase unityEvent = GetUnityEvent(component, componentEvent);

        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
        {
            UnityEventTools.RemovePersistentListener(unityEvent, i);
        }
    }

    public void RemoveAllEvents(TComponent component, TEnum componentEvent)
    {
        UnityEventBase unityEvent = GetUnityEvent(component, componentEvent);

        (unityEvent as UnityEvent)?.RemoveAllListeners();
    }


}

public abstract class UIExtensionBase<TComponent>
    where TComponent : Component
{
    protected virtual UnityEventBase GetUnityEvent(TComponent Component)
    {
        return null;
    }

    public void AddCustomEvent(TComponent component, UdonSharpBehaviour behavior, string eventName)
    {
        UnityEventBase unityEvent = GetUnityEvent(component);
        UnityAction<string> baseEvent = behavior.SendCustomEvent;
        UnityEventTools.AddStringPersistentListener(unityEvent, baseEvent, eventName);
    }

    public void SetCustomEvent(TComponent component, UdonSharpBehaviour behavior, string eventName)
    {
        ResetEventInput(component);
        UnityEventBase unityEvent = GetUnityEvent(component);
        UnityAction<string> baseEvent = behavior.SendCustomEvent;
        UnityEventTools.AddStringPersistentListener(unityEvent, baseEvent, eventName);
    }

    public void ResetEventInput(TComponent component)
    {
        UnityEventBase unityEvent = GetUnityEvent(component);

        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
        {
            UnityEventTools.RemovePersistentListener(unityEvent, i);
        }
    }
}
