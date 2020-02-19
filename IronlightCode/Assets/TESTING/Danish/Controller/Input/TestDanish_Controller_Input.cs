// GENERATED AUTOMATICALLY FROM 'Assets/TESTING/Danish/Controller/Input/TestDanish_Controller_Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TestDanish_Controller_Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TestDanish_Controller_Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TestDanish_Controller_Input"",
    ""maps"": [
        {
            ""name"": ""Traversal"",
            ""id"": ""5432049c-a8c6-464e-a6c7-9c61e25b44a0"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c138788f-f399-43ef-9fe5-fcc27529e108"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8230ee4b-40b2-4bf8-a4c0-3a9f68602d60"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1)""
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""bf93ff86-3acd-4247-b4ba-6a0d675081e2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""773ca573-5e69-44d7-931e-7351e8edf2c5"",
                    ""path"": ""2DVector(normalize=false)"",
                    ""interactions"": ""Hold(duration=0.01),Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4ccc9810-6d59-4c2d-bc1d-5866d9e9e4eb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""92c563b4-e5c2-42b2-b631-8f67b041352f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0ebc77cf-56c1-41c7-b3e6-e2ef79e638f2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""029ba5e1-8eb6-4b2f-957a-78da1b2069e1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""53f9f5c7-c54e-4b1d-8214-614e41495ddd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddcfa448-6cc9-4891-a04f-43fcc1a5929e"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""4533a6a6-403a-4bd5-b3a5-65f5f3daedff"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""951ef509-b195-4bd1-adbc-7ad5851968a8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f899aac0-b295-444e-a677-ac622bb7faac"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold(duration=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Traversal
        m_Traversal = asset.FindActionMap("Traversal", throwIfNotFound: true);
        m_Traversal_Movement = m_Traversal.FindAction("Movement", throwIfNotFound: true);
        m_Traversal_Jump = m_Traversal.FindAction("Jump", throwIfNotFound: true);
        m_Traversal_Dash = m_Traversal.FindAction("Dash", throwIfNotFound: true);
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_Attack = m_Combat.FindAction("Attack", throwIfNotFound: true);
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

    // Traversal
    private readonly InputActionMap m_Traversal;
    private ITraversalActions m_TraversalActionsCallbackInterface;
    private readonly InputAction m_Traversal_Movement;
    private readonly InputAction m_Traversal_Jump;
    private readonly InputAction m_Traversal_Dash;
    public struct TraversalActions
    {
        private @TestDanish_Controller_Input m_Wrapper;
        public TraversalActions(@TestDanish_Controller_Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Traversal_Movement;
        public InputAction @Jump => m_Wrapper.m_Traversal_Jump;
        public InputAction @Dash => m_Wrapper.m_Traversal_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Traversal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TraversalActions set) { return set.Get(); }
        public void SetCallbacks(ITraversalActions instance)
        {
            if (m_Wrapper.m_TraversalActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_TraversalActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_TraversalActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_TraversalActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_TraversalActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_TraversalActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_TraversalActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_TraversalActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_TraversalActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_TraversalActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_TraversalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public TraversalActions @Traversal => new TraversalActions(this);

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_Attack;
    public struct CombatActions
    {
        private @TestDanish_Controller_Input m_Wrapper;
        public CombatActions(@TestDanish_Controller_Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_Combat_Attack;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);
    public interface ITraversalActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface ICombatActions
    {
        void OnAttack(InputAction.CallbackContext context);
    }
}
