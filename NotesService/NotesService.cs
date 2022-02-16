using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Requests.NotesRequest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.NotesService
{
    public class NotesService : INotesService
    {
        IDataProvider _dataProvider = null;

        public NotesService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Update(NotesUpdateRequest model)
        {
            string procName = "[dbo].[Notes_Update]";

            _dataProvider.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                UpdateNoteMapper(model, col);

            },
           returnParameters: null);
        }

        private static void UpdateNoteMapper(NotesUpdateRequest model, SqlParameterCollection col)
        {
            UpdateNoteMapper(model, col);
        }

        public int AddNotes(NotesAddRequests model)
        {
            int id = 0;
            string procName = "[dbo].[Notes_Insert]";

            _dataProvider.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                NoteMapper(model, col);

                SqlParameter idOutput = new SqlParameter("@Id", SqlDbType.Int);
                idOutput.Direction = ParameterDirection.Output;
                col.Add(idOutput);
            },
            returnParameters: delegate (SqlParameterCollection returnCollect)
            {
                object ojectId = returnCollect["@Id"].Value;
                int.TryParse(ojectId.ToString(), out id);
            });
            return id;
        }

        private static void NoteMapper(NotesAddRequests model, SqlParameterCollection col)
        {
            NoteMapper(model, col);
        }

        public Notes Get(int id)
        {
            string procName = "[dbo].[Notes_SelectById]";
            Notes notes = null;

            _dataProvider.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {

                paramCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                int startIndex = 0;
                notes = MapNotes(reader, ref startIndex);

            });

            return notes;
        }

        public Paged<Notes> GetNotesCreatedBy(int CreatedBy, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Notes_Select_CreatedBy]";
            int totalCount = 0;
            Paged<Notes> pagedResults = null;
            List<Notes> notes = null;
            _dataProvider.ExecuteCmd(procName, delegate (SqlParameterCollection collect)
            {
                collect.AddWithValue("@CreatedBy", CreatedBy);
                collect.AddWithValue("@pageIndex", pageIndex);
                collect.AddWithValue("@pageSize", pageSize);
            },
            delegate (IDataReader reader, short set)
            {
                int startIndex = 0;
                var note = MapNotes(reader, ref startIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startIndex++);
                }

                if (notes == null)
                {
                    notes = new List<Notes>();
                }
                notes.Add(note);
            });
            if (notes != null)
            {
                pagedResults = new Paged<Notes>(notes, pageIndex, pageSize, totalCount);
            }

            return pagedResults;
        }

        private static Notes MapNotes(IDataReader reader, ref int startIndex)
        {
            Notes ANotes = new Notes();
            int startingIndex = 0;

            ANotes.Id = reader.GetSafeInt32(startingIndex++);
            ANotes.EntityId = reader.GetSafeInt32(startingIndex++);
            ANotes.EntityTypeId = reader.GetSafeInt32(startingIndex++);
            ANotes.TagId = reader.GetSafeInt32(startingIndex++);
           
            return ANotes;
        }

    }
}
