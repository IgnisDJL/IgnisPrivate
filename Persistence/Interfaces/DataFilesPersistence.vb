''' <summary>
''' Takes care of saving and getting data files
''' </summary>
Public MustInherit Class DataFilesPersistence

    Public MustOverride Sub initializeImportation()

    Public MustOverride Sub finalizeImportation()

    ''' <summary>Verifies that the archives are in the correct format. If not, corrects the errors.</summary>
    ''' <returns>True if no error has been found or all found errors have been corrected. False if an error could not be corrected.</returns>
    Public MustOverride Function verifyFormat() As Boolean

    ''' <summary>Clears and reset the archives with the right format</summary>
    Public MustOverride Sub reset()

    ''' <summary>Returns the .csv file for the given day</summary>
    ''' <param name="day">The day when the .csv file was created by the factory's program</param> 
    Public MustOverride Function getCSVFile(day As Date) As CSVFile

    ''' <summary>Returns the .log file for the given day</summary>
    ''' <param name="day">The day when the .log file was created by the factory's program</param> 
    Public MustOverride Function getLOGFile(day As Date) As LOGFile

    ''' <summary>Returns all the .csv files stored in the database</summary>
    Public MustOverride Function getAllCSVFiles() As List(Of CSVFile)

    ''' <summary>Returns all the .log files stored in the database</summary>
    Public MustOverride Function getAllLOGFiles() As List(Of LOGFile)

    ''' <summary>Returns the .mdb file</summary>
    Public MustOverride Function getMDBFile() As MDBFile

    ''' <summary>Returns the .log events file for the given day</summary>
    ''' <param name="day">The day when the file was created by the factory's program</param> 
    Public MustOverride Function getEventsFile(day As Date) As EventsFile

    ''' <summary>Returns all the .log files stored in the database</summary>
    Public MustOverride Function getAllEventsFiles() As List(Of EventsFile)

    ''' <summary>Returns all the csv, log and events files in the database</summary>
    ''' <remarks>Order of array is [CSV, LOG, EVENT]</remarks>
    Public MustOverride Function getAllCSVLOGAndEventsFiles() As List(Of DataFile())

    ''' <summary>Saves the given CSV file in the archives</summary>
    ''' <param name="fileToImport">The file to copy to the archives</param>
    ''' <returns>The file information of the file that was created and saved</returns>
    Public MustOverride Function addCSVFile(fileToImport As IO.FileInfo) As IO.FileInfo

    ''' <summary>Saves the given LOG file in the archives</summary>
    ''' <param name="fileToImport">The file to copy to the archives</param>
    ''' <returns>The file information of the file that was created and saved</returns>
    Public MustOverride Function addLOGFile(fileToImport As IO.FileInfo) As IO.FileInfo

    ''' <summary>Saves the given MDB file in the archives</summary>
    ''' <param name="fileToImport">The file to copy to the archives</param>
    ''' <returns>The file information of the main file that was created and saved</returns>
    Public MustOverride Function addMDBFile(fileToImport As IO.FileInfo) As IO.FileInfo

    ''' <summary>Saves the given events file in the archives</summary>
    ''' <param name="fileToImport">The file to copy to the archives</param>
    ''' <returns>The file information of the file that was created and saved</returns>
    Public MustOverride Function addEventsFile(fileToImport As IO.FileInfo) As IO.FileInfo


End Class
