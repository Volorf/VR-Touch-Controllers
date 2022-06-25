using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputEventsManager : MonoBehaviour
{
    [Header("Right Hand Controller Events")]
        
        [Header("Trigger")]
        public UnityEvent rightHandTriggerPressed;
        public UnityEvent rightHandTriggerReleased;
    
        [Header("Grip")]
        public UnityEvent rightHandGripPressed;
        public UnityEvent rightHandGripReleased;
    
        [Header("Grip")] 
        public UnityEvent rightHandSecondaryButtonPressed;
        public UnityEvent rightHandSecondaryButtonReleased;
    
        [Header("Left Hand Controller Events")]
        
        [Header("Trigger")]
        public UnityEvent leftHandTriggerPressed;
        public UnityEvent leftHandTriggerReleased;
        
        [Header("Grip")]
        public UnityEvent leftHandGripPressed;
        public UnityEvent leftHandGripReleased;
        
        private XRIDefaultInputActions _xrInputActions;
    
        // Right Hand Input Actions
        private InputAction _rightHandTrigger;
        private InputAction _rightHandGrip;
        private InputAction _rightHandSecondary;
        
        // Left Hand Input Actions
        private InputAction _leftHandTrigger;
        private InputAction _leftHandGrip;
    
        private void Awake()
        {
            _xrInputActions = new XRIDefaultInputActions();
            
            // Right Hand
            _rightHandTrigger = _xrInputActions.XRIRightHandInteraction.Activate;
            _rightHandGrip = _xrInputActions.XRIRightHandInteraction.Select;
            _rightHandSecondary = _xrInputActions.XRIRightHandInteraction.Secondary;
    
            // Left Hand
            _leftHandTrigger = _xrInputActions.XRILeftHandInteraction.Activate;
            _leftHandGrip = _xrInputActions.XRILeftHandInteraction.Select;
    
        }
    
        private void OnEnable()
        {
            // Right Hand
            _rightHandTrigger.Enable();
            _rightHandGrip.Enable();
    
            _rightHandTrigger.performed += RightTriggerPressed;
            _rightHandTrigger.canceled += RightTriggerReleased;
            
            _rightHandGrip.performed += RightGripPressed;
            _rightHandGrip.canceled += RightGripReleased;
    
            _rightHandSecondary.performed += RightSecondaryButtonPressed;
            _rightHandSecondary.canceled += RightSecondaryButtonReleased;
            
            // Left Hand
            _leftHandTrigger.Enable();
            _leftHandGrip.Enable();
    
            _leftHandTrigger.performed += LeftTriggerPressed;
            _leftHandTrigger.canceled += LeftTriggerReleased;
    
            _leftHandGrip.performed += LeftGripPressed;
            _leftHandGrip.canceled += LeftGripReleased;
        }
    
        private void OnDisable()
        {
            // Right Hand
            
            _rightHandTrigger.Disable();
            _rightHandGrip.Disable();
    
            _rightHandTrigger.performed -= RightTriggerPressed;
            _rightHandTrigger.canceled -= RightTriggerReleased;
            
            _rightHandGrip.performed -= RightGripPressed;
            _rightHandGrip.canceled -= RightGripReleased;
            
            _rightHandSecondary.performed -= RightSecondaryButtonPressed;
            _rightHandSecondary.canceled -= RightSecondaryButtonReleased;
            
            // Left Hand
            
            _leftHandTrigger.Disable();
            _leftHandGrip.Disable();
            
            _leftHandTrigger.performed -= LeftTriggerPressed;
            _leftHandTrigger.canceled -= LeftTriggerReleased;
    
            _leftHandGrip.performed -= LeftGripPressed;
            _leftHandGrip.canceled -= LeftGripReleased;
            
            
        }
        
        // Right Hand Events Invoking
        private void RightTriggerPressed(InputAction.CallbackContext callbackContext) => rightHandTriggerPressed.Invoke();
        private void RightTriggerReleased(InputAction.CallbackContext callbackContext) => rightHandTriggerReleased.Invoke();
        private void RightGripPressed(InputAction.CallbackContext callbackContext) => rightHandGripPressed.Invoke();
        private void RightGripReleased(InputAction.CallbackContext callbackContext) => rightHandGripReleased.Invoke();
    
        private void RightSecondaryButtonPressed(InputAction.CallbackContext callbackContext) =>
            rightHandSecondaryButtonPressed.Invoke();
    
        private void RightSecondaryButtonReleased(InputAction.CallbackContext callbackContext) =>
            rightHandSecondaryButtonReleased.Invoke();
        
        // Left Hand Events Invoking
        private void LeftTriggerPressed(InputAction.CallbackContext callbackContext) => leftHandTriggerPressed.Invoke();
        private void LeftTriggerReleased(InputAction.CallbackContext callbackContext) => leftHandTriggerReleased.Invoke();
        private void LeftGripPressed(InputAction.CallbackContext callbackContext) => leftHandGripPressed.Invoke();
        private void LeftGripReleased(InputAction.CallbackContext callbackContext) => leftHandGripReleased.Invoke();
}
