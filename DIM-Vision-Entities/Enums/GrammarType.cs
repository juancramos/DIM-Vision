using System.ComponentModel.DataAnnotations;

namespace DIM_Vision_Entities.Enums
{
    public enum GrammarType
    {
        [Display(Name = "No action in grammar")]
        None = 0,
        [Display(Name = "")]
        Alternative = 1
    }
}
