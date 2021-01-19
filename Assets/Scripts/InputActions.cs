// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Ship Controls"",
            ""id"": ""2f675008-a1c3-4cef-8587-e165b86146bd"",
            ""actions"": [
                {
                    ""name"": ""Roll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7e16ec73-c461-4b5c-97aa-260b1f1f8639"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveUpDown"",
                    ""type"": ""PassThrough"",
                    ""id"": ""53561538-7664-4f08-b721-c6fe1713c8fb"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveForwardBack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0553f601-5247-4931-8f66-8a5cacfbf2a8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveLeftRight"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7f68233a-6084-48ea-9b78-9b054a32676d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateLeftRight"",
                    ""type"": ""PassThrough"",
                    ""id"": ""60703dad-96d3-4300-8edf-d9c29bd219f7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateUpDown"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9b3ba640-1293-4820-b18d-97b4736a1552"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""9cc1ec88-44df-45bf-9cb6-2f60386587e9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""15dc491c-a710-4146-a383-21fbe8f9d0ae"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2de90ff4-2eac-4494-af85-7c35ab671571"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ef5f20df-d0bc-45cd-98c2-23aabba2c000"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""2850c0b1-e11d-4d16-b105-3224f62de403"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUpDown"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9d5586da-fcec-4b84-8c8c-128a994ece81"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e7d21c5a-caf7-4373-9444-336319cbbf2a"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f79d4350-87b5-4b3e-a421-0d9a6c1f0f6d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForwardBack"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ebb21130-90a3-4304-a464-737696000ae6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForwardBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""27ddb4f4-fa50-4770-9f73-cfb4326b3fa2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForwardBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""147500b2-2069-4101-a306-98183b640b8e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""434a9918-34b2-4186-a2d4-aec3ccda1597"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeftRight"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""296fa1a6-564b-4be5-a34e-0c9459410155"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9dc93521-35d9-452c-b27a-7bdb086765db"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b7fc1a6e-fcf5-4db5-b585-7a36187ed3a0"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateLeftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b40d074-ca1b-4d70-a50f-54c37ce8e925"",
                    ""path"": ""<Joystick>/stick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateLeftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3b3e148-934a-4b5c-b1b2-ffd863d145d3"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateUpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a51bbd13-4dbc-409a-974c-5d4e40108667"",
                    ""path"": ""<Joystick>/stick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateUpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Ship Controls
        m_ShipControls = asset.FindActionMap("Ship Controls", throwIfNotFound: true);
        m_ShipControls_Roll = m_ShipControls.FindAction("Roll", throwIfNotFound: true);
        m_ShipControls_MoveUpDown = m_ShipControls.FindAction("MoveUpDown", throwIfNotFound: true);
        m_ShipControls_MoveForwardBack = m_ShipControls.FindAction("MoveForwardBack", throwIfNotFound: true);
        m_ShipControls_MoveLeftRight = m_ShipControls.FindAction("MoveLeftRight", throwIfNotFound: true);
        m_ShipControls_RotateLeftRight = m_ShipControls.FindAction("RotateLeftRight", throwIfNotFound: true);
        m_ShipControls_RotateUpDown = m_ShipControls.FindAction("RotateUpDown", throwIfNotFound: true);
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

    // Ship Controls
    private readonly InputActionMap m_ShipControls;
    private IShipControlsActions m_ShipControlsActionsCallbackInterface;
    private readonly InputAction m_ShipControls_Roll;
    private readonly InputAction m_ShipControls_MoveUpDown;
    private readonly InputAction m_ShipControls_MoveForwardBack;
    private readonly InputAction m_ShipControls_MoveLeftRight;
    private readonly InputAction m_ShipControls_RotateLeftRight;
    private readonly InputAction m_ShipControls_RotateUpDown;
    public struct ShipControlsActions
    {
        private @InputActions m_Wrapper;
        public ShipControlsActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Roll => m_Wrapper.m_ShipControls_Roll;
        public InputAction @MoveUpDown => m_Wrapper.m_ShipControls_MoveUpDown;
        public InputAction @MoveForwardBack => m_Wrapper.m_ShipControls_MoveForwardBack;
        public InputAction @MoveLeftRight => m_Wrapper.m_ShipControls_MoveLeftRight;
        public InputAction @RotateLeftRight => m_Wrapper.m_ShipControls_RotateLeftRight;
        public InputAction @RotateUpDown => m_Wrapper.m_ShipControls_RotateUpDown;
        public InputActionMap Get() { return m_Wrapper.m_ShipControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipControlsActions set) { return set.Get(); }
        public void SetCallbacks(IShipControlsActions instance)
        {
            if (m_Wrapper.m_ShipControlsActionsCallbackInterface != null)
            {
                @Roll.started -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnRoll;
                @MoveUpDown.started -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnMoveUpDown;
                @MoveUpDown.performed -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnMoveUpDown;
                @MoveUpDown.canceled -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnMoveUpDown;
                @MoveForwardBack.started -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnMoveForwardBack;
                @MoveForwardBack.performed -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnMoveForwardBack;
                @MoveForwardBack.canceled -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnMoveForwardBack;
                @MoveLeftRight.started -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnMoveLeftRight;
                @MoveLeftRight.performed -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnMoveLeftRight;
                @MoveLeftRight.canceled -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnMoveLeftRight;
                @RotateLeftRight.started -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnRotateLeftRight;
                @RotateLeftRight.performed -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnRotateLeftRight;
                @RotateLeftRight.canceled -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnRotateLeftRight;
                @RotateUpDown.started -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnRotateUpDown;
                @RotateUpDown.performed -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnRotateUpDown;
                @RotateUpDown.canceled -= m_Wrapper.m_ShipControlsActionsCallbackInterface.OnRotateUpDown;
            }
            m_Wrapper.m_ShipControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @MoveUpDown.started += instance.OnMoveUpDown;
                @MoveUpDown.performed += instance.OnMoveUpDown;
                @MoveUpDown.canceled += instance.OnMoveUpDown;
                @MoveForwardBack.started += instance.OnMoveForwardBack;
                @MoveForwardBack.performed += instance.OnMoveForwardBack;
                @MoveForwardBack.canceled += instance.OnMoveForwardBack;
                @MoveLeftRight.started += instance.OnMoveLeftRight;
                @MoveLeftRight.performed += instance.OnMoveLeftRight;
                @MoveLeftRight.canceled += instance.OnMoveLeftRight;
                @RotateLeftRight.started += instance.OnRotateLeftRight;
                @RotateLeftRight.performed += instance.OnRotateLeftRight;
                @RotateLeftRight.canceled += instance.OnRotateLeftRight;
                @RotateUpDown.started += instance.OnRotateUpDown;
                @RotateUpDown.performed += instance.OnRotateUpDown;
                @RotateUpDown.canceled += instance.OnRotateUpDown;
            }
        }
    }
    public ShipControlsActions @ShipControls => new ShipControlsActions(this);
    public interface IShipControlsActions
    {
        void OnRoll(InputAction.CallbackContext context);
        void OnMoveUpDown(InputAction.CallbackContext context);
        void OnMoveForwardBack(InputAction.CallbackContext context);
        void OnMoveLeftRight(InputAction.CallbackContext context);
        void OnRotateLeftRight(InputAction.CallbackContext context);
        void OnRotateUpDown(InputAction.CallbackContext context);
    }
}
