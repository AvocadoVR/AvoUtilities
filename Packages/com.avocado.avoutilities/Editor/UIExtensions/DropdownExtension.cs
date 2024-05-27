using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropdownExtension : UIExtensionBase<Dropdown>
{
    protected override UnityEventBase GetUnityEvent(Dropdown Component)
    {
        return Component.onValueChanged;
    }
}
