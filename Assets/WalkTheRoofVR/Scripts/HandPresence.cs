using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
public class HandPresence : MonoBehaviour
{
    private InputDevice target_device;
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>(); 
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);        

        foreach (var item in devices)
        {
            Debug.Log(item.name + " | " + item.characteristics);
        }
      
      if(devices.Count > 0)
      {
          target_device = devices[0];
      }
       
    }

    // Update is called once per frame
    void Update()
    {
        target_device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if(primaryButtonValue)
        Debug.Log("Pressing Primary Button");

        target_device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if(triggerValue > 0.1f)
        Debug.Log("Trigger pressed : "+ triggerValue);

        target_device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        Debug.Log("Primary Touchpad " + primary2DAxisValue);
    }
}
