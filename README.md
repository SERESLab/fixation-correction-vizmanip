# Eye Fixation Visualizer and Manual Fixation Corrections

Eye tracking researchers routinely need to correct drift in eye fixations manually.This tool “Eye Fixation Visualizer” helps with this activity by allowing the user to visualize fixations then make changes using a simple drag and drop functionality to correct fixations manually. 

# Features
* Visualize fixations
* Help researchers visually correct eye fixations
* Perform a three-way comparison between the original fixations, manually corrected fixations, and automatically corrected fixations.  
* Simple drag and drop corrections to allow fast and easy fixation changes

# Data format used
The files used to generate the visual mapping of the fixation points is a comma delimited text file. The file must have the columns 'x' and 'y' for the original x and y data, this data will cloned into a new table called manual corrections that can be manipulated via mouse. Output save file will also be in CSV format consisting of all columns in original and only x, y columns edited. 
