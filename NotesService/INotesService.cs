using Sabio.Models;
using Sabio.Models.Requests.NotesRequest;

namespace Sabio.Services.NotesService
{
    public interface INotesService
    {
        int AddNotes(NotesAddRequests model);

        void Update(NotesUpdateRequest model);

        public Notes Get(int id);

        Paged<Notes> GetNotesCreatedBy(int CreatedBy, int pageIndex, int pageSize);
    }
}