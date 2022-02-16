import PropTypes from "prop-types";
import React from "react";
import AddNote from "./AddNote";
import Note from "./Note";

const NotesList = ({ notes, handleAddNote, handleDeleteNote }) => {
  return (
    <>
      <div className="notes-list">
        {notes.map((note) => (
          <Note
            id={note.id}
            text={note.text}
            date={note.date}
            key={note.id}
            handleDeleteNote={handleDeleteNote}
          />
        ))}
        <AddNote handleAddNote={handleAddNote} />
      </div>
    </>
  );
};
NotesList.propTypes = {
  notes: PropTypes.func,
  handleAddNote: PropTypes.func.isRequired,
  handleDeleteNote: PropTypes.func.isRequired,
};
export default NotesList;
