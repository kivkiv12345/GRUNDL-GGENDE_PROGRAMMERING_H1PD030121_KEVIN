
# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.
import os
from ast import literal_eval
import json
import sys

if __name__ == '__main__':
    go_again = 'y'

    while go_again == 'y':  # Allow the user to loop the program.
        os.system('clear')  # Clear the console (when possible).
        usrinput = None  # Default value for the user input.

        if input("Use predefined variable? Y/n:\n").lower() == 'n':
            usrinput = input("\nPlease provide an input to show memory usage of: \n")

        if isinstance(usrinput, str):  # User input will always be a string, so non-strings are always predefined.
            if usrinput.lower() == 'none':  # None does not feature a constructor, and must therefore be handled separately.
                if input("Would you like to convert your input to NoneType? Y/n:\n").lower() == 'y': usrinput = None
            else:  # Otherwise; we test the input against the constructors, featured in the dictionary below.
                for msg, convert in {"to a float": float, "to an int": int, "using JSON": json.loads, "using literal eval": literal_eval}.items():
                    try:
                        convert(usrinput)  # Start by testing for exceptions by trying to use the current constructor.
                        if input(f"Your variable is currently a string.\nWould you like for it to be converted {msg}? Y/n:\n").lower() == 'y':
                            usrinput = convert(usrinput)  # The user has decided to use an applicable constructor.
                            break
                    except (ValueError, TypeError, SyntaxError): continue

        print()  # Create some space before we show the output.

        varsize = sys.getsizeof(usrinput)

        print(f"Your input is of type:                  {type(usrinput)}")
        print(f"Your input is located at:               {hex(id(usrinput))}")
        print(f"The size is of input is:                {varsize}")
        print(f"The maximum size of your variable is:   {2**varsize}")

        print()
        if input("Show visual representation of the variable size? Y/n:\n").lower() == 'y':  # The diagram can become quite large at times.
            print()
            seperator = "+--------------+"
            middle = "-     BYTE     -"

            print(seperator)
            for _ in range(varsize):
                print(middle)
                print(seperator)

        go_again = input("\nRepeat program? Y/n:\n").lower()
