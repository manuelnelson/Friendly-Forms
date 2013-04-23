using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace BusinessLogic.Models
{
    public class AccountUser
    {
        /// <summary>
        /// Gets or sets the identifier for the user.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the role ID for the user
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the user's display name.
        /// </summary>
        [StringLength(30, ErrorMessage = "Name must be less than 30 characters")]        
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        ///// <summary>
        ///// Gets or sets the authorization identifier for the user.
        ///// </summary>
        //[Display(Name = "Authorization Id")]
        //public string AuthorizationId { get; set; }

        /// <summary>
        /// Gets or sets the email for the user.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Email]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the email for the user.
        /// </summary>
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
