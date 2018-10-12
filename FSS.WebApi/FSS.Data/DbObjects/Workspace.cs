using FSS.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSS.Data.DbObjects
{
    public class Workspace : IEntityBase
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User {get;set;}

    }
}
