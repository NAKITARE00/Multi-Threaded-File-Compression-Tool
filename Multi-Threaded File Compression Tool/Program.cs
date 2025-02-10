using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

class FileCompressionTool
{
    // Compress a file using multithreading
    public static async Task CompressFileAsync(string inputFile, string outputFile)
    {
        if (!File.Exists(inputFile))
        {
            Console.WriteLine($"Error: File '{inputFile}' does not exist.");
            return;
        }

        try
        {
            using (FileStream originalFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (FileStream compressedFileStream = new FileStream(outputFile, FileMode.Create))
            using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
            {
                Console.WriteLine($"Compressing {inputFile}...");
                await originalFileStream.CopyToAsync(compressionStream);
            }
            Console.WriteLine($"Compression Complete: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Decompress a file using multithreading
    public static async Task DecompressFileAsync(string compressedFile, string outputFile)
    {
        if (!File.Exists(compressedFile))
        {
            Console.WriteLine($"Error: File '{compressedFile}' does not exist.");
            return;
        }

        try
        {
            using (FileStream compressedFileStream = new FileStream(compressedFile, FileMode.Open))
            using (FileStream decompressedFileStream = new FileStream(outputFile, FileMode.Create))
            using (GZipStream decompressionStream = new GZipStream(compressedFileStream, CompressionMode.Decompress))
            {
                Console.WriteLine($"Decompressing {compressedFile}...");
                await decompressionStream.CopyToAsync(decompressedFileStream);
            }
            Console.WriteLine($"Decompression Complete: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Compress a File");
        Console.WriteLine("2. Decompress a File");
        Console.Write("Enter choice: ");
        string choice = Console.ReadLine();

        Console.Write("Enter input file path: ");
        string inputFile = Console.ReadLine();

        Console.Write("Enter output file path: ");
        string outputFile = Console.ReadLine();

        if (choice == "1")
        {
            await CompressFileAsync(inputFile, outputFile);
        }
        else if (choice == "2")
        {
            await DecompressFileAsync(inputFile, outputFile);
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
}
