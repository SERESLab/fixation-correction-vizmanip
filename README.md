# Eye Fixation Visualizer

Eye tracking researchers routinely need to correct drift in eye fixations manually.This tool “Eye Fixation Visualizer” helps with this activity by allowing the user to visualize fixations and any changes that they make to the eye fixation data. 

# Features
* Visualize fixations
* Help researchers visually correct eye fixations
* Perform a three-way comparison between the original fixations, manually corrected fixations, and automatically corrected fixations.  

# Data format used
The files used to generate the visual mapping of the fixation points is a comma delimited text file. The file must have the columns 'x' and 'y' for the original x and y data and 'x-man-cor' and 'y-man-cor' for the x and y corrected fixation data and optionally 'x-auto-cor' and 'y-auto-cor' for optional automatically corrected data points. The only requirement for the ordering of points is that they are in sequential order since time data is not taken into consideration.