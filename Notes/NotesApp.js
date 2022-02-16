import React, { useState } from "react";
import NotesList from "./NotesList";
import "./NotesIndex.css";
import { nanoid } from "nanoid";
import NotesSearch from "./NotesSearch";

const NotesApp = () => {
  const [notes, setNotes] = useState([
    {
      id: nanoid(),
      text: "You can delete this note.",
      date: "15/04/202",
    },
  ]);
  const [searchText, setSearchText] = useState("");
  const addNote = (text) => {
    const date = new Date();
    const newNote = {
      id: nanoid(),
      text: text,
      date: date.toLocaleDateString(),
    };
    const newNotes = [...notes, newNote];
    setNotes(newNotes);
  };
  const deleteNote = (id) => {
    const newNotes = notes.filter((note) => note.id !== id);
    setNotes(newNotes);
  };

  return (
    <>
      <div className="App">
        <div className="container">
          <NotesSearch handleSearchNote={setSearchText} />
          <NotesList
            notes={notes.filter((note) =>
              note.text.toLowerCase().includes(searchText)
            )}
            handleAddNote={addNote}
            handleDeleteNote={deleteNote}
          />
        </div>
      </div>
    </>
  );
};
export default NotesApp;
