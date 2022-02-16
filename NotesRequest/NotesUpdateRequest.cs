using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.NotesRequest
{
    public class NotesUpdateRequest
    {
        public int Id { get; set; } 

        public string Notes { get; set; }

        public int EntityTypeId { get; set; }
    }
}
