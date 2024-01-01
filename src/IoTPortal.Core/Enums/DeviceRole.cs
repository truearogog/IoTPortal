using System.ComponentModel.DataAnnotations;

namespace IoTPortal.Core.Enums
{
    public enum DeviceRole
    {
        // can change user roles
        [Display(Name = "Owner", Description = "Can change user roles and manage device")]
        Owner,
        // can change device properties
        [Display(Name = "Power User", Description = "Can manage device")]
        PowerUser,
        // can see device info and measurements
        [Display(Name = "User", Description = "Can see device info and measurements")]
        User
    }
}
