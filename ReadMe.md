<h1>Lab10-TimeTables</h1>
This is a CLI application that the user cant interact with. It will generate a timeTable of every student in the database and save it into a Excel spreadsheet.

The application connects to localhost MySQL database for all queries.

It initially queries for all students to make the tables for.
It will create a Excel sheet for each student and query the database for each subject they are enroled in and make a timetable with them.

It will then save the Excel sheets in a Excel file named "timetables3.xlsx".

Connection String: Server=localhost;Database=studenttimetable;Uid=student;Pwd=secret;
