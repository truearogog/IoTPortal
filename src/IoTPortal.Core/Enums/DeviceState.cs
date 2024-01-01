using System.ComponentModel.DataAnnotations;

namespace IoTPortal.Core.Enums
{
    public enum DeviceState
    {
        // owner can change measurement types
        [Display(Name = "Setup", Description = "Device properties can be changed")]
        Setup = 0,
        // device can be used
        [Display(Name = "Ready", Description = "Device is ready for use")]
        Ready = 1,
    }
}
