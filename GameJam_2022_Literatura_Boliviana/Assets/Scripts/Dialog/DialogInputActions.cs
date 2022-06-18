//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Dialog/DialogInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @DialogInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DialogInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DialogInputActions"",
    ""maps"": [
        {
            ""name"": ""Dialog"",
            ""id"": ""66a4424c-5cb9-408b-ab84-0664141d5c7f"",
            ""actions"": [
                {
                    ""name"": ""NextDialog"",
                    ""type"": ""Button"",
                    ""id"": ""e5397471-7f44-4936-82f3-16aec3e96c36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PreviousDialog"",
                    ""type"": ""Button"",
                    ""id"": ""771b767b-ef55-4b0e-86b5-7b715c493bf3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CloseDialog"",
                    ""type"": ""Button"",
                    ""id"": ""5e182209-ad5b-45b5-9ac5-9a26153794cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f569b2af-d195-4e59-b0da-bab69dc28972"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextDialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f85e79b-c67c-4f03-9339-ab5b570c64cd"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousDialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc21470e-1cee-45da-bced-01a6fc81f13d"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseDialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Dialog
        m_Dialog = asset.FindActionMap("Dialog", throwIfNotFound: true);
        m_Dialog_NextDialog = m_Dialog.FindAction("NextDialog", throwIfNotFound: true);
        m_Dialog_PreviousDialog = m_Dialog.FindAction("PreviousDialog", throwIfNotFound: true);
        m_Dialog_CloseDialog = m_Dialog.FindAction("CloseDialog", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Dialog
    private readonly InputActionMap m_Dialog;
    private IDialogActions m_DialogActionsCallbackInterface;
    private readonly InputAction m_Dialog_NextDialog;
    private readonly InputAction m_Dialog_PreviousDialog;
    private readonly InputAction m_Dialog_CloseDialog;
    public struct DialogActions
    {
        private @DialogInputActions m_Wrapper;
        public DialogActions(@DialogInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @NextDialog => m_Wrapper.m_Dialog_NextDialog;
        public InputAction @PreviousDialog => m_Wrapper.m_Dialog_PreviousDialog;
        public InputAction @CloseDialog => m_Wrapper.m_Dialog_CloseDialog;
        public InputActionMap Get() { return m_Wrapper.m_Dialog; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogActions set) { return set.Get(); }
        public void SetCallbacks(IDialogActions instance)
        {
            if (m_Wrapper.m_DialogActionsCallbackInterface != null)
            {
                @NextDialog.started -= m_Wrapper.m_DialogActionsCallbackInterface.OnNextDialog;
                @NextDialog.performed -= m_Wrapper.m_DialogActionsCallbackInterface.OnNextDialog;
                @NextDialog.canceled -= m_Wrapper.m_DialogActionsCallbackInterface.OnNextDialog;
                @PreviousDialog.started -= m_Wrapper.m_DialogActionsCallbackInterface.OnPreviousDialog;
                @PreviousDialog.performed -= m_Wrapper.m_DialogActionsCallbackInterface.OnPreviousDialog;
                @PreviousDialog.canceled -= m_Wrapper.m_DialogActionsCallbackInterface.OnPreviousDialog;
                @CloseDialog.started -= m_Wrapper.m_DialogActionsCallbackInterface.OnCloseDialog;
                @CloseDialog.performed -= m_Wrapper.m_DialogActionsCallbackInterface.OnCloseDialog;
                @CloseDialog.canceled -= m_Wrapper.m_DialogActionsCallbackInterface.OnCloseDialog;
            }
            m_Wrapper.m_DialogActionsCallbackInterface = instance;
            if (instance != null)
            {
                @NextDialog.started += instance.OnNextDialog;
                @NextDialog.performed += instance.OnNextDialog;
                @NextDialog.canceled += instance.OnNextDialog;
                @PreviousDialog.started += instance.OnPreviousDialog;
                @PreviousDialog.performed += instance.OnPreviousDialog;
                @PreviousDialog.canceled += instance.OnPreviousDialog;
                @CloseDialog.started += instance.OnCloseDialog;
                @CloseDialog.performed += instance.OnCloseDialog;
                @CloseDialog.canceled += instance.OnCloseDialog;
            }
        }
    }
    public DialogActions @Dialog => new DialogActions(this);
    public interface IDialogActions
    {
        void OnNextDialog(InputAction.CallbackContext context);
        void OnPreviousDialog(InputAction.CallbackContext context);
        void OnCloseDialog(InputAction.CallbackContext context);
    }
}
