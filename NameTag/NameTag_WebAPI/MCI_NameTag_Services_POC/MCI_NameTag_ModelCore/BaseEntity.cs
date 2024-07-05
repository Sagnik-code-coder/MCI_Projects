using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_NameTag_ModelCore
{
    public class BaseEntity
    {
        [NotMapped]
        [MaxLength(50), Column("CreatedBy")]
        public string? CreatedBy { get; set; }

        [NotMapped]
        [Column("CreatedOn")]
        public DateTime? CreatedOn { get; set; }

        [NotMapped]
        [MaxLength(50), Column("ModifiedBy")]
        public string? ModifiedBy { get; set; }

        [NotMapped]
        [Column("ModifiedOn")]
        public DateTime? ModifiedOn { get; set; }
    }
}
