using UnityEngine;
using UnityEngine.UI;
using UdonSharp;
using AvoUtils.Editor.UIExtensions;
using System;
using System.Drawing.Design;

public static class AvoUI
{
    public static ButtonExtension ButtonUtil { get; } = new ButtonExtension();
    public static ToggleExtension ToggleUtil { get; } = new ToggleExtension();
    public static DropdownExtension DropdownUtil { get; } = new DropdownExtension();
    public static InputFieldExtension InputFieldUtil { get; } = new InputFieldExtension();
}
