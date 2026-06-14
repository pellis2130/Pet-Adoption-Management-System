# Pet Adoption Management System

## Project Summary

The Pet Adoption Management System is a console-based C# application created to help an animal shelter manage pets that are available for adoption. The application allows users to add, view, search, update, and delete pet records through the terminal window. It also stores pet medical information and adoption status so shelter staff can keep organized records.

This project solves a realistic problem because animal shelters need a reliable way to manage pet information. Instead of tracking pets manually, this application stores records in a SQLite database and allows users to manage the information through a simple menu system.

## Features

* Add new pet records
* View all pets
* Search for a pet by ID
* Update pet information
* Delete pet records
* Store medical record details
* Track adoption status
* Demonstrate polymorphism
* Store and retrieve data using SQLite

## Technologies Used

* C#
* .NET
* SQLite
* Visual Studio Code
* GitHub

## How the Project Meets the Requirements

| Requirement                | How It Was Implemented                                                                    |
| -------------------------- | ----------------------------------------------------------------------------------------- |
| Realistic input and output | The application uses a terminal menu where users enter choices and pet information.       |
| In-code documentation      | Each class includes header documentation and comments explaining the purpose of the file. |
| Interface class            | `IAdoptable` defines adoption behaviors for pets.                                         |
| Abstract class             | `Pet` is an abstract base class for all pet types.                                        |
| Composition                | `Pet` contains a `MedicalRecord` object.                                                  |
| Polymorphism               | `Dog`, `Cat`, and `OtherPet` are handled through `Pet` and `IAdoptable` references.       |
| Constructors               | Classes include constructors to initialize object data.                                   |
| Access specifiers          | The project uses public, protected, and private members appropriately.                    |
| SQLite CRUD operations     | `PetRepository` creates, reads, updates, and deletes pet records in SQLite.               |

## Class Overview

* `Program`: Runs the main menu and controls user input/output.
* `Pet`: Abstract base class for all pets.
* `Dog`: Represents dog records.
* `Cat`: Represents cat records.
* `OtherPet`: Represents pets that are not dogs or cats.
* `IAdoptable`: Interface for adoption-related behavior.
* `MedicalRecord`: Stores medical information for each pet.
* `Adopter`: Stores adopter information.
* `Adoption`: Represents an adoption transaction.
* `PetRepository`: Handles SQLite database operations.
* `SQLiteDatabase`: Creates or connects to the database.

## What Went Well

The project successfully demonstrates the required object-oriented programming concepts and connects them to a realistic application. The console menu makes the program easy to use, and SQLite allows pet records to be saved and retrieved after the program closes.

## Challenges

The most challenging parts were adding SQLite database support, fixing input validation errors, and making sure the program did not crash when users entered unexpected values. These issues were corrected by improving input validation and testing the CRUD features.

## Final Reflection

This project helped me practice designing and building a complete C# application from proposal to final implementation. I learned how to connect object-oriented programming concepts with database storage and how to organize code into meaningful classes. The final application meets the course requirements and demonstrates a practical solution for managing pet adoption records.

## Screen Recording

YouTube Video Link: https://youtu.be/J_jsaq6peNQ 

## Repository Link

GitHub Repository: https://github.com/pellis2130/Pet-Adoption-Management-System.git 

