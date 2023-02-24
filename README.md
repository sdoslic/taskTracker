# Task tracker
Windows application used for tracking tasks.

"Task tracker" application enables managing tasks and people, assigning tasks to person, previewing person's tasks and previewing task board.
When the application is started main window is shown with three tabs: Tasks, People and Board and few buttons placed at the bottom. Tasks tab contains list of tasks, People tab contains list of persons and Board tab presents task grouped by their status (todo, ongoing, done). You can also preview tasks that are assigned to the person by double clicking at the person listed in the People tab.

In order to use the application first create a person and then proceed with creating the tasks. You can not create the task without assigning person.

In the left bottom corner there is a field to type text for filtering tasks or persons (depending on which tab is currently active). Just type the text and press the Filter buttom, or press the Enter key. Tasks are filtered by name and status and persons are filtered by name.

Buttons Add, Update and Delete are used for adding new, updating selected and deleting selected task or person (depending on which tab is currently active). Clicking on Add and Update new window is opened to fill the data for task or person.

# Build and run
## Download or clone the project
You can download the project by clicking on green "<>Code" button on the page and choosing "Download ZIP" from the drop down menu. Also you ca clone the project using link given on clicking on the "<>Code" button and choosing HTTPS. Just copy the link and run "git clone <link>" from the GitBash or other git console.
## Open project in Visual Studio
Double click on the TaskTracker.sln solution file to open solution in Visual studio or open the project from the menu File -> Open -> Project/Solution... and choose project's solution file. Project is created in Visual studio 2019. Check if your version is compatible with the version of project.
## Build
Go to the menu item Build -> Build Solution or press F6.
##Running the application
After project is successfully compiled, go to the menu Debug -> Start debugging or press F5. The application will run.
