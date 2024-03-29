﻿using UnityEngine;

public class VRController : MonoBehaviour
{
    
    
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRendererRightController;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRendererLeftController;

    private SkinnedMeshRenderer _currentSkinnedMeshRenderer;
    
    private enum Hand
    {
        Left,
        Right
    }

    [SerializeField] private Hand currentHand = Hand.Right;
    
    [SerializeField] private bool testMode = false;
    private const float BLEND_SHAPE_MULTIPLIER = 100f;
    
    private const int SECONDARY_BUTTON_INDEX = 0;
    [Range(0f, 100f)] [SerializeField] float secondaryButtonWeight = 0f;

    private const int PRIMARY_BUTTON_INDEX = 1;
    [Range(0f, 100f)] [SerializeField] float primaryButtonWeight = 0f;
    
    private const int TRIGGER_BUTTON_INDEX = 2;
    [Range(0f, 100f)] [SerializeField] float triggerButtonWeight = 0f;
    
    private const int GRIP_BUTTON_INDEX = 3;
    [Range(0f, 100f)] [SerializeField] float gripButtonWeight = 0f;
    
    [SerializeField] private GameObject joystick;
    private const float X_OFFSET = 20f;
    private const float Y_OFFSET = 20f;
    
    public Vector2 vec2 = new Vector2(0f, 0f);

    private void Awake()
    {
        switch (currentHand)
        {
            case Hand.Right:
                _currentSkinnedMeshRenderer = skinnedMeshRendererRightController;
                skinnedMeshRendererLeftController.gameObject.SetActive(false);
                break;
            case Hand.Left:
                _currentSkinnedMeshRenderer = skinnedMeshRendererLeftController;
                joystick.transform.position += new Vector3(-joystick.transform.position.x * 2.0f, 0f, 0f);
                skinnedMeshRendererRightController.gameObject.SetActive(false);
                break;
            default:
                Debug.Log("No hands");
                break;
        }
    }

    private void Update()
    {
        if (!testMode) return;
        _currentSkinnedMeshRenderer.SetBlendShapeWeight(SECONDARY_BUTTON_INDEX, secondaryButtonWeight);
        _currentSkinnedMeshRenderer.SetBlendShapeWeight(PRIMARY_BUTTON_INDEX, primaryButtonWeight);
        _currentSkinnedMeshRenderer.SetBlendShapeWeight(TRIGGER_BUTTON_INDEX, triggerButtonWeight);
        _currentSkinnedMeshRenderer.SetBlendShapeWeight(GRIP_BUTTON_INDEX, gripButtonWeight);
        SetJoystickState(vec2);
        Debug.LogWarning("The VR Controller in Test Mode. All device's events will be overwritten by values in the editor.");
    }

    public void SetJoystickState(Vector2 vector2)
    {
        float zRot = vector2.x.Remap(-1f, 1f, X_OFFSET, -X_OFFSET);
        float xRot = vector2.y.Remap(-1f, 1f, -Y_OFFSET, Y_OFFSET);
        joystick.transform.localEulerAngles = (new Vector3(xRot, 0f, zRot));
    }
    
    // public void SetSecondaryButtonState(bool value)
    // {
    //     float processedValue = value ? BLEND_SHAPE_MULTIPLIER : 0f;
    //     ProcessButtonState(SECONDARY_BUTTON_INDEX, processedValue);
    // }
    

    // public void SetPrimaryButtonState(bool value)
    // {
    //     float processedValue = value ? BLEND_SHAPE_MULTIPLIER : 0f;
    //     ProcessButtonState(PRIMARY_BUTTON_INDEX, processedValue);
    // }

    // public void SetTriggerButtonState(float value) => ProcessButtonState(TRIGGER_BUTTON_INDEX, value);
    // public void SetGripButtonState(float value) => ProcessButtonState(GRIP_BUTTON_INDEX, value);

    // Instant reactions
    public void SetTriggerButtonPressed() => ProcessButtonState(TRIGGER_BUTTON_INDEX, BLEND_SHAPE_MULTIPLIER);
    public void SetTriggerButtonReleased() => ProcessButtonState(TRIGGER_BUTTON_INDEX, 0f);
    
    public void SetGripButtonPressed() => ProcessButtonState(GRIP_BUTTON_INDEX, BLEND_SHAPE_MULTIPLIER);
    public void SetGripButtonReleased() => ProcessButtonState(GRIP_BUTTON_INDEX, 0f);
    
    public void SetPrimaryButtonPressed() => ProcessButtonState(PRIMARY_BUTTON_INDEX, BLEND_SHAPE_MULTIPLIER);
    public void SetPrimaryButtonReleased() => ProcessButtonState(PRIMARY_BUTTON_INDEX, 0f);
    
    public void SetSecondaryButtonPressed() => ProcessButtonState(SECONDARY_BUTTON_INDEX, BLEND_SHAPE_MULTIPLIER);
    public void SetSecondaryButtonReleased() => ProcessButtonState(SECONDARY_BUTTON_INDEX, 0f);
    
    private void ProcessButtonState(int index, float value)
    {
        float clampedValue = Mathf.Clamp(value * BLEND_SHAPE_MULTIPLIER, 0f, BLEND_SHAPE_MULTIPLIER);
        _currentSkinnedMeshRenderer.SetBlendShapeWeight(index, clampedValue);
    }
}
