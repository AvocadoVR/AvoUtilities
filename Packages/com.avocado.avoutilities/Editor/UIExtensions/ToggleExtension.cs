using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleExtension : UIExtensionBase<Toggle>
{
    protected override UnityEventBase GetUnityEvent(Toggle Component)
    {
        return Component.onValueChanged;
    }
}
