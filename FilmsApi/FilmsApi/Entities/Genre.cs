using FilmsApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace FilmsApi.Entities
{
    public class Genre : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(maximumLength: 10)]
        [FirstCapitalLetter]
        public string Name { get; set; }

        [Range(18, 120)]
        public int Age { get; set; }

        [CreditCard]
        public string CreditCard { get; set; }

        [Url]
        public string URL { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var firstLetter = Name[0].ToString();

                if (firstLetter != firstLetter.ToUpper())
                {
                    yield return new ValidationResult("The first letter must be uppercase",
                        new string[] { nameof(Name) });
                }
            }
        }
    }
}
