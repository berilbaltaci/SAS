using System.ComponentModel.DataAnnotations.Schema;

namespace Comp4920_SAS.Models
{
[Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
    }
}