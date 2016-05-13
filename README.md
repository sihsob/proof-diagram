# proof-diagram
Visualize Fitch styled proofs in the form of argument diagrams with proof validation.  Project created for Bram van Heuveln's Intermediate Logic course.

###What are Arugument Diagrams?
Traditionally used to represent arguments in natural language in a tree like structure.  Argument diagrams show the structure of arguments and allows one to easily find the important parts of the diagram.  These diagrams also help to show the structure and "lines of reasoning".  The method of creating these diagrams also helps to remove redundancies and unnecessary information.  Conveniently, argument diagrams transfer over fairly well for formal proofs.  Proofs can now be visually represented while retaining their step-by-step process and subproofs.

For more information: https://en.wikipedia.org/wiki/Argument_map or http://www.cogsci.rpi.edu/~heuveb/teaching/CriticalThinking/Web/Presentations/ArgumentDiagrams.pdf

### How to Use
1. Add Step:  Adds a node (which represents a step) to the diagram.  Requires you to enter a sentence and choose an inference rule from a drop-down menu.
2. Add Arrows: Will start and turn off "add arrow mode" where you can add an arrow between two nodes (either between two steps or between a step and a subproof container).  A green box will be next to the button while in add arrow mode.  Click on the following in this order:
  - The node that the arrow should start from
  - The node that the arrow should end at
  - Press the 'a' key on the keyboard
3. Verify Step: Verifies the step (will turn green if correct, red if incorrect).  Must select node to verify.  There also must exist an arrow between the node you want to verify and it's premises or "reason(s)" for it.  You will need to exit "add arrow mode" in order to verify a step.
4. Start Subproof: Makes a special container to use for subproofs.  Click to start "subproof mode".  A purpose box will be next to the button while in subproof mode.  Once a subproof container is created, then you add steps to it.  However, having nested subproofs WILL NOT work at the moment.
5. End Subproof: Click this button to make a subproof as complete.  Make sure to click on the subproof to end.
6. Restart: Clears the entire graph.  Unfortunately undoing one step at a time does not work at the moment.
7. Quit: Exits the application.
8. Scroll wheel will zoom in and out.

### Future Developement
The plan is to port the interface from unity over to a web front-end using PaperJS for the drawing and bootstrap for the overall interface.  Hopefully this will allow us to fully implement features we do not have right now, like nested subproofs, undoing, and dragging nodes around.  We also plan to improve the proof verfication process to make it more flexible and usable.  We also hope to add a save/import and load option.
