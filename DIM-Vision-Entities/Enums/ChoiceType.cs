using System.ComponentModel.DataAnnotations;

namespace DIM_Vision_Entities.Enums
{
    public enum ChoiceType
    {
        [Display(Name = "No action in choice")]
        None = 0,
        [Display(Name = "SemanticResultValue")]
        SemanticResultValue = 1
    }
}
