using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.NotesRequest
{
    public class Notes
    {
        public int Id { get; set; }

        public int EntityId { get; set; }

        public int EntityTypeId  { get; set; }

        public int TagId { get; set; }
       
    }
}
