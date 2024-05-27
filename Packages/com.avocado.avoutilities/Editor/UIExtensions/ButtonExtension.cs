using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonExtension : UIExtensionBase<Button>
{
    protected override UnityEventBase GetUnityEvent(Button Component)
    {
        return Component.onClick;
    }
}
