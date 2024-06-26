# About

The use case for this script is to input text into a textbox of a running process, and then press enter.
The provided script should be easy to modify for any specific use cases.

# AutomationIDScript

AutomationIDScript program requires 3 arguments: ProcessName AutomationID InputText

ProcessName: The name of the running process to search for

AutomationID: The AutomationID of the element in the process to search for

InputText: The text to be set in the TextBox found with the AutomationID


The 'Enter' key will be pressed after successfully setting the text

# TestApplication 

The TestApplication program is a simple program containing a TextBox with AutomationID '1000'
Pressing 'Enter' in this program will remove the set text from the TextBox
