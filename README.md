# _Word Count_

#### _3/01/2019_

#### By _**Olha Wysocky**_

## Description
_This is a software to keep track of employees and their clients._


## Known Bugs

_Clients are not removed when stylist was deleted._

## Installation Requirements
* _Download and install .NET Core 1.1.4 SDK._
* _Download and install .NET Core Runtime 1.1.2_
* _Download and install Mono_
* _Download and install MAMP_

## Setup instructions
* _On GitHub, navigate to the main page of the repository._
* _On the right find the green button "Clone or download", click it._
* _To clone the repository in Desktop choose "Open in Desktop" or download the ZIP file._
* _For more information, see "Cloning a repository from GitHub to GitHub Desktop."_
* _Change into the work directory: $ cd WordCounter.Solution._
* _To run the program, first navigate to the WordCounter folder then compile and execute: $ dotnet restore, $ dotnet build, dotnet run._
* _In the browser go to the link http://localhost:5000/, enjoy._
* _To run the tests, novigate the folder WordCounter.Tests and use these commands: $ dotnet restore $ dotnet test._
* _Start MAMAP and enter the fallowing to your terminal:_
* _/Applications/MAMP/Library/bin/mysql --host=localhost -uroot -proot_
* _> CREATE DATABASE to_do;_
* _> USE to_do;_
* _> CREATE TABLE categories (id serial PRIMARY KEY, name VARCHAR(255));_
* _> CREATE TABLE tasks (id serial PRIMARY KEY, description VARCHAR(255));_


## Technologies Used

* _C#_

### License
MIT
**

Copyright (c) 2019 **_Olha Wysocky_**
