using System.ComponentModel.DataAnnotations;

namespace LPManagement.UI.Models
{
    /// <summary>
    /// Account model class.
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Gets or sets username.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}