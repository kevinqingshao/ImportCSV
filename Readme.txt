I used ASP.NET MVC core to develop this web application.
There are two page. Import CSV page for uploading CSV files. Show Account List for displaying accounts list.
 
I followed SOLID principles and used Strategy Design Pattern to create 
ImportCSV\Services\ITransform.cs and ImportCSV\Services\Transform.cs, then did implementation 
in contoller ImportCSV\Controllers\StandardAccountsController.cs

#1 Format.csv is #1 Format csv file.
#2 Format.csv is #2 Format csv file.
Standard Target File Format.csv is Standard Target File Format file.

On Import CSV page, display 3 csv file format details information.
User select csv file type, check 'with CSV header', then select csv file, select a CSV file, after click Upload,
application will upload csv file and save data in DB, redirect to show Standard Account List.
