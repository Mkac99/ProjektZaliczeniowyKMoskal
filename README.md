# FIBARO CUSTOMERS-app
This repository contains the source code for the web application for managing customer list (CRUD actions). It contains of several projects and 2 separate databases (one for customers, and one for logging the events).

## Requirements
* Visual Studio 2022 with .NET6
* SQL Server 2016 or newer 

## Setup
* Clone the repository
    * ${MainDir} = local path where you put the repository
* Locate the database creation script files:
    * ${MainDir}/Customers.MainApp/db_scripts/Customers_create.sql
    * ${MainDir}/Customers.Tracking/db_scripts/Tracking_create.sql
* In both files replace the value of the @directory variable to the path, where the database file should be located, after it is created
* Run both scripts separately (preferably using SQL Server Management Studio)

## Launching
* Run the Visual Studio
* Go to Solution's properties window
* In the **Startup Project** section choose **Multiple startup projects:**, then below select value **Start** in the **Action** column for projects: **Customers.MainApp** and **Customers.Tracking**
* Close the window and run the application
    * Customers.MainApp should be running locally
    * Customers.Tracking should be running separately on IIS Express