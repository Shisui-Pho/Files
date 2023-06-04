# Files
Class library that lets you copy ,move and delete files, get list of directories. files in a directory and also serialize and deserialize files in xml and json format


USING THE LIBRARY  
---------------------
Add a reference file 'FileControl.dll' to your project.  
Add a using namespace : Control.Files
using a netstandard2.0

FILES CLASS
----
--The Files class lets you get the file names of every directory  
--You can access it by using the class static methods : Files.""  
--All methods will return a list of string arrays for the files  
--All methods takes an array of file directories  
--You can pass a single directory by implicitly making it a string array :(new string[]{"directory name"}  
  
    
How it works  (Methods)  
  
    
GetFileNamesWithDirectories : Returns the full Path of all the files in the directory   


GetFileNamesWithoutDirectories : Returns the file names of all the files in a directory   
  
    

  
FileControl Class
---
--Do simple file manipulation controls includind: Copying, moving and deleting files  
--Has methods where return types are void, but a boolean values will be passed through the out parameter  
--Each function has an overloaded version which takes a string array of files   
--All files must have the full path, unless they are in the root folder of the .exe file   
--Methods will take has two common parameters : files and a boolean which must be returned to see if operation was a success or not   

How it works  (Methods)  
DeleteFile  : Has two overloaded version, one takes a single file and the other take multiple files  

MoveFile    : Has two overloaded version, one takes a single file and the other take multiple files
            : In both versions you must pass the destination folder (Full path)

CopyFile    : Has two overloaded version, one takes a single file and the other take multiple files  
            : In both versions you must pass the destination folder (Full path)  
            : If you are copying in the same root folder, then an extension will be added to the file name  
            
            
            
            
FileDirectoryInfo Class
--
--Get the file or directory infomation  
--Methods returns a string  
  
    
How it works  (Methods)  
  
FileInfo      : Takes the file name(with full path) and returns a string  
  
  
DirectoryInfo : Takes the directory name(with full path) and returns a string
  
  
  
  
JSerializer Class  and XSerializer Class  
--
--Serialize and deserialize files in a JSON Format(JSerializer) or in Xml format(XSerializer)
--Methods return booleans to show if the process was a success or not   
  
  
  
How it works (Methods)  
SerializeFile<T>   : Takes three paremeters(The object you want to serialize, the name of the file and an optional boolean paremeter to append on an existing file)

DeserializeFile<T> : Takes two parameters (A reference object and a file name)  
  
    
    
    
    
    
    
    
What i learned
--------------
1. I learned how to work with files. writing and reading from text files(Wasn't able to add that feature to this library due to different ways in which the writting can be implemented)
2. Learned how serialization works in both the JSON and Xml format
3. Learned how to control user erros by making use of booleans
4. How to use generic function to differnt data types
  
  
