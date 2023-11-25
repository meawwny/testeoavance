using System.ComponentModel.DataAnnotations;

namespace avanceproyidk.DataAccess.DBEntities.Abstraccions
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool Status { get; set; }
    }
}
