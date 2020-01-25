// GENERATED AUTOMATICALLY FROM 'Assets/TESTING/Brian/PlayerTestArea/NewInput/TestController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TestController : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @TestController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TestController"",
    ""maps"": [
        {
            ""name"": ""Combat"",
            ""id"": ""5df179ea-83fb-4b37-a78b-8a8907fe37ee"",
            ""actions"": [
                {
                    ""name"": ""OrbInput"",
                    ""type"": ""Button"",
                    ""id"": ""042b8ae6-4767-4d95-a589-25100c130ad8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BeamInput"",
                    ""type"": ""Button"",
                    ""id"": ""7a8afde2-06c7-484d-a928-eb3e47e7d2b5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RadialInput"",
                    ""type"": ""Button"",
                    ""id"": ""396ec5fb-bad8-468a-9ad8-c525aaee8435"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6c4b4b10-5e30-4073-b639-1b3ff2306523"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": ""Hold(duration=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""OrbInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""258cd5b2-e14c-4b9e-98a3-1c718c7a1830"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""BeamInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc73fbca-4654-46e2-9de4-a097bea7bd1b"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""RadialInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_OrbInput = m_Combat.FindAction("OrbInput", throwIfNotFound: true);
        m_Combat_BeamInput = m_Combat.FindAction("BeamInput", throwIfNotFound: true);
        m_Combat_RadialInput = m_Combat.FindAction("RadialInput", throwIfNotFound: true);
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

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_OrbInput;
    private readonly InputAction m_Combat_BeamInput;
    private readonly InputAction m_Combat_RadialInput;
    public struct CombatActions
    {
        private @TestController m_Wrapper;
        public CombatActions(@TestController wrapper) { m_Wrapper = wrapper; }
        public InputAction @OrbInput => m_Wrapper.m_Combat_OrbInput;
        public InputAction @BeamInput => m_Wrapper.m_Combat_BeamInput;
        public InputAction @RadialInput => m_Wrapper.m_Combat_RadialInput;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @OrbInput.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnOrbInput;
                @OrbInput.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnOrbInput;
                @OrbInput.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnOrbInput;
                @BeamInput.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnBeamInput;
                @BeamInput.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnBeamInput;
                @BeamInput.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnBeamInput;
                @RadialInput.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnRadialInput;
                @RadialInput.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnRadialInput;
                @RadialInput.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnRadialInput;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OrbInput.started += instance.OnOrbInput;
                @OrbInput.performed += instance.OnOrbInput;
                @OrbInput.canceled += instance.OnOrbInput;
                @BeamInput.started += instance.OnBeamInput;
                @BeamInput.performed += instance.OnBeamInput;
                @BeamInput.canceled += instance.OnBeamInput;
                @RadialInput.started += instance.OnRadialInput;
                @RadialInput.performed += instance.OnRadialInput;
                @RadialInput.canceled += instance.OnRadialInput;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface ICombatActions
    {
        void OnOrbInput(InputAction.CallbackContext context);
        void OnBeamInput(InputAction.CallbackContext context);
        void OnRadialInput(InputAction.CallbackContext context);
    }
}
