/*Developer name     : Phiwokwakhe Khathwane 
 * Available at      : phiwokwakhe299@gmail.com
 * Project Type      : Class Library
 * Project name      : File control Library
*/
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Control.Files 
{
    //For file and directory contents
    [Serializable]
    public static class Files
    {
        //Getting Files from
        public static List<string[]> GetFileNamesWithDirectories(string[] Directories) //where T : IEnumerable<T>
        {
            return GetFiles(Directories);
        }//GetFileNamesWithDirectories
        public static List<string[]> GetFileNamesWithoutDirectories(string[] Directory) //where T : IEnumerable<T>
        {
            return GetFileNames(GetFiles(Directory));
        }//GetFileNamesWithoutDirectories

        private static List<string[]> GetFiles(string[] directory) //where T : IEnumerable<T>
        {
            List<string[]> lstDirectories = new List<string[]>();
            string[] sFiles;
            foreach (var s in directory)
            {
                sFiles = Directory.GetFiles(Convert.ToString(s));
                lstDirectories.Add(sFiles);
            }
            return lstDirectories;
        }//GetName
        private static List<string[]> GetFileNames(List<string[]> lstDirectories)
        {
            string longFile = "";
            string[] sFiles;

            List<string[]> lstFiles = new List<string[]>();

            foreach (string[] s in lstDirectories)
            {
                foreach (string ss in s)
                {
                    longFile += Path.GetFileName(ss) + "*";
                }

                sFiles = longFile.Split('*');
                lstFiles.Add(sFiles);
            }
            return lstFiles;
        }
    }//Files

    /// <summary>
    /// For Moving, deleting and copying files to different locations
    /// </summary>


    public static class FileControl 
    {
        public static void DeleteFile(string File, out bool isSuccess)
        {
            isSuccess = Delete(new string[] { File });
        }//DeleteSingleFile

        public static void DeleteFile(string[] Files, out bool isSuccess)
        {
            isSuccess = Delete(Files);
        }//DeleteMultipleFiles

        private static  bool Delete(string[] Files)
        {
            try
            {
                foreach(string sFile in Files)
                    File.Delete(sFile);
                return true;
            }
            catch {return false;}
        }//Delete

        public static void MoveFile(string File,string Destination, out bool isSuccess)
        {
            isSuccess = Move(new string[] { File},Destination);
        }//MoveSingleFile
        public static void MoveFile(string[] Files,string Desination, out bool isSuccess)
        {
            isSuccess = Move(Files, Desination);
        }//MoveMultipleFiles
        private static bool Move(string[] Files , string Destination)
        {
            try
            {
                foreach (string sFile in Files)
                    File.Move(sFile, Destination);
                return true;
            }
            catch { return false; }
        }//Move
        public static void CopyFile(string File,string Destination, out bool isSuccess)
        {
            isSuccess = Copy(new string[] { File }, Destination);
        }//CopySingleFile
        public static void CopyFile(string[] Files, string Desination, out bool isSuccess)
        {
            isSuccess = Copy(Files, Desination);
        }//CopyMultipleFiles
        private static bool Copy(string[] Files, string sDestination)
        {
            try
            {
                foreach (string sFile in Files)
                {
                    if (Path.GetDirectoryName(sFile) == Path.GetDirectoryName(sDestination))
                    {
                        string ext = Path.GetExtension(sFile);
                        string sPath = Path.GetFullPath(sFile);
                        string sName = Path.GetFileNameWithoutExtension(sFile);
                        int i = sName.Length + ext.Length;
                        sPath = sPath.Substring(0, sPath.Length - i);
                        string sNewName = sPath + Path.GetFileNameWithoutExtension(sName) + "_Copy" + 1 + ext;
                        sNewName = CopyNumber(1, sNewName, ext, sPath); /*sPath + Path.GetFileNameWithoutExtension(sFile) + "_Copy" + ext;*/
                        File.Copy(sFile, sNewName);
                    }
                    else
                        File.Copy(sFile, sDestination);
                }
                return true;
            }
            catch{ return false; } 
        }//Move
        private static string CopyNumber(int i, string sName, string ext , string sPath)
        {
            //string sNewName = sPath + Path.GetFileNameWithoutExtension(sName) + "_Copy" + i + ext;
            if (File.Exists(sName))
            {
                return CopyNumber(i+ 1, sName.Replace("_Copy"+ (i-1) , "_Copy" + i) , ext, sPath);
            }
            return sName;
        }//CopyNumber
    }//FileControl


    /// <summary>
    /// For files and directory info's
    /// </summary>
    public static class FileDirectoryInfo 
    {
        public static string FileInfo(string sourcefile)
        {
            int pad = 15;
            string sDetails = "FILE NAME : "+Path.GetFileName(sourcefile)+ "\n============================================="+ "\n";
            sDetails += "Atributes ".PadRight(pad) + ":" + File.GetAttributes(sourcefile).ToString()+"\n" + "Creation time ".PadRight(pad) + ":"+
                                File.GetCreationTime(sourcefile).ToString()+ "\n"+ "Last modified ".PadRight(pad) + ":" +  
                                File.GetLastWriteTime(sourcefile) + "\n" + "Last accessed ".PadRight(pad) +":" + 
                                File.GetLastAccessTime(sourcefile);
            return sDetails;
        }
        public static string DirectoryInfo(string sourcedirectory)
        {
	        int iFileCount = Files.GetFileNamesWithDirectories(new string[]{sourcedirectory}).Count;
            int pad = 15;
            string sDetails = "DIRECTORY NAME : " + Path.GetDirectoryName(sourcedirectory) + "\n=============================================" + "\n";
            sDetails +="Creation time ".PadRight(pad) + ":" +
                                Directory.GetCreationTime(sourcedirectory).ToString() + "\n" + "Last modified ".PadRight(pad) + ":" +
                                Directory.GetLastWriteTime(sourcedirectory) + "\n" + "Last accessed ".PadRight(pad) + ":" +
                                Directory.GetLastAccessTime(sourcedirectory) +"\nContains  " + iFileCount + " files";

            return sDetails;
        }//DirectoryInfo
    }//FileDirectoryInfo

    public static class JSerializer 
    {
        /// <summary>
        /// Contains methods to serialize and decerialize files to and from Json format
        /// </summary>
        private static JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All, ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor };
        public static bool SerializeFile<T>(T content, string fileName , bool append = false)
        {
            StreamWriter file = new StreamWriter(fileName, append);
            try
            {
                string sText = JsonConvert.SerializeObject(content, settings);
                file.WriteLine(sText);
                //success = "successful";
                return true;
            }
            catch{return false;}
            finally { file.Close(); }
        }//SerializeFile
        public static bool DeserializeFile<T>(ref T data_reference , string file)
        {
            try
            {
                string sAllText = File.ReadAllText(file);
                data_reference = (T)JsonConvert.DeserializeObject(sAllText, settings);
                return true;
            }
            catch{ return false; }
        }//Decerialze in a JSON FORMAT
    }//

    public static class XSerializer
    {
        public static bool SerializeFile<T>(T contents, string filename, bool append = false)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using(FileStream file = new FileStream(filename , FileMode.Create))
                {
                    serializer.Serialize(file, contents);
                }//Opening and closing file
                return true;
            }//try
            catch
            {
                return false;
            }//end catche
        }//SerializeFile
        public static bool DeserializeFile<T>(ref T data_reference,string _file )
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (FileStream file = new FileStream(_file, FileMode.Open))
                {
                    data_reference =  (T)serializer.Deserialize(file);
                }
               return true;
            }
            catch { return false; }
        }//DeserializeFile
    }//XSERIALIZER
}//namespace

